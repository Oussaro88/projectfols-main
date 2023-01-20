using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateKnocked_GhostRange : StateKnocked
{
    public bool found = false;
    public float randomX;
    public float randomZ;
    public Vector3 escapePos;
    public LayerMask mask;

    public StateAttackMagic stateMagic01;
    public StateAttackMagic stateMagic02;
    public StateAttackMagic stateMagic03;
    public StateAttackMagic stateMagic04;
    public StateWaiting stateWaiting;

    public override EnemyState RunState(EnemyBehaviour enemyBehaviour)
    {
        if (!once1)
        {
            once1 = true;
            once2 = false;
            timer = 0;
            mask = LayerMask.GetMask("Ground");
            enemyBehaviour.agent.SetDestination(enemyBehaviour.gameObject.transform.position);
            enemyBehaviour.enemyAnim.SetBool("IsWalking", false);
            anim.SetTrigger("IsKnocked");

            if (enemyBehaviour.gameObject.GetComponent<EnemyRange>())
            {
                if (statePursue)
                {
                    statePursue.once = false;
                }
                if (stateMagic01)
                {
                    stateMagic01.once1 = false; stateMagic01.once2 = false; stateMagic01.once3 = false; stateMagic01.once4 = true;
                }
                if (stateMagic02)
                {
                    stateMagic02.once1 = false; stateMagic02.once2 = false; stateMagic02.once3 = false; stateMagic02.once4 = true;
                }
                if (stateMagic03)
                {
                    stateMagic03.once1 = false; stateMagic03.once2 = false; stateMagic03.once3 = false; stateMagic03.once4 = true;
                }
                if (stateMagic04)
                {
                    stateMagic04.once1 = false; stateMagic04.once2 = false; stateMagic04.once3 = false; stateMagic04.once4 = true;
                }
                if (stateWaiting)
                {
                    stateWaiting.timer = 0;
                    stateWaiting.once = false;
                    stateWaiting.magicCoolDown = false;
                    stateWaiting.start = false;
                }
            }
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Knocked") && !once2)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
            {
                enemyBehaviour.agent.SetDestination(enemyBehaviour.gameObject.transform.position);
                found = false;

                do
                {
                    randomX = Random.Range(enemyBehaviour.boundBox.center.x - enemyBehaviour.boundBox.extents.x + enemyBehaviour.agent.radius,
                        enemyBehaviour.boundBox.center.x + enemyBehaviour.boundBox.extents.x - enemyBehaviour.agent.radius);
                    randomZ = Random.Range(enemyBehaviour.boundBox.center.z - enemyBehaviour.boundBox.extents.z + enemyBehaviour.agent.radius,
                        enemyBehaviour.boundBox.center.z + enemyBehaviour.boundBox.extents.z - enemyBehaviour.agent.radius);
                    escapePos = new Vector3(randomX, transform.position.y, randomZ);

                    NavMeshHit hit;
                    if (NavMesh.SamplePosition(escapePos, out hit, 1f, NavMesh.AllAreas))
                    {
                        if (Physics.Raycast(escapePos, -transform.up, 2f, mask))
                        {
                            escapePos = hit.position;
                            found = true;
                        }
                    }
                } while (!found);

                once2 = true;
            }
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Knocked"))
        {
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                enemyBehaviour.gameObject.transform.position = escapePos;
                enemyBehaviour.agent.SetDestination(enemyBehaviour.gameObject.transform.position);
                //character.GetComponent<SkinnedMeshRenderer>().material.SetFloat("_OutlineWidth", 0f);
                //enemyBehaviour.gameObject.GetComponent<EnemyMain>().canHurt = true;
                once1 = false;
                once2 = false;
                anim.SetBool("IsWalking", true);
                anim.SetTrigger("IsWalkingTrigger");
                return statePursue;
            }
        }

        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Knocked"))
        {
            timer += Time.deltaTime;

            if (timer >= 2f)
            {
                timer = 0;
                once1 = false;
            }
        }

        return this;
    }
}
