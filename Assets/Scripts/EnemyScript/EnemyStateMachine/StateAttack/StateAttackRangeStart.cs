using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAttackRangeStart : StateAttack
{
    public StatePursue statePursue;
    public StateAttackRange02 stateRangeSphere;
    public StateAttackRange02 stateRangeArrow;
    public StateAttackRange02 stateRangeLance;
    public float playerDistance;
    private Vector3 target;
    public bool once = false;
    public Animator anim;
    public bool canDmg;
    public int randNum; 
    public int hp;
    public int hpMax;

    public override EnemyState RunState(EnemyBehaviour enemyBehaviour)
    {
        if (!once)
        {
            hpMax = enemyBehaviour.gameObject.GetComponent<EnemyMain>().GetMaxHP;
            once = true;
        }

        hp = enemyBehaviour.gameObject.GetComponent<EnemyMain>().GetCurrentHP;

        if (hp <= 30 /*30% (hpMax *30 /100)*/) //At Low Health
        {
            randNum = Random.Range(0, 5);
            switch (randNum)
            {
                case 0:
                    return stateRangeLance;
                case 1:
                case 2:
                    return stateRangeArrow;
                default:
                    return stateRangeSphere;
            }    
        }
        else
        {
            randNum = Random.Range(0, 3);
            switch (randNum)
            {
                case 0:
                    return stateRangeArrow;
                default:
                    return stateRangeSphere;
            }

        }

        //return this;
    }
}

