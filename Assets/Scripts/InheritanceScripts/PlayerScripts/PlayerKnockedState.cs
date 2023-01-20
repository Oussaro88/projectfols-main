using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKnockedState : IPlayerBaseState
{
    private GameManager gameManager;
    private UIManager uiManager;

    private PlayerEntity playerEntityInstance;
    private PlayerStateMachine playerState;

    public PlayerKnockedState(PlayerEntity playerEntity, PlayerStateMachine stateMachine)
    {
        gameManager = GameManager.instance;
        uiManager = UIManager.Instance;
        this.playerEntityInstance = playerEntity;
        this.playerState = stateMachine;
    }


    private void KnockedAnimation()
    {
        playerEntityInstance.Animator.SetBool("Attack", false);
        playerEntityInstance.meleeTime = 0f;
        uiManager.SwordImage.SetActive(true);
        playerEntityInstance.Animator.SetBool("Knocked", false);
        //playerEntityInstance.hasBeenKnocked = true;
        playerEntityInstance.playerState.ChangeState(playerEntityInstance.DefaultState);
    }

    public void EnterState()
    {
        Debug.Log(GetType().Name);
        //playerEntityInstance.isKnocked = false;
    }

    public void ExitState()
    {
        return;
    }

    public void OnUpdate()
    {
        KnockedAnimation();
    }

}
