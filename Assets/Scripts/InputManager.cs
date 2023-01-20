using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private static InputManager instance;

    //[SerializeField] private GameObject player; //Référence au joueur pour accéder à ses variables

    private GameManager gameManager;
    public PlayerInput myPlayerInput;
    public MyInputAction myInputAction;
    private InputAction moveAction;
    private InputAction meleeAction;
    private InputAction interactAction;
    private InputAction cancelAction;
    private InputAction returnAttackAction;
    private InputAction dodgeAction;
    private InputAction fireAction;
    private InputAction shieldAction;
    private InputAction slashAction;
    private InputAction pauseAction;

    private InputAction UISubmit;
    public bool UISubmitPressed = false;

    public static InputManager Instance { get => instance; set => instance = value; }


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
        meleeAction = myInputAction.Player.Melee;
        returnAttackAction = myInputAction.Player.ReturnAttack;
   
        dodgeAction = myInputAction.Player.Dodge;
        fireAction = myInputAction.Player.Fire;
        shieldAction = myInputAction.Player.Shield;
        interactAction = myInputAction.Player.Interact;
        cancelAction = myInputAction.Player.Cancel;
        slashAction = myInputAction.Player.Slash;
        pauseAction = myInputAction.Player.Pause;
        UISubmit = myInputAction.UI.Submit;
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

    //public void EnableInput()
    //{
    //    gameManager.inputManager.GetComponent<PlayerInput>().ActivateInput();
    //}

    //public void DisableInput()
    //{
    //    gameManager.inputManager.GetComponent<PlayerInput>().DeactivateInput();
    //}

    #region Inputs

    public void OnMove(InputAction.CallbackContext context)
    {
        gameManager.player.GetComponent<PlayerEntity>().Move = context.ReadValue<Vector2>();
        gameManager.player.GetComponent<PlayerEntity>().XAxis = gameManager.player.GetComponent<PlayerEntity>().Move.x;
        gameManager.player.GetComponent<PlayerEntity>().ZAxis = gameManager.player.GetComponent<PlayerEntity>().Move.y;
    }


    //public void OnJump(InputAction.CallbackContext context)
    //{
    //    gameManager.player.GetComponent<PlayerEntity>().IsJumping = context.performed;
    //}

    //private void Jump()
    //{
    //    if (jumpAction.triggered)
    //    {
    //        gameManager.player.GetComponent<PlayerEntity>().IsJumping = true;
    //    }
    //    else
    //    {
    //        gameManager.player.GetComponent<PlayerEntity>().IsJumping = false;
    //    }
    //}

    //public void OnRun(InputAction.CallbackContext context)
    //{
    //    gameManager.player.GetComponent<PlayerEntity>().IsRunning = context.performed;
    //}

    //public void OnDodge(InputAction.CallbackContext context)
    //{
    //    gameManager.player.GetComponent<PlayerEntity>().IsDodging = context.performed;
    //}

    private void Dodge()
    {
        if(dodgeAction.triggered)
        {
            gameManager.player.GetComponent<PlayerEntity>().IsDodging = true;
        }
        else
        {
            gameManager.player.GetComponent<PlayerEntity>().IsDodging = false;
        }
    }

    //public void OnFire(InputAction.CallbackContext context)
    //{
    //    gameManager.player.GetComponent<PlayerEntity>().IsFiring = context.performed;
    //}

    private void Fire()
    {
        //if (fireAction.IsPressed())
        if(fireAction.triggered)
        {
            gameManager.player.GetComponent<PlayerEntity>().IsFiring = true;
        }
        else
        {
            gameManager.player.GetComponent<PlayerEntity>().IsFiring = false;
        }
    }

    //public void OnMelee(InputAction.CallbackContext context)
    //{
    //    gameManager.player.GetComponent<PlayerEntity>().IsUsingMelee = context.performed;
    //}

    private void Melee()
    {
        if (meleeAction.triggered)
        {
            gameManager.player.GetComponent<PlayerEntity>().IsUsingMelee = true;
        }
        else
        {
            gameManager.player.GetComponent<PlayerEntity>().IsUsingMelee = false;
        }
    }

    //public void OnShield(InputAction.CallbackContext context)
    //{
    //    gameManager.player.GetComponent<PlayerEntity>().IsUsingShield = context.performed;
    //}

    private void Shield()
    {
        if(shieldAction.IsPressed())
        {
            gameManager.player.GetComponent<PlayerEntity>().IsUsingShield = true;
        }
        else
        {
            gameManager.player.GetComponent<PlayerEntity>().IsUsingShield = false;
        }
    }

    //public void OnPick(InputAction.CallbackContext context)
    //{
    //    gameManager.player.GetComponent<PlayerEntity>().IsPicking = context.performed;
    //}

    //private void Pick()
    //{
    //    if(pickAction.triggered)
    //    {
    //        gameManager.player.GetComponent<PlayerEntity>().IsPicking = true;
    //    }
    //    else
    //    {
    //        gameManager.player.GetComponent<PlayerEntity>().IsPicking = false;
    //    }
    //}

    //public void OnReturningAttack(InputAction.CallbackContext context)
    //{
    //    gameManager.player.GetComponent<PlayerEntity>().IsReturningAttack = context.performed;
    //}

    //private void ReturningAttack()
    //{
    //    if(returnAttackAction.triggered)
    //    {
    //        gameManager.player.GetComponent<PlayerEntity>().IsReturningAttack = true;
    //    }
    //    else
    //    {
    //        gameManager.player.GetComponent<PlayerEntity>().IsReturningAttack = false;
    //    }
    //}

    //public void OnSlash(InputAction.CallbackContext context)
    //{
    //    gameManager.player.GetComponent<PlayerEntity>().IsSlashing = context.performed;
    //}

    private void Slash()
    {
        if (slashAction.triggered)
        {
            gameManager.player.GetComponent<PlayerEntity>().IsSlashing = true;
        }
        else
        {
            gameManager.player.GetComponent<PlayerEntity>().IsSlashing = false;
        }
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

    private void Submit()
    {
        if (UISubmit.triggered)
        {
            UISubmitPressed = true;
        }
        else { UISubmitPressed = false; }
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
        //Jump();
        Melee();
        //ReturningAttack();
        Dodge();
        Fire();
        Shield();
        //Pick();
        Slash();
        Interact();
        Cancel();
        PauseGame();
        Submit();

        GetCurrentScheme();
    }
}
