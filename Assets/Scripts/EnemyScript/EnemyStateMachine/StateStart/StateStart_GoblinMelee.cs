using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateStart_GoblinMelee : StateStart
{
    public StateAttackMelee01 stateMelee01;
    public StateAttackMelee02 stateMelee02;
    public StateAttackRange01 stateRange01;

    public override EnemyState RunState(EnemyBehaviour enemyBehaviour)
    {
        if (!once)
        {
            stateMelee01.once1 = false;
            stateMelee01.once2 = false;
            stateMelee01.once3 = false;

            stateMelee02.once1 = false;
            stateMelee02.once2 = false;
            stateMelee02.once3 = false;

            stateRange01.once1 = false;
            stateRange01.once2 = false;
            stateRange01.once3 = false;

            statePursue.once = false;

            stateKnocked.once1 = false;
            stateKnocked.once2 = false;

            stateDeath.once1 = false;
            stateDeath.once2 = false;
            stateDeath.once3 = false;

            if (enemyBehaviour.GetComponent<EnemyMain>().isPooling)
            {
                stateRange01.isPooling = true;
                onceP = true;
            }
            else
            {
                stateRange01.isPooling = false;
                onceI = true;
            }

            enemyBehaviour.gameObject.GetComponent<EnemyMelee>().CannotDamage();
            enemyBehaviour.gameObject.GetComponent<EnemyMain>().canHurt = false;

            character.GetComponent<Renderer>().material = dissolveMat;
            weapon = enemyBehaviour.gameObject.GetComponent<EnemyMelee>().SendWeaponUsed();
            weapon.GetComponent<Renderer>().material = dissolveMat;
            itemSmall.GetComponent<Renderer>().material = dissolveMat;
            itemLarge.GetComponent<Renderer>().material = dissolveMat;

            character.GetComponent<Renderer>().material.SetFloat("_CutoffHeight", 3f);
            weapon.GetComponent<Renderer>().material.SetFloat("_CutoffHeight", 2f);
            itemSmall.GetComponent<Renderer>().material.SetFloat("_CutoffHeight", 3f);
            itemLarge.GetComponent<Renderer>().material.SetFloat("_CutoffHeight", 5f);

            cutoffValueChar = 3f;
            cutoffValueWep = 2f;
            cutoffValueSmall = 3f;
            cutoffValueLarge = 5f;

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
            weapon.GetComponent<Renderer>().material.SetFloat("_CutoffHeight", -1f);
            itemSmall.GetComponent<Renderer>().material.SetFloat("_CutoffHeight", -14f);
            itemLarge.GetComponent<Renderer>().material.SetFloat("_CutoffHeight", -16f);

            stateDeath.cutoffValueChar = -1f;
            stateDeath.cutoffValueWep = -1f;
            stateDeath.cutoffValueSmall = -14f;
            stateDeath.cutoffValueLarge = -16f;

            character.GetComponent<Renderer>().material = defaultMat;
            weapon.GetComponent<Renderer>().material = defaultMat;
            itemSmall.GetComponent<Renderer>().material = defaultMat;
            itemLarge.GetComponent<Renderer>().material = defaultMat;

            enemyBehaviour.gameObject.GetComponent<EnemyMain>().canHurt = true;
            return stateWander;
        }
        else
        {
            timer += Time.deltaTime;

            cutoffValueChar -= Time.deltaTime;
            cutoffValueWep -= Time.deltaTime;
            cutoffValueSmall -= Time.deltaTime * 5;
            cutoffValueLarge -= Time.deltaTime * 6;

            character.GetComponent<Renderer>().material.SetFloat("_CutoffHeight", cutoffValueChar);
            weapon.GetComponent<Renderer>().material.SetFloat("_CutoffHeight", cutoffValueWep);
            itemSmall.GetComponent<Renderer>().material.SetFloat("_CutoffHeight", cutoffValueSmall);
            itemLarge.GetComponent<Renderer>().material.SetFloat("_CutoffHeight", cutoffValueLarge);

            return this;
        }
    }
}
