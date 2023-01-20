using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlockState : MonoBehaviour, IPlayerBaseState
{
    private GameManager gameManager;

    private PlayerEntity playerEntityInstance;
    private PlayerStateMachine playerState;



    public PlayerBlockState(PlayerEntity playerEntity, PlayerStateMachine stateMachine)
    {
        gameManager = GameManager.instance;
        this.playerEntityInstance = playerEntity;
        this.playerState = stateMachine;
    }

    private void UseShield()
    {
        playerEntityInstance.vfxCube.SetActive(true);
        playerEntityInstance.Animator.SetBool("Block", true);
        playerEntityInstance.Speed = 0f;
    }

    public void EnterState()
    {
        Debug.Log(GetType().Name);
        UseShield();
    }

    public void ExitState()
    {
        playerEntityInstance.shieldTimer -= Time.deltaTime;
        if (playerEntityInstance.shieldTimer < 0)
        {
            playerEntityInstance.IsUsingShield = false;
        }

        if (!playerEntityInstance.IsUsingShield)
        {
            playerEntityInstance.hasBlockedAttack = true;
            playerEntityInstance.Speed = playerEntityInstance.ResetSpeedValue;
            playerEntityInstance.Animator.SetBool("Block", false);
            playerEntityInstance.vfxCube.SetActive(false);
            playerEntityInstance.playerState.ChangeState(playerEntityInstance.DefaultState);
        }
    }

    public void OnUpdate()
    {
        ExitState();
    }


}
