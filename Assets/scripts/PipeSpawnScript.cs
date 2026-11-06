using System.Collections.Generic;
using UnityEngine;

public class PipeSpawnScript : MonoBehaviour
{
    public GameObject colliderPipePrefab;
    public GameObject coinPrefab;
    public GameObject bgPipePrefab1;
    public GameObject bgPipePrefab2;
    public GameObject bgPipePrefab3;
    public GameObject bgPipePrefab4;
    public Sprite coinSprite;
    public float shortestSpawnInterval = 1;
    public float longestSpawnInterval = 3;
    private float _timer = 0;
    public float heightOffset = 0;
    private double _colliderPipeSpawnInterval;
    private bool _spawnStop = false;
    private List<float> _yPositions;
    private float _lowestYPoint;
    private float _highestYPoint;

    private GameObject firstColliderPipeGameObject;

    private GameObject secondColliderPipeGameObject;


    private float _numPositions = 1000;

    private int _yPositionIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        initiateColliderPipePositions();

        spawnColliderPipes();

        _colliderPipeSpawnInterval = Random.Range(0, 1);

    }

    // Update is called once per frame
    void Update()
    {



        checkColliderPipePositionsAndSpawnNewPipe();



    }

    private void initiateColliderPipePositions()
    {
        _lowestYPoint = transform.position.y - heightOffset;
        _highestYPoint = transform.position.y + heightOffset;

        _yPositions = new List<float>();


        for (var yPosIndex = 0; yPosIndex < _numPositions; yPosIndex++)
        {
            _yPositions.Add(Random.Range(_lowestYPoint, _highestYPoint));
        }

        
    }

    private void spawnColliderPipes()
    {

        _yPositionIndex += 1;

        if (_yPositionIndex >= _numPositions)
        {
            _yPositionIndex = 0;
        }

        float pipe1YPosition = Random.Range(_lowestYPoint, _highestYPoint);



        firstColliderPipeGameObject = Instantiate(colliderPipePrefab, new Vector3(transform.position.x, pipe1YPosition, 0), transform.rotation);

        firstColliderPipeGameObject.GetComponent<PipeMoveScript>().pipeId = 1;

        firstColliderPipeGameObject.transform.parent = transform;

        float yPos = Random.Range(-10, 10);

        bool positiveNumber = yPos > 0;

        List<List<int>> valueList = new List<List<int>>();

        valueList.Add(new List<int>() { 0, -5, -8, -10, -8, -5, -0 });

        valueList.Add(new List<int>() { 0, -5, -8, -5, 0, 5, 8, 5, 0, -5, -8 });

        valueList.Add(new List<int>() { 0, -5, -8, -5, 0, 5, 8, 5, 0, -5, -8 });

        valueList.Add(new List<int>() { 0, 2, 4, 6, 6, 6, 6, 6, 6, 6, 6, 6 });



        List<GameObject> coinsObjects = new List<GameObject>();

        float xSize = 4f;
        float yMaximum = 100f;

        var listOfValues = valueList[Random.Range(0, valueList.Count)];

        for (int i = 0; i < listOfValues.Count; i++)
        {

            float xPosition = xSize + i * xSize;
            float yPosition = (listOfValues[i] / yMaximum) * Screen.height;

            var coin = Instantiate(coinPrefab, new Vector3(transform.position.x + xPosition, pipe1YPosition + listOfValues[i], 0), transform.rotation);

            coin.transform.parent = firstColliderPipeGameObject.transform;

            coinsObjects.Add(coin);

        }


        Vector3 lastCoinPostion = coinsObjects[coinsObjects.Count - 1].transform.position;

        secondColliderPipeGameObject = Instantiate(colliderPipePrefab, new Vector3(lastCoinPostion.x + 1, lastCoinPostion.y, 0), transform.rotation);

        secondColliderPipeGameObject.GetComponent<PipeMoveScript>().pipeId = 2;

        secondColliderPipeGameObject.transform.parent = transform;
    }

    private void checkColliderPipePositionsAndSpawnNewPipe()
    {
        if (_spawnStop) return;

        if (secondColliderPipeGameObject.transform.position.x < 0)
        {
            if (_timer < _colliderPipeSpawnInterval)
            {
                _timer += Time.deltaTime;

            }
            else
            {
                spawnColliderPipes();
                _timer = 0;
            }
        }
    }


    public void stopSpawn()
    {
        _spawnStop = true;
    }
}
