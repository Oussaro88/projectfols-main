using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDefaultState : IPlayerBaseState
{

    private GameManager gameManager;
    private UIManager uiManager;
    private PlayerEntity playerEntityInstance;
    private PlayerStateMachine playerState;


    public PlayerDefaultState(PlayerEntity playerEntity, PlayerStateMachine stateMachine)
    {
        gameManager = GameManager.instance;
        uiManager = UIManager.Instance;
        this.playerEntityInstance = playerEntity;
        this.playerState = stateMachine;
    }

    private void Move()
    {
        playerEntityInstance.Animator.SetFloat("Speed", 0f, 150f, Time.time);

        //  Mouvement du joueur
        playerEntityInstance.Move = new Vector3(playerEntityInstance.XAxis, 0, playerEntityInstance.ZAxis); //Update des coordonnées d'emplacement du vecteur
        playerEntityInstance.Move = Quaternion.Euler(0, playerEntityInstance.Cam.transform.eulerAngles.y, 0) * playerEntityInstance.Move; //Décale le vecteur de déplacement en fonction de la l'axe y de la caméra 

        if (playerEntityInstance.Move != Vector3.zero) //Quand j'utilise mon input
        {
            //Debug.Log(move);
            playerEntityInstance.IsMoving = true;
            playerEntityInstance.MyCharacter.transform.forward = playerEntityInstance.Move * Time.deltaTime; //Oriente le joueur vers la direction du mouvement
            if(playerEntityInstance.Move.magnitude  <= 0.5)
            {
                playerEntityInstance.Animator.SetFloat("Speed", 0.5f, 20f, Time.time);
            }
            else
            {
                playerEntityInstance.Animator.SetFloat("Speed", 1f, 20f, Time.time);
            }
        }
        else 
        { 
            playerEntityInstance.IsMoving = false; 
        }

        playerEntityInstance.MyCharacter.Move(playerEntityInstance.Move * playerEntityInstance.Speed * Time.deltaTime); //Déplace le joueur
    }


    public void EnterState()
    {
        Debug.Log(GetType().Name);
        //playerEntityInstance.hasExecutedDodge = false;
    }

    public void ExitState()
    {
        //DODGE
        if (playerEntityInstance.IsDodging && playerEntityInstance.IsGrounded && playerEntityInstance.GetCurrentStamina >= 20f && playerEntityInstance.Move != Vector3.zero) //DONE
        {
            playerEntityInstance.GetCurrentStamina -= 20f;
            playerEntityInstance.DodgeVelocity = playerEntityInstance.Move;
            gameManager.inputManager.OnDisable();
            playerEntityInstance.playerState.ChangeState(playerEntityInstance.DodgeState);
        }

        //SHIELD
        if (playerEntityInstance.IsUsingShield && playerEntityInstance.IsGrounded && playerEntityInstance.shieldTimer >= 5) //DONE
        {
            if (playerEntityInstance.GetCurrentMana >= 10)
            {
                playerEntityInstance.OnUsingMana(10);
                uiManager.ShieldImage.SetActive(false);
                playerEntityInstance.playerState.ChangeState(playerEntityInstance.BlockState);
            }
        }

        //FIRE
        if (playerEntityInstance.IsFiring && playerEntityInstance.IsGrounded && playerEntityInstance.GetCurrentMana >= 10f) //DONE
        {
            playerEntityInstance.playerState.ChangeState(playerEntityInstance.RangedAttackState);
        }

        //MELEE
        if (playerEntityInstance.IsUsingMelee && playerEntityInstance.IsGrounded && playerEntityInstance.GetCurrentStamina >= 50f)
        {
            playerEntityInstance.GetCurrentStamina -= 50f;
            playerEntityInstance.MeleeVelocity = playerEntityInstance.Move;
            gameManager.inputManager.OnDisable();
            playerEntityInstance.playerState.ChangeState(playerEntityInstance.MeleeState);
        }

        //SLASH
        if (playerEntityInstance.IsSlashing && playerEntityInstance.IsGrounded)
        {
            playerEntityInstance.SlashVelocity = playerEntityInstance.Move;
            gameManager.inputManager.OnDisable();
            playerEntityInstance.playerState.ChangeState(playerEntityInstance.SlashState);
        }

        //KNOCKED
        if (playerEntityInstance.isKnocked)
        {
            gameManager.inputManager.OnDisable();
            playerEntityInstance.playerState.ChangeState(playerEntityInstance.KnockedState);
        }

        //DEATH
        if (playerEntityInstance.GetCurrentHP <= 0)
        {
            playerEntityInstance.playerState.ChangeState(playerEntityInstance.DeathState);
        }
    }

    private void ReEnableinput()
    {
        //DODGE
        if (playerEntityInstance.hasExecutedDodge)
        {
            playerEntityInstance.resetDodgeInputTimer += Time.deltaTime;
            if (playerEntityInstance.resetDodgeInputTimer >= 0.6f)
            {
                gameManager.inputManager.OnEnable();
                playerEntityInstance.resetDodgeInputTimer = 0f;
                playerEntityInstance.hasExecutedDodge = false;
            }
        }

        if (!playerEntityInstance.hasExecutedDodge)
        {
            playerEntityInstance.resetDodgeInputTimer += Time.deltaTime;
            if (playerEntityInstance.resetDodgeInputTimer >= 0.35f)
            {
                gameManager.inputManager.OnEnable();
                //playerEntityInstance.resetDodgeInputTimer = 0f;
            }
        }


        if (playerEntityInstance.HasUsedMelee)
        {
            playerEntityInstance.resetMeleeInputTimer += Time.deltaTime;
            if (playerEntityInstance.resetMeleeInputTimer >= 0.5f)
            {
                gameManager.inputManager.OnEnable();
                playerEntityInstance.HasUsedMelee = false;
                playerEntityInstance.resetMeleeInputTimer = 0f;
            }
        }

        if (playerEntityInstance.hasRequestedSlash)
        {
            playerEntityInstance.resetSlashInputTimer += Time.deltaTime;
            if (playerEntityInstance.resetSlashInputTimer >= 1f)
            {
                playerEntityInstance.hasRequestedSlash = false;
                playerEntityInstance.resetSlashInputTimer = 0f;
                gameManager.inputManager.OnEnable();
            }
        }


        if (playerEntityInstance.isKnocked)
        {
            playerEntityInstance.resetKnockedInputTimer += Time.deltaTime;
            if (playerEntityInstance.resetKnockedInputTimer >= 0.35f)
            {
                playerEntityInstance.isKnocked = false;

                playerEntityInstance.resetKnockedInputTimer = 0f;
                //playerEntityInstance.hasBeenKnocked = false;
                gameManager.inputManager.OnEnable();
            }
        }
    }


    public void OnUpdate()
    {

        Move();

        ExitState();

        ReEnableinput();

        if (playerEntityInstance.hasFired)
        {
            playerEntityInstance.Animator.SetBool("Spell", false);
            playerEntityInstance.hasFired = false;
        }

        if (playerEntityInstance.hasBlockedAttack)
        {
            playerEntityInstance.BlockCoolDown -= Time.deltaTime;
            if (playerEntityInstance.BlockCoolDown <= 0f)
            {
                playerEntityInstance.hasBlockedAttack = false;
                playerEntityInstance.BlockCoolDown = 10f;
                playerEntityInstance.shieldTimer = 5f;

                if (playerEntityInstance.BlockCoolDown == 10)
                {
                    uiManager.ShieldImage.SetActive(true);
                }
            }
        }

    }
}
