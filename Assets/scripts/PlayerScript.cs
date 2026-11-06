using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    public Rigidbody2D playerRigidBody;
    public float flapStrength;
    private LogicScript _logicScript;

    private bool controlsEnabled = true;
    private SpriteRenderer _spriteRenderer;
    private AudioSource _thudSound;
    //public Animator animator;
    private GameObject _logicManager;


    private PlayerControls _playerControls;

    private Animator _animator;

    private void Awake()
    {
        _playerControls = new PlayerControls();

        _animator = GetComponent<Animator>();

    }


    // Start is called before the first frame update
    void Start()
    {
        _logicManager = GameObject.Find("LogicManager");

        _logicScript = _logicManager.GetComponent<LogicScript>();

        _spriteRenderer = GetComponent<SpriteRenderer>();

        _thudSound = GetComponent<AudioSource>();

        

    }

    private void OnEnable()
    {
        _playerControls.InGame.MoveUp.performed += moveBird;
        _playerControls.InGame.Enable();

        //playerControls.InGame.TouchInput.started += ctx => StartTouch(ctx);
        //playerControls.InGame.TouchInput.canceled += ctx => EndTouch(ctx);

       
    }

    private void onDisabled()
    {
        _playerControls.InGame.Disable();
    }

    private void StartTouch(InputAction.CallbackContext context)
    {
        Debug.Log("StartTouch" + context);
    }

    private void EndTouch(InputAction.CallbackContext context)
    {
        Debug.Log("EndTouch" + context);
    }

    private void moveBird(InputAction.CallbackContext obj)
    {
       if (controlsEnabled)
       {
          playerRigidBody.velocity = Vector2.up * flapStrength;
       }
        
    }


    // Update is called once per frame
    void Update()
    {

        bool playerIsMoving = playerRigidBody.velocity.y == 0 ? false : true;


        Vector3 playerPos = Camera.main.WorldToScreenPoint(playerRigidBody.position);

        float spriteHeight = _spriteRenderer.sprite.rect.height;

        bool birdFliesOffScreen = (playerPos.y + spriteHeight) < 0 || (playerPos.y - spriteHeight) > Screen.height;

        if (birdFliesOffScreen)
        {
            stopPlayerAndEndGame();
            _thudSound.Play();
        }
        



    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("OnCollisionEnter2D:" + collision);
        _thudSound.Play();
        stopPlayerAndEndGame();
    }


    private void stopPlayerAndEndGame()
    {
        _animator.SetBool("PlayerHit",true);
        _logicScript.stopPipes();
        _logicScript.gameOver();
        controlsEnabled = false;
        playerRigidBody.gravityScale = 0.0f;
        playerRigidBody.velocity = Vector2.up * 0;
        playerRigidBody.freezeRotation = true;
    }
}
