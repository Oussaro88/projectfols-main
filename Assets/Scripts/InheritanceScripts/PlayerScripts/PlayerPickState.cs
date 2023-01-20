using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickState : MonoBehaviour
{ 
    //private GameManager gameManager;

    //private PlayerEntity playerEntityInstance;
    //private PlayerStateMachine playerState;

    //public PlayerPickState(PlayerEntity playerEntity, PlayerStateMachine stateMachine)
    //{
    //    gameManager = GameManager.instance;
    //    this.playerEntityInstance = playerEntity;
    //    this.playerState = stateMachine;
    //}

    //private void PickingItem()
    //{
    //    if (playerEntityInstance.HasPickedItem)
    //    {
    //        playerEntityInstance.Speed = 0f;
    //        playerEntityInstance.Animator.SetBool("Pick", true);
    //        playerEntityInstance.Sword.SetActive(false);
    //        playerEntityInstance.TimeToWait += Time.deltaTime;
    //        if (playerEntityInstance.TimeToWait >= 2.2f)
    //        {
    //            playerEntityInstance.Animator.SetBool("Pick", false);
    //            playerEntityInstance.Sword.SetActive(true);
    //            playerEntityInstance.Speed = playerEntityInstance.ResetSpeedValue;
    //            playerEntityInstance.TimeToWait = 0f;
    //            playerEntityInstance.HasPickedItem = false;
    //            playerEntityInstance.playerState.ChangeState(playerEntityInstance.DefaultState);
    //        }
    //    }
    //}

    //public void EnterState()
    //{
    //    Debug.Log(GetType().Name);
    //}

    //public void ExitState()
    //{
    //    return;
    //}

    //public void OnUpdate()
    //{
    //    PickingItem();
    //}

}
