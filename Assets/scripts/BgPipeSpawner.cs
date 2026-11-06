using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgPipeSpawner : MonoBehaviour
{
    public GameObject bgPipePrefab1;
    public GameObject bgPipePrefab2;
    public GameObject bgPipePrefab3;
    public GameObject bgPipePrefab4;

    private List<GameObject> _bgPipePrefabs;

    private List<GameObject> _trackedPrefabObjects;
    
    public bool stopUpdate;


    // Start is called before the first frame update
    void Start()
    {
        _bgPipePrefabs = new List<GameObject>();

        _bgPipePrefabs.Add(bgPipePrefab1);
        _bgPipePrefabs.Add(bgPipePrefab2);
        _bgPipePrefabs.Add(bgPipePrefab3);
        _bgPipePrefabs.Add(bgPipePrefab4);

        createInitialSetOfBgPipes();

    }

    private void createInitialSetOfBgPipes()
    {
        _trackedPrefabObjects = new List<GameObject>();
        int startX = 0;

        int numInitialPrefabs = 20;

        for (int prefabIndex = 0; prefabIndex < numInitialPrefabs; prefabIndex++)
        {
            int randomIndex = Random.Range(0, _bgPipePrefabs.Count);
            var newObject = Instantiate(_bgPipePrefabs[randomIndex], new Vector3(startX, transform.position.y, 0), transform.rotation);

            newObject.transform.parent = transform;

            _trackedPrefabObjects.Add(newObject);
            startX += Random.Range(10,30);
        }
    }



    private void trackBgPipePositions()
    {
        for (int bgPipeIndex = 0; bgPipeIndex < _trackedPrefabObjects.Count; bgPipeIndex++)
        {
            var pipeOffScreen = _trackedPrefabObjects[bgPipeIndex].GetComponent<ColumnBGMoveScript>().pipeOffScreen;

            if (pipeOffScreen)
            {
                _trackedPrefabObjects[bgPipeIndex].GetComponent<ColumnBGMoveScript>().destroyPipe();
                _trackedPrefabObjects.RemoveAt(bgPipeIndex);

                var lastIndex = _trackedPrefabObjects.Count - 1;

                var newObject = Instantiate(_bgPipePrefabs[Random.Range(0, _bgPipePrefabs.Count)], new Vector3(_trackedPrefabObjects[lastIndex].transform.position.x + Random.Range(10,30), transform.position.y, 0), transform.rotation);

                newObject.transform.parent = transform;

                _trackedPrefabObjects.Add(newObject);
            }
        }

    }
    // Update is called once per frame
   public void Update()
    {
        if (stopUpdate) return;

        trackBgPipePositions();
    }


}
