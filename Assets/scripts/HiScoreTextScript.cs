using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class HiScoreTextScript : MonoBehaviour
{
    public LogicScript logicScript;

     public Sprite[] characterArray;

     private List<Sprite> _spritesToShow;

     private List<GameObject> _spriteGameObjects;

     private GameObject[] _gameObjects;

     private Camera _camera;

     private float _timer = 0;

     private float _currentScore = 0;

     public int _countFPS = 30;


     private GameObject _scoreNumber; 

     private int _value;

     private Coroutine _countingCoroutine;

     private float _duration = 1f;

     private int _scoreValue
     {
        get
        {
            return _value;
        }
        set
        {
            UpdateText(value);
            _value = value;
        }
     }


    void  Awake() {

       

        _camera = Camera.main;

        Debug.Log("_backgroundSprite:"+_camera);

        _spriteGameObjects = new List<GameObject>();

        _spritesToShow = new List<Sprite>();

        _scoreNumber = new GameObject();

        _scoreNumber.SetActive(false);

        _scoreNumber.transform.parent = transform;

        var spriteRenderer = _scoreNumber.AddComponent<SpriteRenderer>();

        spriteRenderer.sprite = characterArray[0];

        _scoreNumber.name = spriteRenderer.sprite.name;

        _scoreNumber.transform.localScale = new Vector3(5,5,5);

                



          spriteRenderer.sortingOrder  = 5;
            spriteRenderer.sortingLayerName = "Colliders";

        
    }

    void updateScore(int scoreNumber) 
    {

        if (_spriteGameObjects != null)
        {
            for (int objIndx = 0; objIndx < _spriteGameObjects.Count; objIndx++)
            {
                Destroy(_spriteGameObjects[objIndx]);
            }

             _spriteGameObjects.Clear();
        }

        _spritesToShow.Clear();
    
       string scoreText = scoreNumber.ToString("D6");

        for (int charIndex = 0; charIndex < scoreText.Length; charIndex++)
        {
          var charToFind = scoreText[charIndex].ToString().ToUpper();

           if (charToFind == " ") {
              var spriteToAdd = characterArray.Where(sprite => sprite.name == "space").ToArray()[0];
               _spritesToShow.Add(spriteToAdd);
            } else {
               var spriteToAdd = characterArray.Where(sprite => sprite.name == charToFind).ToArray()[0];
               _spritesToShow.Add(spriteToAdd);
            }
        }

        // _spritesToShow.Reverse();


        Vector3 startingVector = transform.position;

        float xPos = Screen.width * 0.95f;

        float width = 0;

        int lastSpriteIndex = _spritesToShow.Count - 1;


        for (int spriteIndex = 0; spriteIndex < _spritesToShow.Count; spriteIndex++)
        {
            GameObject letterSprite = Instantiate( _scoreNumber );

            letterSprite.GetComponent<SpriteRenderer>().sprite = _spritesToShow[spriteIndex];
            letterSprite.name = letterSprite.GetComponent<SpriteRenderer>().sprite.name ;

            letterSprite.transform.position = _camera.ScreenToWorldPoint(new Vector3(Screen.width * 0.05f, Screen.height * 0.8f, _camera.nearClipPlane));

            width += letterSprite.GetComponent<SpriteRenderer>().bounds.size.x;

            letterSprite.transform.position += new Vector3(width, 0, 0);

            letterSprite.transform.parent = transform;

            letterSprite.SetActive(true);

            _spriteGameObjects.Add(letterSprite);

        }

        for (int spriteIndex = 0; spriteIndex < _spriteGameObjects.Count; spriteIndex++)
        {
            GameObject letterSprite = _spriteGameObjects[spriteIndex];
            letterSprite.transform.position -= new Vector3(letterSprite.GetComponent<SpriteRenderer>().bounds.size.x, 0, 0);
        }
                   

    }
    void Start()
    {
        logicScript = GameObject.Find("LogicManager").GetComponent<LogicScript>();
        _scoreValue = 0;

        updateScore(PlayerPrefs.GetInt("highscore"));
    }

    private IEnumerator CountText(int newValue)
    {
        WaitForSeconds Wait = new WaitForSeconds(1f / _countFPS);

        int previousValue = _value;
        int stepAmount;

        if (newValue - previousValue < 0)
        {
            stepAmount = Mathf.FloorToInt((newValue - previousValue) / (_countFPS * _duration)); // newValue = -20, previousValue = 0. CountFPS = 30, and Duration = 1; (-20- 0) / (30*1) // -0.66667 (ceiltoint)-> 0
        }
         else
        {
            stepAmount = Mathf.CeilToInt((newValue - previousValue) / (_countFPS * _duration)); // newValue = 20, previousValue = 0. CountFPS = 30, and Duration = 1; (20- 0) / (30*1) // 0.66667 (floortoint)-> 0
        }

        if (previousValue < newValue)
        {
            while(previousValue < newValue)
            {
                previousValue += stepAmount;
                if (previousValue > newValue)
                {
                    previousValue = newValue;
                }

                updateScore(previousValue);

                yield return Wait;
            }
        }
        else
        {
            while (previousValue > newValue)
            {
                previousValue += stepAmount; // (-20 - 0) / (30 * 1) = -0.66667 -> -1              0 + -1 = -1
                if (previousValue < newValue)
                {
                    previousValue = newValue;
                }

                updateScore(previousValue);

                yield return Wait;
            }
        }

    }

    private void UpdateText(int newValue)
    {
        if (_countingCoroutine != null)
        {
            StopCoroutine(_countingCoroutine);
        }

        _countingCoroutine = StartCoroutine(CountText(newValue));
    }

    // Update is called once per frame
    void Update()
    {
        int playerScore = logicScript.getPlayerScore();
        // updateScore((int)playerScore);

        // _currentScore < targetValue

        if (playerScore > PlayerPrefs.GetInt("highscore"))
        {
            PlayerPrefs.SetInt("highscore",playerScore);
             _scoreValue = playerScore;
        }

        
        

    }

   

}

