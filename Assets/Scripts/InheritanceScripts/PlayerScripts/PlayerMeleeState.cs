using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeState : IPlayerBaseState
{
    private GameManager gameManager;
    private UIManager uiManager;

    private PlayerEntity playerEntityInstance;
    private PlayerStateMachine playerState;



    public PlayerMeleeState(PlayerEntity playerEntity, PlayerStateMachine stateMachine)
    {
        gameManager = GameManager.instance;
        uiManager = UIManager.Instance;
        this.playerEntityInstance = playerEntity;
        this.playerState = stateMachine;
    }

    public void MeleeAttack() //changed to public
    {
        uiManager.SwordImage.SetActive(false);
        //playerEntityInstance.meleePS.SetActive(true);
        playerEntityInstance.MyCharacter.Move(playerEntityInstance.MeleeVelocity * playerEntityInstance.meleeSpeed * Time.deltaTime);
        playerEntityInstance.HasUsedMelee = true;
        playerEntityInstance.Animator.SetBool("Attack", true);
        gameManager.meleeHasBeenUsed = true;
    }

    public void EnterState()
    {
        Debug.Log(GetType().Name);
        MeleeAttack();
    }

    public void ExitState()
    {
        playerEntityInstance.meleeTime += Time.deltaTime;
        if (playerEntityInstance.meleeTime >= 0.45f)
        {
            playerEntityInstance.Animator.SetBool("Attack", false);
            //playerEntityInstance.HasUsedMelee = false;
            playerEntityInstance.meleeTime = 0f;
            uiManager.SwordImage.SetActive(true);
            //playerEntityInstance.meleePS.SetActive(false);
            //gameManager.testTimer = 0f;
            playerEntityInstance.playerState.ChangeState(playerEntityInstance.DefaultState);
        }
    }

    public void OnUpdate()
    {
        ExitState();
        
        if (playerEntityInstance.isKnocked)
        {
            playerEntityInstance.Animator.SetBool("Attack", false);
            playerEntityInstance.playerState.ChangeState(playerEntityInstance.KnockedState);
        }

    }
}
