using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateKnocked_GoblinMelee : StateKnocked
{
    //Add Pursue
    public StateAttackMelee01 stateMelee01;
    public StateAttackMelee02 stateMelee02;
    public StateAttackRange01 stateRange01;
    public override EnemyState RunState(EnemyBehaviour enemyBehaviour)
    {
        if (!once1)
        {
            enemyBehaviour.agent.SetDestination(enemyBehaviour.gameObject.transform.position);
            enemyBehaviour.enemyAnim.SetBool("IsWalking", false);
            anim.SetTrigger("IsKnocked");
            //character.GetComponent<SkinnedMeshRenderer>().material = knockedMat;
            //character.GetComponent<SkinnedMeshRenderer>().material.SetFloat("_OutlineWidth", 0f);
            //stateAttack.once = false;
            //stateAttack.canDmg = false;
            if (enemyBehaviour.gameObject.GetComponent<EnemyMelee>())
            {
                if (statePursue)
                {
                    statePursue.once = false;
                }
                if (stateMelee01)
                {
                    stateMelee01.once1 = false; stateMelee01.once2 = false; stateMelee01.once3 = false;
                }
                if (stateMelee02)
                {
                    stateMelee02.once1 = false; stateMelee02.once2 = false; stateMelee02.once3 = false;
                }
                if (stateRange01)
                {
                    stateRange01.once1 = false; stateRange01.once2 = false; stateRange01.once3 = false;
                }
                enemyBehaviour.gameObject.GetComponent<EnemyMelee>().CannotDamage(); //Only Usable if this enemy gameobject has script enemymelee
            }
            once1 = true;
            once2 = false;
            timer = 0;
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Knocked") && !once2)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
            {
                enemyBehaviour.agent.SetDestination(enemyBehaviour.gameObject.transform.position);
                //character.GetComponent<SkinnedMeshRenderer>().material.SetFloat("_OutlineWidth", 4f);
                //enemyBehaviour.gameObject.GetComponent<EnemyMain>().canHurt = false;
                once2 = true;
            }
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Knocked"))
        {
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                enemyBehaviour.agent.SetDestination(enemyBehaviour.gameObject.transform.position);
                //character.GetComponent<SkinnedMeshRenderer>().material.SetFloat("_OutlineWidth", 0f);
                //enemyBehaviour.gameObject.GetComponent<EnemyMain>().canHurt = true;
                once1 = false;
                once2 = false;
                anim.SetBool("IsWalking", true);
                //anim.SetTrigger("IsWalkingTrigger");
                return statePursue;
            }
        }

        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Knocked")) 
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
