using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlashState : IPlayerBaseState
{
    private GameManager gameManager;

    private PlayerEntity playerEntityInstance;
    private PlayerStateMachine playerState;

    public PlayerSlashState(PlayerEntity playerEntity, PlayerStateMachine stateMachine)
    {
        gameManager = GameManager.instance;
        this.playerEntityInstance = playerEntity;
        this.playerState = stateMachine;
    }

    private void Slash()
    {
        playerEntityInstance.MyCharacter.Move(playerEntityInstance.SlashVelocity * playerEntityInstance.slashSpeed * Time.deltaTime);
        playerEntityInstance.Animator.SetBool("Slash", true);
        playerEntityInstance.hasRequestedSlash = true;
        gameManager.slashHasBeenUsed = true;
    }

    public void EnterState()
    {
        Debug.Log(GetType().Name); 
        Slash();
    }

    public void ExitState()
    {
        playerEntityInstance.slashTimer += Time.deltaTime;
        if (playerEntityInstance.slashTimer >= 0.75f)
        {
            playerEntityInstance.Animator.SetBool("Slash", false);
            playerEntityInstance.slashTimer = 0f;
            //gameManager.slashHasBeenUsed = false;
            playerEntityInstance.playerState.ChangeState(playerEntityInstance.DefaultState);
        }
    }

    public void OnUpdate()
    {
        ExitState();
    }
}
