using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAttackRangeLance : StateAttackRange02
{
    public override EnemyState RunState(EnemyBehaviour enemyBehaviour)
    {
        rangeDistance = Vector3.Distance(rangePos, transform.position);
        playerDistance = Vector3.Distance(transform.position, enemyBehaviour.player.transform.position);

        //Go to a Random Position for Range Attack
        if (!once1)
        {
            randomX = Random.Range(enemyBehaviour.boundBox.center.x - enemyBehaviour.boundBox.extents.x + enemyBehaviour.agent.radius,
                    enemyBehaviour.boundBox.center.x + enemyBehaviour.boundBox.extents.x - enemyBehaviour.agent.radius);
            randomZ = Random.Range(enemyBehaviour.boundBox.center.z - enemyBehaviour.boundBox.extents.z + enemyBehaviour.agent.radius,
                enemyBehaviour.boundBox.center.z + enemyBehaviour.boundBox.extents.z - enemyBehaviour.agent.radius);
            rangePos = new Vector3(randomX, transform.position.y, randomZ);

            enemyBehaviour.agent.SetDestination(rangePos);
            enemyBehaviour.agent.isStopped = false;
            once1 = true;
            once2 = false;
            once3 = false;
            once4 = true;
            timer = 0;
        }

        //Shoot Animation
        if ((playerDistance >= 7f || playerDistance <= 3f || rangeDistance <= enemyBehaviour.agent.stoppingDistance) && !once2)
        {
            enemyBehaviour.agent.SetDestination(enemyBehaviour.gameObject.transform.position);
            enemyBehaviour.enemyAnim.SetBool("IsWalking", false);
            once2 = true;
            anim.SetTrigger("IsThrowing");
        }

        //Look At Player
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Spell") && once2)
        {
            target = new Vector3(enemyBehaviour.player.transform.position.x, enemyBehaviour.gameObject.transform.position.y, enemyBehaviour.player.transform.position.z);
            enemyBehaviour.gameObject.transform.LookAt(target);
        }

        //When Shoot Animation reached Halfway (Hand goes Forward to Shoot)
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Spell") && !once3)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
            {
                once4 = false;
                //enemyBehaviour.agent.SetDestination(enemyBehaviour.gameObject.transform.position); 
                //enemyBehaviour.GetComponent<EnemyMain>().OnAttack();

                // Code for launching Lance
                if (isPooling)
                {
                    ranged = enemyBehaviour.poolingManager.callRangeLance();
                    ranged.SetActive(true);
                    ranged.transform.position = projectileSpawn.transform.position;
                    ranged.transform.rotation = projectileSpawn.transform.rotation;
                    ranged.GetComponent<BaseProjectile>().dmg = 50;
                    ranged.GetComponent<BaseProjectile>().useRange = true;
                    ranged.GetComponent<BaseProjectile>().isPooling = true;
                    ranged.GetComponent<Rigidbody>().AddForce(transform.forward * 12, ForceMode.Impulse);
                }
                else
                {
                    ranged = Instantiate(projectile, projectileSpawn.transform.position, projectileSpawn.transform.rotation);
                    ranged.GetComponent<BaseProjectile>().dmg = 30;
                    ranged.GetComponent<Rigidbody>().AddForce(transform.forward * 12, ForceMode.Impulse);
                }

                once3 = true;
            }
        }

        //When Shoot Animation almost End
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Spell") && !once4)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1) //0.9
            {
                //enemyBehaviour.agent.SetDestination(enemyBehaviour.gameObject.transform.position);
                once1 = false;
                once2 = false;
                once3 = false;
                once4 = true;

                if (combo && stateRange02)
                {
                    randNum = Random.Range(0, 3);
                    switch (randNum)
                    {
                        case 2:
                            enemyBehaviour.enemyAnim.SetBool("IsWalking", true);
                            //stateRange02.once1 = true;
                            //stateRange02.rangeDistance = 0;
                            return stateRange02;
                        default:
                            return statePursue;
                    }
                }
                else
                {
                    return statePursue;
                }
            }
        }

        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Spell") && !anim.GetCurrentAnimatorStateInfo(0).IsName("MWalking"))
        {
            timer += Time.deltaTime;

            if (timer >= 1f)
            {
                timer = 0;
                anim.SetBool("IsWalking", true);
                once1 = false;
            }
        }

        return this;
    }
}