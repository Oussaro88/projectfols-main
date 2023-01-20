using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Cinemachine;
using UnityEngine.SceneManagement;

public class PlayerEntity : PhysicalEntity, IShopCustomer, IDataPersistence
{

    #region Variables

    private GameManager gameManager;
    private UIManager uiManager;
    private PlayerEntity playerEntityInstance;
    public GameObject renderedCharacter;
    private CharacterController myCharacter; //Référence au character controller
    private Animator animator;
    [SerializeField] private CinemachineVirtualCamera cam; // Référence à la caméra
    
    //  Variables pour le déplacement
    private Vector3 move;
    private float xAxis;
    private float zAxis;
    private bool isMoving = false;

    //  Variables pour la gravité
    [SerializeField] private bool isGrounded = false;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private float checkGroundDistance;
    public Vector3 velocity;
    [SerializeField] private float gravityForce;
    [SerializeField] private GameObject checkGroundSphere;


    [Header("Movement Attributes")]
    [SerializeField] private float speed = 6f;
    [SerializeField] private float resetSpeedValue = 6f;
    //private bool isRunning;
    //[SerializeField] private float runningSpeed;
    private bool isDodging;
    [SerializeField] private float dodgeTime = 0f;
    [SerializeField] private float dodgeSpeed;
    private Vector3 dodgeVelocity;
    public bool hasExecutedDodge = false;
    public float resetDodgeInputTimer = 0f;

    //  Variables pour l'endurance, points de magie
    [SerializeField] private float currentStamina;
    [SerializeField] private float maxStamina;
    [SerializeField] private float currentMana;
    [SerializeField] private float maxMana;


    // Variables pour le Firing
    [Header("Firing Attributes")]
    [SerializeField] private GameObject bullet; //Référence au projectile
    private bool isFiring = false;
    public bool hasFired = false;
    private float timer;
    private float fireRate = 0.5f;
    [SerializeField] private int bulletVelocity = 15;


    //Variables pour arme courte portée
    //Melee
    [Header("Melee Attributes")]
    private bool isUsingMelee = false;
    [SerializeField] private bool hasUsedMelee = false;
    private Vector3 meleeVelocity;
    public float meleeTime = 0f;
    public float meleeSpeed;


    //Slash
    [Header("Slash Attributes")]
    private Vector3 slashVelocity;
    public float slashSpeed;
    public float slashTimer = 0f;
    private bool isSlashing = false;
    public bool hasRequestedSlash = false;


    //Shield
    [Header("Shield Attributes")]
    private bool isUsingShield = false;
    private float blockCoolDown = 10f;
    public float shieldTimer = 5f;
    public GameObject vfxCube;
    public bool hasBlockedAttack = false;


    // Variables pour Steal & return attack
    [SerializeField] private bool canReturnAttack = false;
    [SerializeField] private bool isReturningAttack = false;
    [SerializeField] private int returnFireIndex = 0;
    private GameObject attackToReturn;
    private CapsuleCollider capsuleCollider; // to steal attack
    [SerializeField] private float timercapsule = 0f;
    public bool hasReturnedAttack = false;
    public float changeStateDelay = 0;
    public bool isStealingAttack = false;



    public List<GameObject> damagedEnemiesList;
    public float resetMeleeInputTimer = 0f;
    public float resetSlashInputTimer = 0f;

    public Material defaultMaterial;
    public Material damageMaterial;
    private bool materíalChanged = false;
    public float resetMaterialTimer = 0f;

    public bool isInvincible = false;

    public bool isInteracting = false;
    public bool isCanceling = false;


    public Vector3 knockedVelocity;
    public bool isKnocked = false;
    public bool hasBeenKnocked = false;
    public float resetKnockedInputTimer = 0f;


    public Material dodgeMaterial;
    public GameObject fxElectricity;
    //public GameObject meleePS;

    //public int _currentWeaponIndex;
    public int _currentMeleeDamage = 30;
    public int _currentSlashDamage = 15;
    public GameObject[] _possesedWeapons;

    /// <summary>
    public float knockBackForce;
    public float knockTime = 0f;
    public bool knockknock = false;
    public Vector3 knockDirection;

    public GameObject newvfx;

    public int weaponIndex;


    #endregion


    #region Encapsulation
    public CharacterController MyCharacter { get => myCharacter; set => myCharacter = value; }
    public CinemachineVirtualCamera Cam { get => cam; set => cam = value; }
    public new Vector3 Move { get => move; set => move = value; }
    public float XAxis { get => xAxis; set => xAxis = value; }
    public float ZAxis { get => zAxis; set => zAxis = value; }
    public float Speed { get => speed; set => speed = value; }
    //public bool IsRunning { get => isRunning; set => isRunning = value; }
    //public float RunningSpeed { get => runningSpeed; set => runningSpeed = value; }
    public bool IsDodging { get => isDodging; set => isDodging = value; }
    public float DodgeTime { get => dodgeTime; set => dodgeTime = value; }
    public float DodgeSpeed { get => dodgeSpeed; set => dodgeSpeed = value; }
    public float GravityForce { get => gravityForce; set => gravityForce = value; }
    public GameObject Bullet { get => bullet; set => bullet = value; }
    public bool IsFiring { get => isFiring; set => isFiring = value; }
    public float Timer { get => timer; set => timer = value; }
    public float FireRate { get => fireRate; set => fireRate = value; }
    public int BulletVelocity { get => bulletVelocity; set => bulletVelocity = value; }
    public bool IsUsingMelee { get => isUsingMelee; set => isUsingMelee = value; }
    public bool HasUsedMelee { get => hasUsedMelee; set => hasUsedMelee = value; }
    public bool IsUsingShield { get => isUsingShield; set => isUsingShield = value; }
    public float BlockCoolDown { get => blockCoolDown; set => blockCoolDown = value; }
    public bool CanReturnAttack { get => canReturnAttack; set => canReturnAttack = value; }
    public bool IsReturningAttack { get => isReturningAttack; set => isReturningAttack = value; }
    public GameObject AttackToReturn { get => attackToReturn; set => attackToReturn = value; }
    public CapsuleCollider CapsuleCollider { get => capsuleCollider; set => capsuleCollider = value; }
    public float Timercapsule { get => timercapsule; set => timercapsule = value; }
    public Animator Animator { get => animator; set => animator = value; }
    public bool IsMoving { get => isMoving; set => isMoving = value; }
    public float GetCurrentMana { get => currentMana; set => currentMana = value; }
    public float GetMaxMana { get => maxMana; set => maxMana = value; }
    public float GetCurrentStamina { get => currentStamina; set => currentStamina = value; }
    public float GetMaxStamina { get => maxStamina; set => maxStamina = value; }
    public int ReturnFireIndex { get => returnFireIndex; set => returnFireIndex = value; }
    public bool IsGrounded { get => isGrounded; set => isGrounded = value; }
    public bool IsSlashing { get => isSlashing; set => isSlashing = value; }
    public float ResetSpeedValue { get => resetSpeedValue; set => resetSpeedValue = value; }
    public Vector3 DodgeVelocity { get => dodgeVelocity; set => dodgeVelocity = value; }
    public Vector3 MeleeVelocity { get => meleeVelocity; set => meleeVelocity = value; }
    public PlayerEntity PlayerEntityInstance { get => playerEntityInstance; set => playerEntityInstance = value; }


    #endregion


    #region StateMachineReferences

    public PlayerStateMachine playerState;
    private PlayerDefaultState defaultState;
    //private PlayerJumpState jumpState;
    private PlayerDodgeState dodgeState;
    //private PlayerPickState pickState;
    private PlayerBlockState blockState;
    private PlayerRangedAttackState rangedAttackState;
    private PlayerMeleeState meleeState;
    private PlayerStealAttackState stealAttackState;
    private PlayerDeathState deathState;
    private PlayerKnockedState knockedState;
    private PlayerSlashState slashState;

    public PlayerDefaultState DefaultState { get => defaultState; }
    //public PlayerJumpState JumpState { get => jumpState; }
    public PlayerDodgeState DodgeState { get => dodgeState; }
    //public PlayerPickState PickState { get => pickState; }
    public PlayerBlockState BlockState { get => blockState; }
    public PlayerRangedAttackState RangedAttackState { get => rangedAttackState; }
    public PlayerMeleeState MeleeState { get => meleeState; }
    public PlayerStealAttackState StealAttackState { get => stealAttackState; set => stealAttackState = value; }
    public PlayerDeathState DeathState { get => deathState; set => deathState = value; }
    public PlayerKnockedState KnockedState { get => knockedState; set => knockedState = value; }
    public PlayerSlashState SlashState { get => slashState; set => slashState = value; }
    public Vector3 SlashVelocity { get => slashVelocity; set => slashVelocity = value; }

    #endregion



    // Start is called before the first frame update
    protected override void Start()
    {
        MyCharacter = GetComponent<CharacterController>(); //Récupère le component character controller dans le gameobject
        Animator = GetComponent<Animator>();
        CapsuleCollider = GetComponent<CapsuleCollider>();
        CapsuleCollider.enabled = false;
        gameManager = GameManager.Instance;
        uiManager = UIManager.Instance;
        damagedEnemiesList = new List<GameObject>();

        defaultState = new PlayerDefaultState(this, playerState);
        dodgeState = new PlayerDodgeState(this, playerState);
        blockState = new PlayerBlockState(this, playerState);
        rangedAttackState = new PlayerRangedAttackState(this, playerState);
        meleeState = new PlayerMeleeState(this, playerState);
        stealAttackState = new PlayerStealAttackState(this, playerState);
        deathState = new PlayerDeathState(this, playerState);
        knockedState = new PlayerKnockedState(this, playerState);
        slashState = new PlayerSlashState(this, playerState);

        playerState = new PlayerStateMachine(DefaultState);

        weaponIndex = PlayerPrefs.GetInt("WeaponIndex");
        int equipped = PlayerPrefs.GetInt("Equipped");
        if (equipped == 0)
        {
            SetCurrentWeapon(3);
        }
        else { SetCurrentWeapon(weaponIndex); } 
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (!gameManager.Paused) 
        {
            if (!gameManager.menuOpened)
            {
                OnManagingGravity();

                ResetCharacterMaterial();

                playerState.Update(); // Excute the running state update
            }

            GetCurrentStamina = Mathf.MoveTowards(GetCurrentStamina, GetMaxStamina, 10f * Time.deltaTime); //Remplit la barre d'endurance
        }

        OnManagingSliders();

        //SetCurrentWeapon();

        //if(SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(3))
        //{
        //    RaycastHit hit;
        //    if (Physics.Raycast(this.gameObject.transform.position, Vector3.down, out hit, 10))
        //    {
        //        if(hit.collider == FindObjectOfType<ColliderFall>().gameObject)
        //        {
        //            this.gameObject.transform.position = FindObjectOfType<ColliderFall>().spawnPointPlayer.transform.position;
        //            //this.gameObject.transform.SetParent(FindObjectOfType<Elevation>().transform);
        //            OnHurt(10);
        //        }
        //    }
        //}
        
        
        if (knockknock)
        {
            knockTime += Time.deltaTime;
            KnockBack();
            if (knockTime >= 0.1f)
            {
                knockknock = false;
                knockTime = 0f;
            }
        }

    }
    
    public void KnockBack()
    {
        move = knockDirection * knockBackForce;
        myCharacter.Move(move * Time.maximumDeltaTime);
    } 
    
    public void KnockBack(Vector3 direction)
    {
        move = direction * knockBackForce;
        myCharacter.Move(move * Time.maximumDeltaTime);
    }

    #region Actions
    public override void OnDeath()
    {
        //Death animation maybe then Respawn to Hub, maybe get health, mana and stamina to full by default?
        animator.runtimeAnimatorController = gameManager.deathController;
        animator.SetBool("Dead", true);
        float waitTime = 0f; //A CORRIGER
        waitTime += Time.deltaTime;
        if (waitTime >= 2.5f)
        {
            animator.SetBool("Dead", false);
        }
    }

    public override void OnHurt(int damage)
    {
        ////Player taking damage
        GetCurrentHP -= damage;
        isInvincible = true;
        renderedCharacter.GetComponent<Renderer>().material = damageMaterial;
        materíalChanged = true;
        if (GetCurrentHP <= 0)
        {
            OnDeath();
        }
    }

    public void ResetCharacterMaterial()
    {
        if(materíalChanged)
        {
            resetMaterialTimer += Time.deltaTime;
            if (resetMaterialTimer >= 2f)
            {
                isInvincible = false;
                renderedCharacter.GetComponent<Renderer>().material = defaultMaterial;
                resetMaterialTimer = 0f;
            }
        }
    }

    public override void OnHeal()
    {
        //Player healing their health points
        if (GetCurrentHP < GetMaxHP)
        {
            GetCurrentHP = GetMaxHP;
        }
    }

    //Not sure if it's the player that should manage the following methods below...
    public void OnInteract()
    {
        //How the player interacts with the NPC or objects in the hub?
    }

    public void BuyItem(Item.ItemType itemType)
    {
        //Define how and what happens when player buys an item (potion for example from the shop)
        Debug.Log("Item Bought : " + itemType);
        switch (itemType)
        {
            case Item.ItemType.HealthPotion: OnHeal();
                break;
            case Item.ItemType.ManaPotion: OnRecoverMana();
                break;
        }
    }

    public bool SpendCoin(int coinAmount)
    {
        if (gameManager.inventoryscript.coins >= coinAmount)
        {
            gameManager.inventoryscript.coins -= coinAmount;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void OnSell()
    {
        //If the player can sell items, Define how and what happens when player sells an item (equipment if implementing the concept for example to the shop)
    }

    public void OnUsingMana(int mana)
    {
        GetCurrentMana -= mana;
    }

    public void OnRecoverMana()
    {
        if (GetCurrentMana < GetMaxMana)
        {
            GetCurrentMana = GetMaxMana;
        }
    }

    public void LoadData(GameData data)
    {
        if (!DataPersistenceManager.instance.newSceneLoading)
        {
            this.transform.position = data.playerPos;
        }
        //this.transform.position = data.playerPos;
        this.GetCurrentHP = data.hpData;
        this.GetCurrentMana = data.manaData;
        this.GetCurrentStamina = data.staminaData;
    }

    public void SaveData(GameData data)
    {
        data.playerPos = this.transform.position;
        data.hpData = this.GetCurrentHP;
        data.manaData = this.GetCurrentMana;
        data.staminaData = this.GetCurrentStamina;
    }

    public void SetCurrentWeapon(int index)
    {
        switch (index)
        {
            case 0:

                foreach (GameObject obj in _possesedWeapons)
                {
                    if (obj.name == _possesedWeapons[0].name)
                    {
                        obj.SetActive(true);
                    }
                    if (obj.name != _possesedWeapons[0].name)
                    {
                        obj.SetActive(false);
                    }
                }

                _currentMeleeDamage = 40;
                _currentSlashDamage = 20;
                break;

            case 1:

                foreach (GameObject obj in _possesedWeapons)
                {
                    if (obj.name == _possesedWeapons[1].name)
                    {
                        obj.SetActive(true);
                    }
                    if (obj.name != _possesedWeapons[1].name)
                    {
                        obj.SetActive(false);
                    }
                }
               
                _currentMeleeDamage = 50;
                _currentSlashDamage = 25;
                break;

            case 2:

                foreach (GameObject obj in _possesedWeapons)
                {
                    if (obj.name == _possesedWeapons[2].name)
                    {
                        obj.SetActive(true);
                    }
                    if (obj.name != _possesedWeapons[2].name)
                    {
                        obj.SetActive(false);
                    }
                }
                
                _currentMeleeDamage = 60;
                _currentSlashDamage = 30;
                break;

            case 3:

                foreach (GameObject obj in _possesedWeapons)
                {
                    if (obj.name == _possesedWeapons[3].name)
                    {
                        obj.SetActive(true);
                    }
                    if (obj.name != _possesedWeapons[3].name)
                    {
                        obj.SetActive(false);
                    }
                }
               
                _currentMeleeDamage = 30;
                _currentSlashDamage = 15;
                break;

            case 4:

                foreach (GameObject obj in _possesedWeapons)
                {
                    if (obj.name == _possesedWeapons[4].name)
                    {
                        obj.SetActive(true);
                    }
                    if (obj.name != _possesedWeapons[4].name)
                    {
                        obj.SetActive(false);
                    }
                }
                _currentMeleeDamage = 70;
                _currentSlashDamage = 35;
                break;

        }
    }

    #endregion

    #region Sliders

    private void OnManagingSliders()
    {
        OnManagingStamina(); // Manage the stamina bar

        OnManagingHealth();

        OnManagingMana();
    }
    private void OnManagingStamina()
    {
        //Barre d'endurance
        uiManager.StaminaText.GetComponent<TMPro.TextMeshProUGUI>().text = GetCurrentStamina.ToString("0") + " / " + GetMaxStamina.ToString();
        uiManager.StaminaBar.maxValue = GetMaxStamina;
        uiManager.StaminaBar.value = currentStamina;
        if (currentStamina >= GetMaxStamina) { currentStamina = maxStamina; } else if (currentStamina <= 0) { currentStamina = 0; }
    }

    private void OnManagingHealth()
    {
        uiManager.HpText.GetComponent<TMPro.TextMeshProUGUI>().text = GetCurrentHP.ToString("0") + " / " + GetMaxHP.ToString();
        uiManager.HpBar.maxValue = GetMaxHP;
        uiManager.HpBar.value = GetCurrentHP;
        if (GetCurrentHP >= GetMaxHP) { GetCurrentHP = GetMaxHP; }
        else if (GetCurrentHP <= 0)
        {
            GetCurrentHP = 0;
        }
    }

    private void OnManagingMana()
    {
        //Barre d'endurance
        uiManager.ManaText.GetComponent<TMPro.TextMeshProUGUI>().text = GetCurrentMana.ToString("0") + " / " + GetMaxMana.ToString();
        uiManager.ManaBar.maxValue = GetMaxMana;
        uiManager.ManaBar.value = GetCurrentMana;
        if (GetCurrentMana >= GetMaxMana) { GetCurrentMana = GetMaxMana; } else if (GetCurrentMana <= 0) { GetCurrentMana = 0; }
    }
    #endregion

    #region Gravity + Gounded

    private void OnManagingGravity()
    {
        OnApplyingGravity();
        IsPlayerGrounded();
    }

    private void OnApplyingGravity()
    {
        // Gravité
        velocity.y -= GravityForce; //Application de la force de gravité
        MyCharacter.Move(velocity * Time.deltaTime);

        if(!IsGrounded)
        {
            animator.SetBool("Grounded", true);
        }
        else
        {
            animator.SetBool("Grounded", false);
        }
    }

    private bool IsPlayerGrounded()
    {
        IsGrounded = Physics.CheckSphere(checkGroundSphere.transform.position, checkGroundDistance, groundLayerMask);

        return IsGrounded || MyCharacter.isGrounded;
    }


    #endregion

    #region Collisions and Triggers

    private void OnTriggerStay(Collider other) //OnPickingItem
    {
        if (other.gameObject != null)
        {
            //if (other.gameObject.GetComponent<Drops>())
            //{
            //    IsCollidingWithItem = true;
            //    if (IsPicking)
            //    {
            //        HasPickedItem = true;
            //        //playerEntityInstance.PickableText.GetComponent<TMPro.TextMeshProUGUI>().text = "Picked";
            //        //other.gameObject.SetActive(false);
            //        Destroy(other.gameObject);
            //    }
            //}

            if (other.gameObject.GetComponent<ProjectileManager>())
            {
                if (!CanReturnAttack)
                {
                    other.gameObject.SetActive(false);
                    other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
                    AttackToReturn = other.gameObject;
                    CanReturnAttack = true;
                    Debug.Log("Colliding");
                }
            }
        }
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject != null)
    //    {
    //        if (other.gameObject.GetComponent<Drops>())
    //        {
    //            IsCollidingWithItem = false;
    //        }
    //    }
    //}

    #endregion



}
