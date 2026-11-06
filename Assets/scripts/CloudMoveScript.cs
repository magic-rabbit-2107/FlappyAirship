using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMoveScript : MonoBehaviour
{
    public bool cloudOffScreen;


    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position = transform.position + (Vector3.left * Time.deltaTime) * Random.Range(10,20);

        Vector3 cloudPos = Camera.main.WorldToScreenPoint(transform.position);

        if (cloudPos.x < 0)
        {            
            cloudOffScreen = true;
        }
    }




}
