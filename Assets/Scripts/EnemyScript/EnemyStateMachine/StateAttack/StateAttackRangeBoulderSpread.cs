using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAttackRangeBoulderSpread : StateAttackRange02
{
    public StateAttack stateWarrior;
    public StateWaiting stateWaiting;
    public bool isBoss = false;

    public GameObject ranged2;
    public GameObject ranged3;
    public GameObject projectileSpawn2;
    public GameObject projectileSpawn3;

    public bool isShootingPooling = false;
    public bool iSPOnce1 = false;
    public bool iSPOnce2 = false;
    public bool isShootingInstant = false;
    public bool iSIOnce1 = false;
    public bool iSIOnce2 = false;

    public bool multiShot = false;

    public bool coolDown;
    public float coolTime;

    public float timer2 = 0;

    public override EnemyState RunState(EnemyBehaviour enemyBehaviour)
    {
        rangeDistance = Vector3.Distance(rangePos, transform.position);
        playerDistance = Vector3.Distance(transform.position, enemyBehaviour.player.transform.position);

        //Go to a Random Position for Range Attack
        /*if (!once1)
        {
            //randomX = Random.Range(enemyBehaviour.boundBox.center.x - enemyBehaviour.boundBox.extents.x + enemyBehaviour.agent.radius,
            //        enemyBehaviour.boundBox.center.x + enemyBehaviour.boundBox.extents.x - enemyBehaviour.agent.radius);
            //randomZ = Random.Range(enemyBehaviour.boundBox.center.z - enemyBehaviour.boundBox.extents.z + enemyBehaviour.agent.radius,
            //    enemyBehaviour.boundBox.center.z + enemyBehaviour.boundBox.extents.z - enemyBehaviour.agent.radius);
            //rangePos = new Vector3(randomX, transform.position.y, randomZ);

            enemyBehaviour.agent.SetDestination(rangePos);
            enemyBehaviour.agent.isStopped = false;
            once1 = true;
        }*/

        //Shoot Animation
        if (!once2)
        {
            enemyBehaviour.agent.SetDestination(enemyBehaviour.gameObject.transform.position);
            enemyBehaviour.enemyAnim.SetBool("IsWalking", false);
            once2 = true;
            once3 = false;
            once4 = true;
            iSPOnce1 = false;
            iSPOnce2 = false;
            iSIOnce1 = false;
            iSIOnce2 = false;
            timer = 0;
            timer2 = 0;
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
                if (isPooling)
                {
                    isShootingPooling = true;
                }
                else
                {
                    isShootingInstant = true;
                }

                once3 = true;
            }
        }

        if(isShootingPooling)
        {
            timer2 += Time.deltaTime;

            if (multiShot)
            {
                if (!iSPOnce1)
                {
                    ranged = enemyBehaviour.poolingManager.callBoulderThrow();
                    ranged.SetActive(true);
                    ranged.transform.position = projectileSpawn.transform.position;
                    ranged.transform.rotation = projectileSpawn.transform.rotation;
                    //ranged.GetComponent<BaseProjectile>().dmg = 15;
                    ranged.GetComponent<BaseProjectile>().useRange = true;
                    ranged.GetComponent<BaseProjectile>().isPooling = true;
                    ranged.GetComponent<Rigidbody>().AddForce(projectileSpawn.transform.forward * 20, ForceMode.Impulse);
                    iSPOnce1 = true;
                }

                if (timer2 >= 0.5f && !iSPOnce2)
                {
                    ranged2 = enemyBehaviour.poolingManager.callBoulderThrow();
                    ranged2.SetActive(true);
                    ranged2.transform.position = projectileSpawn2.transform.position;
                    ranged2.transform.rotation = projectileSpawn2.transform.rotation;
                    //ranged2.GetComponent<BaseProjectile>().dmg = 15;
                    ranged2.GetComponent<BaseProjectile>().useRange = true;
                    ranged2.GetComponent<BaseProjectile>().isPooling = true;
                    ranged2.GetComponent<Rigidbody>().AddForce(projectileSpawn2.transform.forward * 20, ForceMode.Impulse);
                    iSPOnce2 = true;
                }

                if (timer2 >= 1f)
                {
                    ranged3 = enemyBehaviour.poolingManager.callBoulderThrow();
                    ranged3.SetActive(true);
                    ranged3.transform.position = projectileSpawn3.transform.position;
                    ranged3.transform.rotation = projectileSpawn3.transform.rotation;
                    //ranged3.GetComponent<BaseProjectile>().dmg = 15;
                    ranged3.GetComponent<BaseProjectile>().useRange = true;
                    ranged3.GetComponent<BaseProjectile>().isPooling = true;
                    ranged3.GetComponent<Rigidbody>().AddForce(projectileSpawn3.transform.forward * 20, ForceMode.Impulse);
                    timer2 = 0;
                    iSPOnce1 = false;
                    iSPOnce2 = false;
                    once4 = false;
                    isShootingPooling = false;
                }
            } 
            else //SingleShot
            {
                ranged = enemyBehaviour.poolingManager.callBoulderThrow();
                ranged.SetActive(true);
                ranged.transform.position = projectileSpawn.transform.position;
                ranged.transform.rotation = projectileSpawn.transform.rotation;
                //ranged.GetComponent<BaseProjectile>().dmg = 15;
                ranged.GetComponent<BaseProjectile>().useRange = true;
                ranged.GetComponent<BaseProjectile>().isPooling = true;
                ranged.GetComponent<Rigidbody>().AddForce(projectileSpawn.transform.forward * 20, ForceMode.Impulse);
                once4 = false;
                isShootingPooling = false;
            }
        }

        if (isShootingInstant)
        {
            timer2 += Time.deltaTime;

            if (multiShot)
            {
                if (!iSIOnce1)
                {
                    ranged = Instantiate(projectile, projectileSpawn.transform.position, projectileSpawn.transform.rotation);
                    //ranged.GetComponent<BaseProjectile>().dmg = 15;
                    ranged.GetComponent<Rigidbody>().AddForce(projectileSpawn.transform.forward * 20, ForceMode.Impulse);
                    iSIOnce1 = true;
                }

                if (timer2 >= 0.5f && !iSIOnce2)
                {
                    ranged2 = Instantiate(projectile, projectileSpawn2.transform.position, projectileSpawn2.transform.rotation);
                    //ranged2.GetComponent<BaseProjectile>().dmg = 15;
                    ranged2.GetComponent<Rigidbody>().AddForce(projectileSpawn2.transform.forward * 20, ForceMode.Impulse);
                    iSIOnce2 = true;
                }

                if (timer2 >= 1f)
                {
                    ranged3 = Instantiate(projectile, projectileSpawn3.transform.position, projectileSpawn3.transform.rotation);
                    //ranged3.GetComponent<BaseProjectile>().dmg = 15;
                    ranged3.GetComponent<Rigidbody>().AddForce(projectileSpawn3.transform.forward * 20, ForceMode.Impulse);
                    timer2 = 0;
                    iSIOnce1 = false;
                    iSIOnce2 = false;
                    once4 = false;
                    isShootingInstant = false;
                }
            }
            else //SingleShot
            {
                ranged = Instantiate(projectile, projectileSpawn.transform.position, projectileSpawn.transform.rotation);
                //ranged.GetComponent<BaseProjectile>().dmg = 15;
                ranged.GetComponent<Rigidbody>().AddForce(projectileSpawn.transform.forward * 20, ForceMode.Impulse);
                once4 = false;
                isShootingInstant = false;
            }
        }

        //When Shoot Animation almost End
        if (/*anim.GetCurrentAnimatorStateInfo(0).IsName("Spell") &&*/ !once4)  //May not need anim.getcurrent...
        {
            //if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1) //0.9
            //{
                //enemyBehaviour.agent.SetDestination(enemyBehaviour.gameObject.transform.position);
                once1 = false;
                once2 = false;
                once3 = false;
                once4 = true;

            /*if (combo && stateRange02)
            {
                randNum = Random.Range(0, 3);
                switch (randNum)
                {
                    case 2:
                        //Debug.Log("Pursuing 2 " + this.gameObject);
                        return statePursue;
                    default:
                        //Debug.Log("Shooting 0-1 " + randNum + " " + this.gameObject);
                        stateRange02.once4 = true;
                        enemyBehaviour.enemyAnim.SetBool("IsWalking", true);
                        //stateRange02.once1 = true;
                        //stateRange02.rangeDistance = 0;
                        return stateRange02;
                }
            }
            else
            {*/
            if (coolDown)
            {
                stateWaiting.magicCoolDown = true;
                stateWaiting.coolTime = coolTime;
                return stateWaiting;
            }
            else if (isBoss)
            {
                return stateWarrior;
            }
            else
            {
                enemyBehaviour.enemyAnim.SetBool("IsWalking", true);
                return statePursue;
            }
            //}
            //}
        }

        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Spell"))
        {
            timer += Time.deltaTime;

            if (timer >= 1f)
            {
                timer = 0;
                once2 = false;
            }
        }

        return this;
    }
}
