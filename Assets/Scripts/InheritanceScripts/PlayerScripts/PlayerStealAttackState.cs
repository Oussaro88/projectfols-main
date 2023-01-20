using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStealAttackState : MonoBehaviour, IPlayerBaseState
{
    private GameManager gameManager;

    private PlayerEntity playerEntityInstance;
    private PlayerStateMachine playerState;

    public PlayerStealAttackState(PlayerEntity playerEntity, PlayerStateMachine stateMachine)
    {
        gameManager = GameManager.instance;
        Debug.Log(gameManager);
        this.playerEntityInstance = playerEntity;
        this.playerState = stateMachine;
    }

    #region ReturnAttack

    private void OnReturningAttack()
    {
        if (playerEntityInstance.CanReturnAttack)
        {

                playerEntityInstance.CanReturnAttack = false;
                playerEntityInstance.Animator.SetBool("Spell", true);
                GameObject gameObj = Instantiate(gameManager.bullet, playerEntityInstance.transform.position + playerEntityInstance.transform.forward * 2f, Quaternion.identity); //Instantiation du projectile
                gameObj.GetComponent<Rigidbody>().AddForce((playerEntityInstance.transform.forward * 2f) * playerEntityInstance.BulletVelocity, ForceMode.Impulse); //Application de la physique sur le projectile

                playerEntityInstance.hasReturnedAttack = true;
                Debug.Log(playerEntityInstance.hasReturnedAttack);

                if (playerEntityInstance.hasReturnedAttack)
                {
                    playerEntityInstance.ReturnFireIndex = 0;
                    playerEntityInstance.hasReturnedAttack = false;
                    playerEntityInstance.playerState.ChangeState(playerEntityInstance.DefaultState);
                }
        }
    }

    private void OnStealingAttack()
    {
        playerEntityInstance.isStealingAttack = true;
        playerEntityInstance.CapsuleCollider.enabled = true;
        if (playerEntityInstance.CapsuleCollider.enabled == true)
        {
            playerEntityInstance.Animator.SetBool("Spell", true);
            playerEntityInstance.Timercapsule += Time.deltaTime;
            if (playerEntityInstance.Timercapsule >= 1.4f)
            {
                playerEntityInstance.Animator.SetBool("Spell", false);
                playerEntityInstance.CapsuleCollider.enabled = false;
                playerEntityInstance.Timercapsule = 0f;
                playerEntityInstance.isStealingAttack = false;
                if(playerEntityInstance.CanReturnAttack)
                {
                    playerEntityInstance.ReturnFireIndex = 2;
                }
                playerEntityInstance.playerState.ChangeState(playerEntityInstance.DefaultState);
            }
        }
    }

    #endregion

    public void EnterState()
    {
        Debug.Log(GetType().Name);
    }

    public void ExitState()
    {
        return;
    }

    public void OnUpdate()
    {
        //Stealing Attack
        if (playerEntityInstance.IsReturningAttack && playerEntityInstance.ReturnFireIndex == 0) //&& !playerEntityInstance.CanReturnAttack)
        {
            playerEntityInstance.ReturnFireIndex = 1;
        }


        if (playerEntityInstance.ReturnFireIndex == 1)
        {
            OnStealingAttack();
        }


        //Returning Attack
        if (playerEntityInstance.IsReturningAttack && playerEntityInstance.ReturnFireIndex == 2)
        {
            Debug.Log("IS RETURNING ATTACK");
            OnReturningAttack();
        }




    }
}
