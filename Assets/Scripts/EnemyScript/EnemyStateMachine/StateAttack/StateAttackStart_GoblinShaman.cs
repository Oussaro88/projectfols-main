using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAttackStart_GoblinShaman : StateAttack
{
    public StatePursue statePursue;

    public StateAttackMagic stateMagicFireBall;
    public StateAttackMagic stateMagicFireArrow;
    public StateAttackMagic stateMagicFireLance;
    public StateAttackMagic stateMagicFireBomb;
    public StateAttackMagic stateMagicFireFloor;
    public StateAttackMagic stateMagicFireWall;
    public StateAttackMagic stateMagicFireWave;
    public StateAttackMagic stateMagicFireMeteor;
    public StateAttackMagic stateMagicLightningField;
    public StateAttackMagic stateMagicLightningStrike;
    public StateAttackMagic stateMagicLightningWave;
    public StateAttackMagic stateMagicLightningStorm;

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

        if (hp <= 200) //At Low Health
        {
            if (playerDistance <= 7) //Close to Boss
            {
                randNum = Random.Range(0, 8);
                switch (randNum)
                {
                    case 0:
                        return stateMagicFireBall;
                    case 1:
                        return stateMagicFireArrow;
                    case 2:
                        return stateMagicFireBomb;
                    case 3:
                        return stateMagicFireFloor;
                    case 4:
                        return stateMagicFireWall;
                    case 5:
                        return stateMagicLightningStrike;
                    case 6:
                        return stateMagicLightningField;
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
                            return stateMagicFireArrow;
                        case 1:
                            return stateMagicFireLance;
                        case 2:
                            return stateMagicFireMeteor;
                        case 3:
                            return stateMagicLightningStorm;
                        default:
                            return statePursue;

                    }
                }
                else //Mid Range
                {
                    randNum = Random.Range(0, 8);
                    switch (randNum)
                    {
                        case 0:
                            return stateMagicFireBall;
                        case 1:
                            return stateMagicFireArrow;
                        case 2:
                            return stateMagicFireLance;
                        case 3:
                            return stateMagicFireWave;
                        case 4: 
                            return stateMagicLightningWave;
                        case 5:
                            return stateMagicFireMeteor;
                        case 6:
                            return stateMagicLightningStorm;
                        default:
                            return statePursue;
                    }
                }
            }
        }
        else //At High Health
        {
            if (playerDistance <= 7) //Close to Boss
            {
                randNum = Random.Range(0, 6);
                switch (randNum)
                {
                    case 0:
                        return stateMagicFireBall;
                    case 1:
                        return stateMagicFireBomb;
                    case 2:
                        return stateMagicFireFloor;
                    case 3:
                        return stateMagicFireWall;
                    case 4:
                        return stateMagicLightningStrike;
                    default:
                        return statePursue;
                }
            }
            else //Far From Boss
            {
                if (enemyBehaviour.sensorManager.CheckPlayerDistanceFar()) //Long Range
                {
                    randNum = Random.Range(0, 3);
                    switch (randNum)
                    {
                        case 0:
                            return stateMagicFireArrow;
                        case 1:
                            return stateMagicFireMeteor;
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
                            return stateMagicFireBall;
                        case 1:
                            return stateMagicFireArrow;
                        case 2:
                            return stateMagicFireWave;
                        case 3: 
                            return stateMagicLightningWave;
                        default:
                            return statePursue;
                    }
                }
            }
        }

        //return this;
    }
}
