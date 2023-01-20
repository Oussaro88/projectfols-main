using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateKnocked_GoblinWarrior : StateKnocked
{
    public StateAttackMelee01 stateMeleeLight;
    public StateAttackMelee02 stateMeleeHeavy;
    public StateAttackMeleeCharge stateMeleeCharge;
    public StateAttackRange02 stateRangeThrow;
    public StateAttackRangeBoulderSpread stateRangeSpreadF;
    public StateAttackRangeBoulderSpread stateRangeSpreadW;
    public StateAttackMagic stateMagicWave;
    public StateAttackMagic stateMagicQuake;
    public StateAttackMagic stateMagicQuakeV2;
    public StateAttackMagic stateMagicFallRandom;
    public StateAttackMagic stateMagicFallPrecise;
    public StateAttackMagic stateMagicBlast;
    public StateAttack stateWarrior;
    public StateWaiting stateWaiting;

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
            stateMeleeLight.once1 = false;
            stateMeleeLight.once2 = false;
            stateMeleeLight.once3 = false;

            stateMeleeHeavy.once1 = false;
            stateMeleeHeavy.once2 = false;
            stateMeleeHeavy.once3 = false;

            stateMeleeCharge.once1 = false;
            stateMeleeCharge.once2 = false;

            stateRangeThrow.once1 = false;
            stateRangeThrow.once2 = false;
            stateRangeThrow.once3 = false;
            stateRangeThrow.once4 = true;

            stateRangeSpreadF.once1 = false;
            stateRangeSpreadF.once2 = false;
            stateRangeSpreadF.once3 = false;
            stateRangeSpreadF.once4 = true;
            stateRangeSpreadF.iSPOnce1 = true;
            stateRangeSpreadF.iSPOnce2 = true;
            stateRangeSpreadF.iSIOnce1 = true;
            stateRangeSpreadF.iSIOnce2 = true;

            stateRangeSpreadW.once1 = false;
            stateRangeSpreadW.once2 = false;
            stateRangeSpreadW.once3 = false;
            stateRangeSpreadW.once4 = true;
            stateRangeSpreadW.iSPOnce1 = true;
            stateRangeSpreadW.iSPOnce2 = true;
            stateRangeSpreadW.iSIOnce1 = true;
            stateRangeSpreadW.iSIOnce2 = true;

            stateMagicWave.once1 = false;
            stateMagicWave.once2 = false;
            stateMagicWave.once3 = false;
            stateMagicWave.once4 = true;

            stateMagicQuake.once1 = false;
            stateMagicQuake.once2 = false;
            stateMagicQuake.once3 = false;
            stateMagicQuake.once4 = true;

            stateMagicQuakeV2.once1 = false;
            stateMagicQuakeV2.once2 = false;
            stateMagicQuakeV2.once3 = false;
            stateMagicQuakeV2.once4 = true;

            stateMagicFallRandom.once1 = false;
            stateMagicFallRandom.once2 = false;
            stateMagicFallRandom.once3 = false;
            stateMagicFallRandom.once4 = true;

            stateMagicFallPrecise.once1 = false;
            stateMagicFallPrecise.once2 = false;
            stateMagicFallPrecise.once3 = false;
            stateMagicFallPrecise.once4 = true;

            stateMagicBlast.once1 = false;
            stateMagicBlast.once2 = false;
            stateMagicBlast.once3 = false;
            stateMagicBlast.once4 = true;

            statePursue.once = false;

            stateWaiting.timer = 0;
            stateWaiting.once = false;
            stateWaiting.magicCoolDown = false;
            stateWaiting.start = false;

            enemyBehaviour.gameObject.GetComponent<EnemyBossWarrior>().CannotDamage(); //Only Usable if this enemy gameobject has script enemymelee

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
                
                return stateWarrior;
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
