using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GameOverTitleScript : MonoBehaviour
{
    public Sprite[] characterArray;

    public String screenTitle;

    private List<Sprite> spritesToShow;
    void  Awake() {

        spritesToShow = new List<Sprite>();

        for (int charIndex = 0; charIndex < screenTitle.Length; charIndex++)
        {
           var charToFind = screenTitle[charIndex].ToString().ToUpper();
           Debug.Log(charToFind);
           if (charToFind != " ")
           {
              var spriteToAdd = characterArray.Where(sprite => sprite.name == charToFind).ToArray()[0];
              spritesToShow.Add(spriteToAdd);
           } else {
              var spriteToAdd = characterArray.Where(sprite => sprite.name == "space").ToArray()[0];
              spritesToShow.Add(spriteToAdd);
           }
          
        }

        Vector3 startingVector = transform.position;

        Vector3 positionChange;

        float xPos = 0;

        float width = 0;



        for (int spriteIndex = 0; spriteIndex < spritesToShow.Count; spriteIndex++)
        {
            var gameObject = new GameObject ();
            gameObject.transform.parent = transform;

            var spriteRenderer = gameObject.AddComponent<SpriteRenderer>();

            spriteRenderer.sprite = spritesToShow[spriteIndex];

            gameObject.name = spriteRenderer.sprite.name;

            gameObject.transform.localScale = new Vector3(5,5,5);


            xPos += spriteRenderer.bounds.size.x;

            width += spriteRenderer.bounds.size.x;

            positionChange = new Vector3(xPos,0,0);


            gameObject.transform.position += positionChange;

            spriteRenderer.sortingOrder  = 8;
            spriteRenderer.sortingLayerName = "Colliders";
       
        }

        transform.position -= new Vector3(width * 0.5f, 0, 0);

        // characterArray[0];
        // var gameObject = new GameObject ();

        // gameObject.transform.parent = transform;

        // var spriteRenderer = gameObject.AddComponent<SpriteRenderer>();

        // spriteRenderer.sprite = characterArray[0];

        // gameObject.name = characterArray[0].name;

        // gameObject.transform.localScale = new Vector3(5,5,5);



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
