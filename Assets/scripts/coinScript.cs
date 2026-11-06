using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class coinScript : MonoBehaviour
{
    public GameObject coinGameObject;
    private LogicScript logicScript;

    private float _timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        logicScript = GameObject.Find("LogicManager").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(coinGameObject);
        logicScript.addScore(100);
        logicScript.playCollectCoinSound();
    }
}
