using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateWaiting : EnemyState
{
    public StatePursue statePursue;
    public StateAttack stateWarrior;
    public bool start = false;
    public bool isBoss = false;
    public bool once = false;
    public bool magicCoolDown = false;
    public float timer = 0f;
    public float coolTime;

    public override EnemyState RunState(EnemyBehaviour enemyBehaviour)
    {
        if (!once)
        {
            enemyBehaviour.agent.SetDestination(enemyBehaviour.gameObject.transform.position);
            enemyBehaviour.enemyAnim.SetBool("IsWalking", false);
            once = true;
        }

        if (magicCoolDown)
        {
            timer += Time.deltaTime;

            if(timer >= coolTime)
            {
                timer = 0;
                magicCoolDown = false;
                once = false;
                return statePursue;
            }
        }

        if (start)
        {
            start = false;
            once = false;

            if (isBoss)
            {
                return stateWarrior;
            }
            else
            {
                return statePursue;
            }
        }

        return this;
    }
}
