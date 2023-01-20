using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState
{ 
    //private GameManager gameManager;

    //private PlayerEntity playerEntityInstance;
    //private PlayerStateMachine playerState;

    //public PlayerJumpState(PlayerEntity playerEntity, PlayerStateMachine stateMachine)
    //{
    //    gameManager = GameManager.instance;
    //    this.playerEntityInstance = playerEntity;
    //    this.playerState = stateMachine;
    //}

    ////private void Jump()
    ////{
    ////    playerEntityInstance.velocity.y = playerEntityInstance.jumpForce; //Application de la force du saut
    ////    playerEntityInstance.Animator.SetBool("Jump", true);
    ////    playerEntityInstance.IsJumping = false;
    ////}

    //public void EnterState()
    //{
    //    Debug.Log(GetType().Name);
    //}

    //public void OnUpdate()
    //{
    //    if(playerEntityInstance.IsJumping)
    //    {
    //        Jump();
    //    }
    //    else
    //    {
    //        playerEntityInstance.Animator.SetBool("Jump", false);
    //        playerEntityInstance.Animator.SetBool("Fall", true);
    //        playerEntityInstance.playerState.ChangeState(playerEntityInstance.DefaultState);
    //    }

    //    if(playerEntityInstance.MyCharacter.isGrounded)
    //    {
    //        playerEntityInstance.Animator.SetBool("Fall", false);
    //    }
    //}

    //public void ExitState()
    //{
    //    return;
    //}
}
