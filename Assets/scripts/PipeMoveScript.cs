using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeMoveScript : MonoBehaviour
{
    public int moveSpeed = 1000;
    public float deadZone = - Screen.width * 2;
    public bool movePipe = true;
    public int pipeId;
    private GameObject middleSection;
    private GameObject _topPipe;


    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {

        if (!movePipe) return;
          
        movePipeAcrossScreen();
    }

    void movePipeAcrossScreen()
    {
        transform.position = transform.position + (Vector3.left * moveSpeed) * (Time.deltaTime * 2);

        if (pipeId == 2 && transform.position.x < deadZone)
        {
            Destroy(gameObject);
        }
    }


    public GameObject getTopPipe()
    {
        return _topPipe;
    }


}
