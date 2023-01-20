using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAttackMeleeCharge : EnemyState
{
    public StatePursue statePursue;
    public StateAttack stateWarrior;
    public bool isBoss = false;
    //private Vector3 target;
    public bool once1 = false;
    public bool once2 = false;
    //public bool once3 = false;
    public float playerDistance = 0;
    public float chargeDistance = 0;
    public Animator anim;
    public float damage;
    public GameObject pathPoint;
    public int playerSensorID;
    //public GameObject warningZone;

    public override EnemyState RunState(EnemyBehaviour enemyBehaviour)
    {
        //playerDistance = Vector3.Distance(transform.position, enemyBehaviour.player.transform.position);

        //Choose Destination and Call Animation for Charge
        if (!once1)
        {
            pathPoint = enemyBehaviour.pathManager.CallPathPointCharge();
            enemyBehaviour.agent.SetDestination(pathPoint.transform.position);
            anim.SetBool("isRunning", true);
            once1 = true;
        }

        chargeDistance = Vector3.Distance(transform.position, pathPoint.transform.position);

        //Attack Animation when reached Destination
        if (chargeDistance <= 2f && !once2)
        {
            enemyBehaviour.agent.SetDestination(enemyBehaviour.gameObject.transform.position);
            enemyBehaviour.enemyAnim.SetBool("IsRunning", false);
            once2 = true;
            anim.SetTrigger("IsAttacking02"); 
            
            if (enemyBehaviour.gameObject.GetComponent<EnemyBossWarrior>())
            {
                enemyBehaviour.gameObject.GetComponent<EnemyBossWarrior>().CanDamage();
            }
        }

        /*if (anim.GetCurrentAnimatorStateInfo(0).IsName("Sword And Shield Attack") && !once3)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
            {
                enemyBehaviour.agent.SetDestination(enemyBehaviour.gameObject.transform.position);

                if (enemyBehaviour.gameObject.GetComponent<EnemyMelee>())
                {
                    enemyBehaviour.gameObject.GetComponent<EnemyMelee>().CanDamage();
                }

                if (enemyBehaviour.gameObject.GetComponent<EnemyBossWarrior>())
                {
                    enemyBehaviour.gameObject.GetComponent<EnemyBossWarrior>().CanDamage();
                }

                //enemyBehaviour.GetComponent<EnemyMain>().OnAttack(); //Remove anim.SetTriggger in EnemyMain
                once3 = true;
            }
        }*/

        //When Attack Animation almost End
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Sword And Shield Attack"))
        {
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                enemyBehaviour.agent.SetDestination(enemyBehaviour.gameObject.transform.position);

                /*if (enemyBehaviour.gameObject.GetComponent<EnemyMelee>())
                {
                    enemyBehaviour.gameObject.GetComponent<EnemyMelee>().CannotDamage();
                }*/

                if (enemyBehaviour.gameObject.GetComponent<EnemyBossWarrior>())
                {
                    enemyBehaviour.gameObject.GetComponent<EnemyBossWarrior>().CannotDamage();
                }

                once1 = false;
                once2 = false;
                //once3 = false;

                if (isBoss)
                {
                    return stateWarrior;
                }
                else
                {
                    enemyBehaviour.enemyAnim.SetBool("IsWalking", true);
                    return statePursue;
                }
            }
        }

        /*if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Spell")) Need to check how to do it with two anims
        {
            enemyBehaviour.enemyAnim.SetBool("IsWalking", true);
            once1 = false;
        }*/

        return this;
    }
}