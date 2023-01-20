using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAttackStart_GoblinWarrior : StateAttack
{ 
    public StatePursue statePursue;
    public StateAttackMelee01 stateMeleeLight;
    public StateAttackMelee02 stateMeleeHeavy;
    public StateAttackMeleeCharge stateMeleeCharge;
    public StateAttackRange02 stateRangeBoulder;
    public StateAttackRange02 stateRangeBoulderSpreadFront;
    public StateAttackRange02 stateRangeBoulderSpreadWide;
    public StateAttackMagic stateMagicQuake;
    public StateAttackMagic stateMagicQuakeV2;
    public StateAttackMagic stateMagicWave;
    public StateAttackMagic stateMagicFallRandom;
    public StateAttackMagic stateMagicFallPrecise;
    public StateAttackMagic stateMagicBlast;
    public float playerDistance;
    public int hp;
    public int hpMax;
    public bool once = false;
    public int randNum;

    public override EnemyState RunState(EnemyBehaviour enemyBehaviour)
    {
        if (!once)
        {
            hpMax = enemyBehaviour.gameObject.GetComponent<EnemyMain>().GetMaxHP;
            once = true;
        }

        playerDistance = Vector3.Distance(transform.position, enemyBehaviour.player.transform.position);
        hp = enemyBehaviour.gameObject.GetComponent<EnemyMain>().GetCurrentHP;

        if (hp <= 200 /*30% (hpMax *30 /100)*/) //At Low Health
        {
            if (playerDistance <= 3) //Close to Boss
            {
                randNum = Random.Range(0, 6);
                switch (randNum)
                {
                    case 0:
                    case 1:
                        return stateMeleeLight;
                    case 2:
                        return stateMeleeHeavy;
                    case 3:
                        return stateRangeBoulderSpreadWide;
                    case 4:
                        return stateMagicQuakeV2;
                    default:
                        return statePursue;
                }
            }
            else //Far From Boss
            {
                if (enemyBehaviour.sensorManager.CheckPlayerDistanceFar()) //Long Range
                {
                    randNum = Random.Range(0, 5);
                    switch (randNum)
                    {
                        case 0:
                        case 1:
                            return stateMagicFallPrecise;
                        case 2:
                            return stateRangeBoulder;
                        case 3:
                            return stateRangeBoulderSpreadFront;
                        //case 4:
                            //return stateMeleeCharge;
                        default:
                            return statePursue;

                    }
                }
                else //Mid Range
                {
                    randNum = Random.Range(0, 7);
                    switch (randNum)
                    {
                        case 0:
                        case 1:
                            return stateRangeBoulder;
                        case 2:
                            return stateRangeBoulderSpreadFront;
                        case 3:
                            return stateRangeBoulderSpreadWide;
                        case 4:
                            //return stateMagicBlast;
                            return stateMagicWave;
                        case 5:
                            return stateMagicFallRandom;
                        default:
                            return statePursue;
                    }
                }
            }
        } 
        else //At High Health
        {
            if (playerDistance <= 3) //Close to Boss
            {
                randNum = Random.Range(0, 5);
                switch (randNum)
                {
                    case 0:
                    case 1:
                        return stateMeleeLight;
                    case 2:
                        return stateMeleeHeavy;
                    case 3:
                        return stateMagicQuake;
                    default:
                        return statePursue;
                }
            }
            else //Far From Boss
            {
                if (enemyBehaviour.sensorManager.CheckPlayerDistanceFar()) //Long Range
                {
                    randNum = Random.Range(0, 4);
                    switch (randNum)
                    {
                        case 0:
                        case 1:
                            return stateMagicFallPrecise;
                        case 2:
                            return stateRangeBoulder;
                        default:
                            return statePursue;

                    }
                }
                else //Mid Range
                {
                    randNum = Random.Range(0, 5);
                    switch (randNum)
                    {
                        case 0:
                        case 1:
                            return stateRangeBoulder;
                        case 2:
                            return stateRangeBoulderSpreadFront;
                        case 3:
                            return stateMagicWave;
                        default:
                            return statePursue;
                    }
                }
            }
        }

        //return this;
    }
}
