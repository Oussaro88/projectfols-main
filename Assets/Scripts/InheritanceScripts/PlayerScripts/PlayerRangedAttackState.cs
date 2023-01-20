using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRangedAttackState : MonoBehaviour, IPlayerBaseState
{
    private GameManager gameManager;

    private PlayerEntity playerEntityInstance;
    private PlayerStateMachine playerState;

    public PlayerRangedAttackState(PlayerEntity playerEntity, PlayerStateMachine stateMachine)
    {
        gameManager = GameManager.instance;
        this.playerEntityInstance = playerEntity;
        this.playerState = stateMachine;
    }

    private void RangedAttack()
    {
        playerEntityInstance.Animator.SetBool("Spell", true);
    }

    public void EnterState()
    {
        Debug.Log(GetType().Name);
        RangedAttack();

  
    }

    public void ExitState()
    {
        if (!playerEntityInstance.hasFired)
        {
            playerEntityInstance.Timer += Time.deltaTime; //lance le chrono
            if (playerEntityInstance.Timer >= playerEntityInstance.FireRate)
            {
                playerEntityInstance.hasFired = true;
                if (playerEntityInstance.GetCurrentMana >= 10f)
                {
                    playerEntityInstance.OnUsingMana(10);
                    GameObject gameObj = Instantiate(gameManager.fireSpell, new Vector3(playerEntityInstance.transform.position.x, playerEntityInstance.transform.position.y + 1.25f, playerEntityInstance.transform.position.z) + playerEntityInstance.transform.forward * 1.5f, Quaternion.identity); //Instantiation du projectile
                    gameObj.GetComponent<Rigidbody>().AddForce(playerEntityInstance.transform.forward * playerEntityInstance.BulletVelocity, ForceMode.Impulse); //Application de la physique sur le projectile
                    Destroy(gameObj, 5f); //Destruction du projectile

                    //GameObject obj = Instantiate(playerEntityInstance.newvfx, new Vector3(playerEntityInstance.transform.position.x + 3, playerEntityInstance.transform.position.y + 1.25f, playerEntityInstance.transform.position.z) + playerEntityInstance.transform.forward * 1.5f, Quaternion.identity); //Instantiation du projectile
                    //obj.GetComponent<Rigidbody>().AddForce(playerEntityInstance.transform.forward * playerEntityInstance.BulletVelocity, ForceMode.Impulse); //Application de la physique sur l
                    //Destroy(obj, 5f); //Destruction du projectile

                }
                playerEntityInstance.Timer = 0; //reset du chrono
                playerEntityInstance.playerState.ChangeState(playerEntityInstance.DefaultState);
            }
        }
    }

    public void OnUpdate()
    {
        ExitState();
    }

}
