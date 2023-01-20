using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAttackMelee01 : EnemyState
{
    public StatePursue statePursue;
    public StateAttackMelee02 stateMelee02;
    public StateAttackRange01 stateRange01;
    public StateAttack stateWarrior;
    public bool isBoss = false;
    private Vector3 target;
    public bool once1 = false;
    public bool once2 = false;
    public bool once3 = false;
    public float playerDistance = 0;
    public Animator anim;
    public int randNum;
    public float damage;
    public float timer = 0;

    public override EnemyState RunState(EnemyBehaviour enemyBehaviour)
    {
        playerDistance = Vector3.Distance(transform.position, enemyBehaviour.player.transform.position);

        //Follow Player
        if (!once1)
        {
            enemyBehaviour.agent.SetDestination(enemyBehaviour.player.transform.position);
        }

        //Look At Player and Attack Animation
        if (playerDistance <= 2f && !once2)
        {
            target = new Vector3(enemyBehaviour.player.transform.position.x, enemyBehaviour.gameObject.transform.position.y, enemyBehaviour.player.transform.position.z);
            enemyBehaviour.gameObject.transform.LookAt(target);
            enemyBehaviour.agent.SetDestination(enemyBehaviour.gameObject.transform.position);
            enemyBehaviour.enemyAnim.SetBool("IsWalking", false);
            once1 = true;
            once2 = true;
            once3 = false;
            timer = 0;
            anim.SetTrigger("IsAttacking01");
        }

        //When Attack Animation almost Start
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("MSword And Shield Slash") && !once3)
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
        }

        //When Attack Animation almost End
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("MSword And Shield Slash"))
        {
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                enemyBehaviour.agent.SetDestination(enemyBehaviour.gameObject.transform.position);

                if (enemyBehaviour.gameObject.GetComponent<EnemyMelee>())
                {
                    enemyBehaviour.gameObject.GetComponent<EnemyMelee>().CannotDamage();
                }

                if (enemyBehaviour.gameObject.GetComponent<EnemyBossWarrior>())
                {
                    enemyBehaviour.gameObject.GetComponent<EnemyBossWarrior>().CannotDamage();
                }

                once1 = false;
                once2 = false;
                once3 = false;

                if (isBoss)
                {
                    return stateWarrior;
                }
                else
                {
                    //enemyBehaviour.enemyAnim.SetBool("IsWalking", true);
                    randNum = Random.Range(0, 3);
                    switch (randNum)
                    {
                        case 2:
                            return stateMelee02;
                        default:
                            enemyBehaviour.enemyAnim.SetBool("IsWalking", true);
                            return statePursue;
                    }
                }
            }
        }

        if (playerDistance >= 7 && isBoss)
        {
            enemyBehaviour.gameObject.GetComponent<EnemyMelee>().CannotDamage();
            once1 = false;
            once2 = false;
            once3 = false;
            return stateWarrior;
        }

        if (playerDistance >= 7 && !isBoss)
        {
            enemyBehaviour.gameObject.GetComponent<EnemyMelee>().CannotDamage();
            once1 = false;
            once2 = false;
            once3 = false;

            return stateRange01;
        }

        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("MSword And Shield Slash") && !anim.GetCurrentAnimatorStateInfo(0).IsName("MWalking"))
        {
            timer += Time.deltaTime;

            if (timer >= 1f)
            {
                timer = 0;
                anim.SetBool("IsWalking", true);
                once1 = false;
                once2 = false;
            }
        }

        return this;
    }
}
