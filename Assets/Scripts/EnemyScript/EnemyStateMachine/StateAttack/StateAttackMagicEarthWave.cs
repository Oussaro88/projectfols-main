using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAttackMagicEarthWave : StateAttackMagic
{
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
            anim.SetTrigger("IsThrowing");
            //character.GetComponent<SkinnedMeshRenderer>().material = signMat;
            /*spell.GetComponent<Rigidbody>().velocity = Vector3.zero;
                spellSign.transform.position = spellSpawn.transform.position;
                spellSign.transform.rotation = spellSpawn.transform.rotation;
                spellSign.SetActive(true);
                //Spell Script Activate*/
        }

        //When Animation reached Halfway
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Spell") && !once2)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
            {
                once4 = false;
                //character.GetComponent<SkinnedMeshRenderer>().material = defaultMat;
                //enemyBehaviour.agent.SetDestination(enemyBehaviour.gameObject.transform.position); 
                //enemyBehaviour.GetComponent<EnemyMain>().OnAttack();

                // Code for launching rocks
                if (isPooling)
                {
                    magic = enemyBehaviour.poolingManager.callEarthWave();

                    magic.SetActive(true);
                    magic.transform.position = spellSpawn.transform.position;
                    magic.transform.rotation = spellSpawn.transform.rotation;
                    magic.GetComponent<Spell_EarthWave>().StartSpell();
                    magic.GetComponent<BaseSpell>().isPooling = true;
                }
                else
                {
                    magic = Instantiate(spell, spellSpawn.transform.position, spellSpawn.transform.rotation);
                    magic.GetComponent<Spell_EarthWave>().StartSpell();
                    
                }

                //magic = Instantiate(spell, spellSpawn.transform.position, spellSpawn.transform.rotation);
                //magic.GetComponent<Spell_EarthWave>().StartSpell();
                //magic.GetComponent<BaseProjectile>().dmg = 20;

                //spellSign.SetActive(false);
                /*spell.GetComponent<Rigidbody>().velocity = Vector3.zero;
                spell.transform.position = spellSpawn.transform.position;
                spell.transform.rotation = spellSpawn.transform.rotation;
                spell.SetActive(true);
                //Spell Script Activate*/

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

                if (isBoss)
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
}

