using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHub : MonoBehaviour
{
    private static InputHub instance;

    //[SerializeField] private GameObject player; //Référence au joueur pour accéder à ses variables

    private GameManager gameManager;
    public PlayerInput myPlayerInput;
    public MyInputAction myInputAction;
    private InputAction moveAction;
    private InputAction interactAction;
    private InputAction cancelAction;
    private InputAction pauseAction;

    public static InputHub Instance { get => instance; set => instance = value; }


    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }


        myInputAction = new MyInputAction();

        interactAction = myInputAction.Player.Interact;
        cancelAction = myInputAction.Player.Cancel;
        pauseAction = myInputAction.Player.Pause;
    }

    private void Start()
    {
        gameManager = GameManager.instance;
        myPlayerInput = GetComponent<PlayerInput>();

    }

    public void OnEnable()
    {
        myInputAction.Enable();
        this.GetComponent<PlayerInput>().ActivateInput();
    }

    public void OnDisable()
    {
        myInputAction.Disable();
        this.GetComponent<PlayerInput>().DeactivateInput();
    }


    #region Inputs

    public void OnMove(InputAction.CallbackContext context)
    {
        gameManager.player.GetComponent<PlayerEntity>().Move = context.ReadValue<Vector2>();
        gameManager.player.GetComponent<PlayerEntity>().XAxis = gameManager.player.GetComponent<PlayerEntity>().Move.x;
        gameManager.player.GetComponent<PlayerEntity>().ZAxis = gameManager.player.GetComponent<PlayerEntity>().Move.y;
    }

    private void Interact()
    {
        //if(interactAction.triggered)
        if(interactAction.IsPressed())
        {
            gameManager.player.GetComponent<PlayerEntity>().isInteracting = true;
        }
        else
        {
            gameManager.player.GetComponent<PlayerEntity>().isInteracting = false;
        }
    }

    private void Cancel()
    {
        if(cancelAction.triggered)
        {
            gameManager.player.GetComponent<PlayerEntity>().isCanceling = true;
        }
        else
        {
            gameManager.player.GetComponent<PlayerEntity>().isCanceling = false;
        }
    }

    private void PauseGame()
    {
        if (pauseAction.triggered)
        {
            gameManager.isPausingGame = true;
        }
        else
        {
            gameManager.isPausingGame = false;
        }
    }

    public string GetCurrentScheme()
    {
        string currentScheme = myPlayerInput.currentControlScheme;
        return currentScheme;
    }

    #endregion

    // Update is called once per frame
    void Update()
    {
        Interact();
        Cancel();
        PauseGame();

        GetCurrentScheme();
    }
}
