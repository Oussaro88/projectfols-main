using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAttackMeleeStart : StateAttack
{
    public StatePursue statePursue;
    public StateAttackMelee01 stateMelee01;
    public StateAttackRange01 stateRange01;
    public float playerDistance;
    private Vector3 target;
    public bool once = false;
    public Animator anim;
    public bool canDmg;
    public int randNum;

    public override EnemyState RunState(EnemyBehaviour enemyBehaviour)
    {
        /*if (!once)
        {
            enemyBehaviour.agent.SetDestination(enemyBehaviour.gameObject.transform.position);
            enemyBehaviour.enemyAnim.SetBool("IsWalking", false);
            once = true;
        }*/

        //If CurrentAnimation is Walk Anim. Trying to Prevent Player Hurt Before Attack Animation Start After Walking
        /*if(anim.GetCurrentAnimatorStateInfo(0).IsName("MWalking") || anim.GetCurrentAnimatorStateInfo(0).IsName("Knocked"))
        {
            if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9)
            {
                canDmg = true;
            }
        }*/

        //when Walk Anim is done
        //if (canDmg)
        //{
        //enemyBehaviour.GetComponent<EnemyMain>().OnAttack();
        //}

        //Look At Player
        /*target = new Vector3(enemyBehaviour.player.transform.position.x, enemyBehaviour.gameObject.transform.position.y, enemyBehaviour.player.transform.position.z);
        enemyBehaviour.gameObject.transform.LookAt(target);

        playerDistance = Vector3.Distance(transform.position, enemyBehaviour.player.transform.position);*/

        //----- ----- Condition To Go To Script StatePursue ----- -----
        /*if (enemyBehaviour.gameObject.GetComponent<EnemyMelee>())
        {
            if (playerDistance >= 4)
            {
                //enemyBehaviour.agent.isStopped = false;
                once = false;
                //onceTimer = false;
                canDmg = false;
                return statePursue;
            }
        }
        else if (enemyBehaviour.gameObject.GetComponent<EnemyRange>())
        {
            if (playerDistance >= 10)
            {
                //enemyBehaviour.agent.enabled = true;
                //enemyBehaviour.agent.isStopped = false;
                once = false;
                return statePursue;
            }
        }*/

        randNum = Random.Range(0, 3);
        switch (randNum)
        {
            case 2:
                return stateRange01;
            default:
                return stateMelee01;
        }

        //return this;
    }
}
