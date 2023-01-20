using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAttackMagic : EnemyState
{
    public StatePursue statePursue;
    public StateWaiting stateWaiting;
    public StateAttack stateWarrior;
    public Vector3 target;
    public GameObject magic;
    public GameObject spell;
    public GameObject spellSpawn;
    public GameObject spellSign;
    public bool once1 = false;
    public bool once2 = false;
    public bool once3 = false;
    public bool once4 = true; //Prevent Return Instantaneous. In short, Upon Entering the Second StateRange, it immediately goes to return, thus breaking the combo.
    public int randNum;
    public Animator anim;
    public bool isProjectile;
    public float projectileSpeed;
    public bool combo;
    public bool coolDown;
    public float coolTime;
    public bool isBoss = false;
    public GameObject character;
    public Material defaultMat;
    public Material signMat;
    public float timer = 0;

    public bool isPooling = false;

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
            /*if (signMat)
            {
                character.GetComponent<SkinnedMeshRenderer>().material = signMat;
            }*/
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
                /*if (defaultMat)
                {
                    character.GetComponent<SkinnedMeshRenderer>().material = defaultMat;
                }*/
                //enemyBehaviour.agent.SetDestination(enemyBehaviour.gameObject.transform.position); 
                //enemyBehaviour.GetComponent<EnemyMain>().OnAttack();

                // Code for casting spell
                if (isPooling)
                {
                    magic = CallMagicSpell(enemyBehaviour);

                    magic.SetActive(true);
                    magic.transform.position = spellSpawn.transform.position;
                    magic.transform.rotation = spellSpawn.transform.rotation;
                    magic.GetComponent<BaseSpell>().StartSpell(); //Doesn't have once in Trigger for Spell, may need it?
                    magic.GetComponent<BaseSpell>().isPooling = true;

                    if (isProjectile)
                    {
                        magic.GetComponent<Rigidbody>().velocity = Vector3.zero;
                        magic.GetComponent<Rigidbody>().AddForce(transform.forward * projectileSpeed, ForceMode.Impulse);
                    }
                } 
                else
                {
                    magic = Instantiate(spell, spellSpawn.transform.position, spellSpawn.transform.rotation);
                    magic.GetComponent<BaseSpell>().StartSpell();
                    if (isProjectile)
                    {
                        magic.GetComponent<Rigidbody>().AddForce(transform.forward * projectileSpeed, ForceMode.Impulse);
                    }
                }
                /*
                magic = Instantiate(spell, spellSpawn.transform.position, spellSpawn.transform.rotation);
                magic.GetComponent<BaseSpell>().StartSpell();
                if (isProjectile)
                {
                    magic.GetComponent<Rigidbody>().AddForce(transform.forward * projectileSpeed, ForceMode.Impulse);
                }*/
                //magic.GetComponent<BaseProjectile>().dmg = 30;


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

                if (coolDown)
                {
                    stateWaiting.magicCoolDown = true;
                    stateWaiting.coolTime = coolTime;
                    return stateWaiting;
                }
                else if(isBoss)
                {
                    return stateWarrior;
                }
                else
                {
                    enemyBehaviour.enemyAnim.SetBool("IsWalking", true);
                    //enemyBehaviour.enemyAnim.SetTrigger("IsWalkingTrigger");
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

    public GameObject CallMagicSpell(EnemyBehaviour enemyBehaviour)
    {
        if (spell.GetComponent<Spell_FireBall>())
        {
            return enemyBehaviour.poolingManager.callFireBall();
        } 
        else if (spell.GetComponent<Spell_FireBomb>())
        {
            return enemyBehaviour.poolingManager.callFireBomb();
        }
        else if (spell.GetComponent<Spell_FireWall>())
        {
            return enemyBehaviour.poolingManager.callFireWall();
        }
        else if (spell.GetComponent<Spell_FireFloor>())
        {
            return enemyBehaviour.poolingManager.callFireFloor();
        }
        else if (spell.GetComponent<Spell_FireWave>())
        {
            return enemyBehaviour.poolingManager.callFireWave();
        }
        else if (spell.GetComponent<Spell_LightningStrike>())
        {
            return enemyBehaviour.poolingManager.callLightningStrike();
        }
        else if (spell.GetComponent<Spell_LightningField>())
        {
            return enemyBehaviour.poolingManager.callLightningField();
        }
        else if (spell.GetComponent<Spell_LightningWave>())
        {
            return enemyBehaviour.poolingManager.callLightningWave();
        }
        else if (spell.GetComponent<Spell_EarthQuake>())
        {
            return enemyBehaviour.poolingManager.callEarthQuake();
        }
        else if (spell.GetComponent<Spell_EarthStomp>())
        {
            return enemyBehaviour.poolingManager.callEarthStomp();
        }
        else if (spell.GetComponent<Spell_EarthWave>())
        {
            return enemyBehaviour.poolingManager.callEarthWave();
        }
        else if (spell.GetComponent<Spell_BoulderFall>())
        {
            return enemyBehaviour.poolingManager.callBoulderFall();
        }
        else if (spell.GetComponent<Spell_BoulderBlast>())
        {
            //return enemyBehaviour.poolingManager.callBoulderBlast;
        }

        return enemyBehaviour.poolingManager.callFireBall();

    }

}
