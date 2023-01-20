using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAttackMagicBoulderFall : StateAttackMagic
{
    //public GameObject pathPoint;
    public GameObject[] pathPointList = new GameObject[12];
    public int foundPlayerID = 0;
    public int count = 0;
    public bool isRandom;

    public override EnemyState RunState(EnemyBehaviour enemyBehaviour)
    {
        //Look At Player
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Spell") && once1)
        {
            target = new Vector3(enemyBehaviour.player.transform.position.x, enemyBehaviour.gameObject.transform.position.y, enemyBehaviour.player.transform.position.z);
            enemyBehaviour.gameObject.transform.LookAt(target);
        }

        //Play Animation
        if (!once1)
        {
            enemyBehaviour.agent.SetDestination(enemyBehaviour.gameObject.transform.position);
            enemyBehaviour.enemyAnim.SetBool("IsWalking", false);
            once1 = true;
            once2 = false;
            once3 = false;
            once4 = true;
            timer = 0;
            count = 0;
            anim.SetTrigger("IsThrowing");
            /*if (signMat)
            {
                character.GetComponent<SkinnedMeshRenderer>().material = signMat;
            }*/
        }

        //When Animation reached Halfway
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Spell") && !once2)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
            {
                once4 = false;
                /*if (defaultMat)
                {
                    character.GetComponent<SkinnedMeshRenderer>().material = defaultMat;
                }*/
                //enemyBehaviour.agent.SetDestination(enemyBehaviour.gameObject.transform.position); 
                //enemyBehaviour.GetComponent<EnemyMain>().OnAttack();

                // Code for casting spell
                if (isPooling)
                {
                    count = 0;
                    foundPlayerID = enemyBehaviour.sensorManager.CallSensorResultPlayer();
                    //magic = enemyBehaviour.poolingManager.callBoulderFall();
                    if (isRandom)
                    {
                        SpawnSpellFall(enemyBehaviour, true, pathPointList.Length);
                    }
                    else //Focus Attack
                    {
                        if(foundPlayerID < 4)
                        {
                            SpawnSpellFall(enemyBehaviour, false, 9);
                        } 
                        else if (foundPlayerID > 4)
                        {
                            SpawnSpellFall(enemyBehaviour, false, 6);
                        }
                        else if (foundPlayerID == 4)
                        {
                            SpawnSpellFall(enemyBehaviour, false, 12);
                        }
                    }

                    //magic.SetActive(true);
                    //magic.transform.position = spellSpawn.transform.position;
                    //magic.transform.rotation = spellSpawn.transform.rotation;
                    //magic.GetComponent<BaseSpell>().StartSpell(); //Doesn't have once in Trigger for Spell, may need it?
                }
                
                once2 = true;
            }
        }

        //When Animation End
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Spell") && !once4)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1) //0.9
            {
                //enemyBehaviour.agent.SetDestination(enemyBehaviour.gameObject.transform.position);
                once1 = false;
                once2 = false;
                once3 = false;
                once4 = true;
                count = 0;

                for (int i = 0; i < count; i++)
                {
                    pathPointList[i].GetComponent<BossPathPoint>().isUsedAttack = false;
                }

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
                /*if (combo)
                {
                    randNum = Random.Range(0, 3);
                    switch (randNum)
                    {
                        case 2:
                            enemyBehaviour.enemyAnim.SetBool("IsWalking", true);
                            //stateRange02.once1 = true;
                            //stateRange02.rangeDistance = 0;
                            //return stateRange02;
                            return this;
                        default:
                            return statePursue;
                    }
                }
                else
                {
                    return statePursue;
                }*/
            }
        }

        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Spell"))
        {
            timer += Time.deltaTime;

            if (timer >= 1f)
            {
                timer = 0;
                once1 = false;
            }
        }

        return this;
    }

    public void SpawnSpellFall(EnemyBehaviour enemyBehaviour, bool rand, int max)
    {
        if(rand)
        {
            do
            {
                do
                {
                    pathPointList[count] = enemyBehaviour.pathManager.CallPathPointAttack(true, 0);
                } while (pathPointList[count].GetComponent<BossPathPoint>().isUsedAttack == true);

                pathPointList[count].GetComponent<BossPathPoint>().isUsedAttack = true;
                magic = enemyBehaviour.poolingManager.callBoulderFall();
                magic.SetActive(true);
                magic.transform.position = pathPointList[count].transform.position;
                magic.transform.rotation = pathPointList[count].transform.rotation;
                magic.GetComponent<BaseSpell>().StartSpell();
                magic.GetComponent<BaseSpell>().isPooling = true;
                count++;

            } while (count < max);
        }
        else if (!rand)
        {
            do
            {
                pathPointList[count] = enemyBehaviour.pathManager.CallPathPointAttack(false, count);
                magic = enemyBehaviour.poolingManager.callBoulderFall();
                magic.SetActive(true);
                magic.transform.position = pathPointList[count].transform.position;
                magic.transform.rotation = pathPointList[count].transform.rotation;
                magic.GetComponent<BaseSpell>().StartSpell();
                magic.GetComponent<BaseSpell>().isPooling = true;
                count++;

            } while (count < max);
        }
    }
}