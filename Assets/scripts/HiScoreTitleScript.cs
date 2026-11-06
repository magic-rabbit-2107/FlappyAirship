using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class HiScoreTitleScript : MonoBehaviour
{
    public Sprite[] characterArray;

    public String screenTitle;

    private List<Sprite> spritesToShow;

    private GameObject _titleObject; 

    private Camera _camera;

    private List<GameObject> _spriteGameObjects;
    // Start is called before the first frame update

    void  Awake() {

        _titleObject = new GameObject();

        _titleObject.SetActive(false);

        _titleObject.transform.parent = transform;

        _titleObject.transform.localScale = new Vector3(4,4,4);

        _camera = Camera.main;

        _spriteGameObjects = new List<GameObject>();


         var spriteRenderer = _titleObject.AddComponent<SpriteRenderer>();

        spriteRenderer.sprite = characterArray[0];

        spriteRenderer.sortingOrder  = 5;
            spriteRenderer.sortingLayerName = "Colliders";


        spritesToShow = new List<Sprite>();

        for (int charIndex = 0; charIndex < characterArray.Length; charIndex++)
        {
            spritesToShow.Add(characterArray[charIndex]);
        }

        // spritesToShow.Reverse();

        Vector3 startingVector = transform.position;

        float xPos = Screen.width * 0.05f;

        float width = 0;

        int lastSpriteIndex = spritesToShow.Count - 1;

        


        for (int spriteIndex = 0; spriteIndex < spritesToShow.Count; spriteIndex++)
        {
            GameObject letterSprite = Instantiate( _titleObject );

            letterSprite.GetComponent<SpriteRenderer>().sprite = spritesToShow[spriteIndex];
            letterSprite.name = letterSprite.GetComponent<SpriteRenderer>().sprite.name ;

            double multiplier = 1;

            Debug.Log("letterSprite.name:"+letterSprite.name);

            if (letterSprite.name == "Font01_52")
            {
                multiplier = 1.5;
            }

            if (letterSprite.name == ":")
            {
                multiplier = 2;
            }

            letterSprite.transform.position = _camera.ScreenToWorldPoint(new Vector3(Screen.width * 0.01f, Screen.height * 0.9f, _camera.nearClipPlane));


            double letterSize = letterSprite.GetComponent<SpriteRenderer>().bounds.size.x * multiplier;

            width += (float)letterSize; 

            letterSprite.transform.position += new Vector3(width, 0, 0);

            letterSprite.transform.parent = transform;

            letterSprite.SetActive(true);

            _spriteGameObjects.Add(letterSprite);

        }


 



    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
