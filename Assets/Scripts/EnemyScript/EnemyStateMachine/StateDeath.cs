using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateDeath : EnemyState
{
    public bool once1 = false;
    public bool once2 = false;
    public bool once3 = false;
    public bool dissolveStart = false;
    public RuntimeAnimatorController deathAnimator;
    public Animator anim;
    public GameObject character;
    public GameObject weapon; 
    public GameObject itemSmall;
    public GameObject itemLarge;
    public GameObject itemMask;
    public Material dissolveMat;
    public float cutoffValueChar = 0;
    public float cutoffValueWep = 0;
    public float cutoffValueSmall = 0;
    public float cutoffValueLarge = 0;
    public float cutoffValueMask = 0;
    public float timer = 0;

    public override EnemyState RunState(EnemyBehaviour enemyBehaviour)
    {
        if (!once1)
        {
            enemyBehaviour.agent.SetDestination(enemyBehaviour.gameObject.transform.position);
            enemyBehaviour.enemyAnim.SetBool("IsWalking", false);
            enemyBehaviour.enemyAnim.runtimeAnimatorController = deathAnimator;

            character.GetComponent<Renderer>().material = dissolveMat;
            character.GetComponent<Renderer>().material.SetFloat("_CutoffHeight", cutoffValueChar);

            if (weapon)
            {
                if(enemyBehaviour.gameObject.GetComponent<EnemyMelee>())
                {
                    weapon = enemyBehaviour.gameObject.GetComponent<EnemyMelee>().SendWeaponUsed();
                }
                if (enemyBehaviour.gameObject.GetComponent<EnemyBossWarrior>())
                {
                    weapon = enemyBehaviour.gameObject.GetComponent<EnemyBossWarrior>().SendWeaponUsed();
                }
                weapon.GetComponent<Renderer>().material = dissolveMat;
                weapon.GetComponent<Renderer>().material.SetFloat("_CutoffHeight", cutoffValueWep);
            }
            if (itemSmall)
            {
                itemSmall.GetComponent<Renderer>().material = dissolveMat;
                itemSmall.GetComponent<Renderer>().material.SetFloat("_CutoffHeight", cutoffValueSmall);
            }
            if (itemLarge)
            {
                itemLarge.GetComponent<Renderer>().material = dissolveMat;
                itemLarge.GetComponent<Renderer>().material.SetFloat("_CutoffHeight", cutoffValueLarge);
            }
            if (itemMask)
            {
                itemMask.GetComponent<Renderer>().material = dissolveMat;
                itemMask.GetComponent<Renderer>().material.SetFloat("_CutoffHeight", cutoffValueMask);
            }

            dissolveStart = false;
            once2 = false;
            once3 = false;
            once1 = true;
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Death") && !once2)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5)
            {
                enemyBehaviour.agent.SetDestination(enemyBehaviour.gameObject.transform.position);
                dissolveStart = true;
                once2 = true;
            }
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Death") && !once3)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9)
            {
                enemyBehaviour.agent.SetDestination(enemyBehaviour.gameObject.transform.position);
                once1 = false;
                once3 = true;
                enemyBehaviour.gameObject.GetComponent<EnemyMain>().ReturnOrigin();
                enemyBehaviour.gameObject.SetActive(false);
            }
        }

        if (dissolveStart && cutoffValueChar <= 3f)
        {
            cutoffValueChar += Time.deltaTime * 5f;
            cutoffValueWep += Time.deltaTime * 5f;
            cutoffValueSmall += Time.deltaTime * 30f;
            cutoffValueLarge += Time.deltaTime * 40f;
            cutoffValueMask += Time.deltaTime * 50f;

            character.GetComponent<Renderer>().material.SetFloat("_CutoffHeight", cutoffValueChar);
            if (weapon)
            {
                weapon.GetComponent<Renderer>().material.SetFloat("_CutoffHeight", cutoffValueWep);
            }
            if (itemSmall)
            {
                itemSmall.GetComponent<Renderer>().material.SetFloat("_CutoffHeight", cutoffValueSmall);
            }
            if (itemLarge)
            {
                itemLarge.GetComponent<Renderer>().material.SetFloat("_CutoffHeight", cutoffValueLarge);
            }
            if (itemMask)
            {
                itemMask.GetComponent<Renderer>().material.SetFloat("_CutoffHeight", cutoffValueMask);
            }
        }

        return this;
    }
}
