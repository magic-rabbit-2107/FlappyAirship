using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreTitleScript : MonoBehaviour
{
    public Sprite[] characterArray;

    public String screenTitle;

    private List<Sprite> spritesToShow;

    private GameObject _titleObject; 

    private Camera _camera;

    private List<GameObject> _spriteGameObjects;

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

        spritesToShow.Reverse();

        Vector3 startingVector = transform.position;

        float xPos = Screen.width * 0.95f;

        float width = 0;

        int lastSpriteIndex = spritesToShow.Count - 1;


        for (int spriteIndex = 0; spriteIndex < spritesToShow.Count; spriteIndex++)
        {
            GameObject letterSprite = Instantiate( _titleObject );

            letterSprite.GetComponent<SpriteRenderer>().sprite = spritesToShow[spriteIndex];
            letterSprite.name = letterSprite.GetComponent<SpriteRenderer>().sprite.name ;

            letterSprite.transform.position = _camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height * 0.9f, _camera.nearClipPlane));

            width += letterSprite.GetComponent<SpriteRenderer>().bounds.size.x;

            letterSprite.transform.position -= new Vector3(width, 0, 0);

            letterSprite.transform.parent = transform;

            letterSprite.SetActive(true);

            _spriteGameObjects.Add(letterSprite);

        }


 



    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
