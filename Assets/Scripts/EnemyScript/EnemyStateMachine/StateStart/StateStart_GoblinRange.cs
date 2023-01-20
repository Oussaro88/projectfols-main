using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateStart_GoblinRange : StateStart
{
    public StateAttackRange02 stateRange01;
    public StateAttackRange02 stateRange02;
    public StateAttackRange02 stateRange03;
    public StateAttackRange02 stateRangeArrow;
    public StateAttackRange02 stateRangeLance;

    public override EnemyState RunState(EnemyBehaviour enemyBehaviour)
    {
        if (!once)
        {
            stateRange01.once1 = false;
            stateRange01.once2 = false;
            stateRange01.once3 = false;
            stateRange01.once4 = true;

            stateRange02.once1 = false;
            stateRange02.once2 = false;
            stateRange02.once3 = false;
            stateRange02.once4 = true;

            stateRange03.once1 = false;
            stateRange03.once2 = false;
            stateRange03.once3 = false;
            stateRange03.once4 = true;
            
            stateRangeArrow.once1 = false;
            stateRangeArrow.once2 = false;
            stateRangeArrow.once3 = false;
            stateRangeArrow.once4 = true;

            stateRangeLance.once1 = false;
            stateRangeLance.once2 = false;
            stateRangeLance.once3 = false;
            stateRangeLance.once4 = true;
            
            statePursue.once = false;

            stateKnocked.once1 = false;
            stateKnocked.once2 = false;

            stateDeath.once1 = false;
            stateDeath.once2 = false;
            stateDeath.once3 = false;

            if (enemyBehaviour.GetComponent<EnemyMain>().isPooling)
            {
                stateRange01.isPooling = true;
                stateRange02.isPooling = true;
                stateRange03.isPooling = true;
                stateRangeArrow.isPooling = true;
                stateRangeLance.isPooling = true;
                onceP = true;
            }
            else
            {
                stateRange01.isPooling = false;
                stateRange02.isPooling = false;
                stateRange03.isPooling = false;
                stateRangeArrow.isPooling = false;
                stateRangeLance.isPooling = false;
                onceI = true;
            }
            
            enemyBehaviour.gameObject.GetComponent<EnemyMain>().canHurt = false;

            character.GetComponent<Renderer>().material = dissolveMat;
            itemSmall.GetComponent<Renderer>().material = dissolveMat;
            itemLarge.GetComponent<Renderer>().material = dissolveMat;

            character.GetComponent<Renderer>().material.SetFloat("_CutoffHeight", 3f);
            itemSmall.GetComponent<Renderer>().material.SetFloat("_CutoffHeight", 9f);
            itemLarge.GetComponent<Renderer>().material.SetFloat("_CutoffHeight", 16f);

            cutoffValueChar = 3f;
            cutoffValueSmall = 9f;
            cutoffValueLarge = 16f;

            cutoffValueChar = 3;

            once = true;
        }

        if (onceI)
        {
            spawnVFX = Instantiate(spawnVFXInstant, enemyBehaviour.gameObject.transform.position, enemyBehaviour.gameObject.transform.rotation);
            spawnVFX.GetComponent<EnemySpawnScript>().StartVFX();
            onceI = false;
        }

        if (onceP)
        {
            spawnVFX = enemyBehaviour.poolingManager.callSpawnVFX();
            spawnVFX.SetActive(true);
            spawnVFX.transform.position = enemyBehaviour.gameObject.transform.position;
            spawnVFX.transform.rotation = enemyBehaviour.gameObject.transform.rotation;
            spawnVFX.GetComponent<EnemySpawnScript>().StartVFX();
            spawnVFX.GetComponent<EnemySpawnScript>().isPooling = true;
            onceP = false;
        }

        timer += Time.deltaTime;

        if (timer >= 6f)
        {
            once = false;
            timer = 0;

            character.GetComponent<Renderer>().material.SetFloat("_CutoffHeight", -1f);
            itemSmall.GetComponent<Renderer>().material.SetFloat("_CutoffHeight", -9f);
            itemLarge.GetComponent<Renderer>().material.SetFloat("_CutoffHeight", -12f);

            stateDeath.cutoffValueChar = -1f;
            stateDeath.cutoffValueSmall = -9f;
            stateDeath.cutoffValueLarge = -12f;

            character.GetComponent<Renderer>().material = defaultMat;
            itemSmall.GetComponent<Renderer>().material = defaultMat;
            itemLarge.GetComponent<Renderer>().material = defaultMat;

            enemyBehaviour.gameObject.GetComponent<EnemyMain>().canHurt = true;
            return stateWander;
        }
        else
        {
            timer += Time.deltaTime;

            cutoffValueChar -= Time.deltaTime;
            cutoffValueSmall -= Time.deltaTime * 5;
            cutoffValueLarge -= Time.deltaTime * 8;

            character.GetComponent<Renderer>().material.SetFloat("_CutoffHeight", cutoffValueChar);
            itemSmall.GetComponent<Renderer>().material.SetFloat("_CutoffHeight", cutoffValueSmall);
            itemLarge.GetComponent<Renderer>().material.SetFloat("_CutoffHeight", cutoffValueLarge);

            return this;
        }
    }
}
