using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
   /**
   * The player score
   ***/
    public int playerScore;
    public int coinCount;    
    public GameObject gameOverScreen;
    public GameObject menuBg;

    public AudioSource pipeEnterAudioSource;

    public AudioSource collectCoinAudioSource;

    private BgPipeSpawner _bgPipeSpawnScript;

    private GameObject _backgroundObject;


    void  Awake() {
        _backgroundObject = GameObject.Find("Background");

        _bgPipeSpawnScript = _backgroundObject.GetComponent<BgPipeSpawner>();

    }

    [ContextMenu("Increase Score")]
    public void addScore(int scoreToAdd)
    {
        Debug.Log("add score"+scoreToAdd);

        playerScore += scoreToAdd;
        Debug.Log("playerScore"+playerScore);

        // pingSound = GetComponent<AudioSource>();

        // Debug.Log(pingSound);
        // pingSound.Play();

    }

    public int getPlayerScore()
    {
        return playerScore;
    }


    public void loadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void gameOver()
    {
        if (playerScore > PlayerPrefs.GetInt("highscore"))
        {
            PlayerPrefs.SetInt("highscore", playerScore);
        }
        delayLoadScene(1);
    }

    public void stopPipes()
    {
        var pipeGameObjects = GameObject.FindGameObjectsWithTag("Pipe");

        foreach (var pipe in pipeGameObjects)
        {
            pipe.GetComponent<PipeMoveScript>().movePipe = false;
        }

        var pipeSpawner = GameObject.FindGameObjectWithTag("PipeSpawner");

        pipeSpawner.GetComponent<PipeSpawnScript>().stopSpawn();

        _bgPipeSpawnScript.stopUpdate = true;

        var backgroundPipeObjects = GameObject.FindGameObjectsWithTag("BackgroundPipe");

        foreach (var bgPipe in backgroundPipeObjects)
        {
            bgPipe.GetComponent<ColumnBGMoveScript>().movePipe = false;
        }

    }

    public void playPipeEnterSound()
    {
        if (pipeEnterAudioSource.isPlaying == false)
        {
            pipeEnterAudioSource.Play();
        }
        
    }

    public void playCollectCoinSound()
    {
        if (collectCoinAudioSource.isPlaying == false)
        {
            collectCoinAudioSource.Play();
        }   
    }

    public void delayLoadScene(float delayTime)
    {
        Debug.Log("delayLoadScene" + delayTime);
        StartCoroutine(DelayLoadScene(delayTime));
    }

    IEnumerator DelayLoadScene(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        menuBg.SetActive(true);

        gameOverScreen.SetActive(true);
    }

    private void Update()
    {
        
    }

}
