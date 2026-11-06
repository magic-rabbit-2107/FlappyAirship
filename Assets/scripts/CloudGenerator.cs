using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;



public class CloudGenerator : MonoBehaviour
{
    public GameObject cloud1;

    public GameObject cloud2;

    public GameObject cloud3;

    public GameObject cloud4;

    public GameObject cloud5;

    private List<GameObject> _trackedPrefabObjects;

    private const int cloudStartPos = 26;

    private int _cloudSpawnInterval;

    private float _timer = 0;

    private List<GameObject> _cloudObjectPrefabs;


    // Start is called before the first frame update
    void Start()
    {
        _trackedPrefabObjects = new List<GameObject>();

        _cloudObjectPrefabs = new List<GameObject>();

        _cloudObjectPrefabs.Add(cloud1);
        _cloudObjectPrefabs.Add(cloud2);
        _cloudObjectPrefabs.Add(cloud3);
        _cloudObjectPrefabs.Add(cloud4);
        _cloudObjectPrefabs.Add(cloud5);

        createCloud();
         

        
    }

    private void createCloud()
    {
        var cloudYPosition = Random.Range(-11,11);

        int randomIndex = Random.Range(0, _cloudObjectPrefabs.Count-1);

        var newCloudObject = Instantiate(_cloudObjectPrefabs[randomIndex], new Vector3(cloudStartPos, cloudYPosition, 0), transform.rotation);

        var randomScale = Random.Range(1,10);

        newCloudObject.transform.localScale += new Vector3(randomScale,randomScale,0);

        newCloudObject.transform.parent = transform;

        _trackedPrefabObjects.Add(newCloudObject);

        _cloudSpawnInterval = Random.Range(1, 5);


    }

    private void removeClouds()
    {
        for (int cloudIndex = 0; cloudIndex < _trackedPrefabObjects.Count; cloudIndex++)
        {

            var cloudObject = _trackedPrefabObjects[cloudIndex];
            
            var cloudOffScreen = cloudObject.GetComponent<CloudMoveScript>().cloudOffScreen;
            
            if (cloudOffScreen)
            {
                _trackedPrefabObjects.RemoveAt(cloudIndex);
                Destroy(cloudObject);
            }
               
        }
    }

    private void spawnCloudsAtRandomIntervals()
    {
        if (_timer < _cloudSpawnInterval)
        {
            _timer += Time.deltaTime;
        }
        else
        {
            createCloud();
            _timer = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {

        spawnCloudsAtRandomIntervals();

        removeClouds();

    }
}
