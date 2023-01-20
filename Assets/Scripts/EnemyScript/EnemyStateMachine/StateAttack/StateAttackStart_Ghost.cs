using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAttackStart_Ghost : StateAttack
{
    public StatePursue statePursue;
    public StateAttackMagic stateMagicBall;
    public StateAttackMagic stateMagicArrow;
    public StateAttackMagic stateMagicLance;
    public StateAttackMagic stateMagicBomb;
    public StateAttackMagic stateMagicWall;
    public StateAttackMagic stateMagicFloor;
    public float playerDistance;
    private Vector3 target;
    public bool once = false;
    public Animator anim;
    public bool canDmg;
    public int randNum;

    public override EnemyState RunState(EnemyBehaviour enemyBehaviour)
    {
        if (enemyBehaviour.GetComponent<EnemyMain>().isPooling)
        {
            if (enemyBehaviour.GetComponent<EnemyMain>().GetCurrentHP <= 30)
            {
                randNum = Random.Range(0, 6);
                switch (randNum)
                {
                    case 0:
                        return stateMagicWall;
                    case 1:
                        return stateMagicFloor;
                    case 2:
                        return stateMagicBomb;
                    default:
                        return stateMagicBall;
                }
            }
            else
            {
                randNum = Random.Range(0, 4);
                switch (randNum)
                {
                    case 0:
                        return stateMagicBomb;
                    default:
                        return stateMagicBall;
                }
            }
        }
        else
        {
            return stateMagicBall;
        }

        //return this;
    }
}
