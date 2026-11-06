using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnBGMoveScript : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float deadZone = -Screen.width;
    public bool movePipe = true;
    public bool pipeOffScreen = false;


    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        if (!movePipe) return;

        transform.position = transform.position + (Vector3.left * Time.deltaTime) * Random.Range(2,20);

        Vector3 columnPos = Camera.main.WorldToScreenPoint(transform.position);

        if (columnPos.x < 0)
        {
            pipeOffScreen = true;
        }
    }

    public void destroyPipe()
    {
        Destroy(gameObject);
    }
}
