using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateKnocked_GoblinShaman : StateKnocked
{
    public bool found = false;
    public float randomX;
    public float randomZ;
    public Vector3 escapePos;
    public int randNum;

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
    public StateAttack stateShaman;
    public StateWaiting stateWaiting;

    public override EnemyState RunState(EnemyBehaviour enemyBehaviour)
    {
        if (!once1)
        {
            enemyBehaviour.agent.SetDestination(enemyBehaviour.gameObject.transform.position);
            enemyBehaviour.enemyAnim.SetBool("IsWalking", false);
            anim.SetTrigger("IsKnocked");

            stateMagicFireBall.once1 = false;
            stateMagicFireBall.once2 = false;
            stateMagicFireBall.once3 = false;
            stateMagicFireBall.once4 = true;

            stateMagicFireArrow.once1 = false;
            stateMagicFireArrow.once2 = false;
            stateMagicFireArrow.once3 = false;
            stateMagicFireArrow.once4 = true;

            stateMagicFireLance.once1 = false;
            stateMagicFireLance.once2 = false;
            stateMagicFireLance.once3 = false;
            stateMagicFireLance.once4 = true;

            stateMagicFireBomb.once1 = false;
            stateMagicFireBomb.once2 = false;
            stateMagicFireBomb.once3 = false;
            stateMagicFireBomb.once4 = true;

            stateMagicFireFloor.once1 = false;
            stateMagicFireFloor.once2 = false;
            stateMagicFireFloor.once3 = false;
            stateMagicFireFloor.once4 = true;

            stateMagicFireWall.once1 = false;
            stateMagicFireWall.once2 = false;
            stateMagicFireWall.once3 = false;
            stateMagicFireWall.once4 = true;

            stateMagicFireWave.once1 = false;
            stateMagicFireWave.once2 = false;
            stateMagicFireWave.once3 = false;
            stateMagicFireWave.once4 = true;

            stateMagicFireMeteor.once1 = false;
            stateMagicFireMeteor.once2 = false;
            stateMagicFireMeteor.once3 = false;
            stateMagicFireMeteor.once4 = true;

            stateMagicLightningField.once1 = false;
            stateMagicLightningField.once2 = false;
            stateMagicLightningField.once3 = false;
            stateMagicLightningField.once4 = true;

            stateMagicLightningStrike.once1 = false;
            stateMagicLightningStrike.once2 = false;
            stateMagicLightningStrike.once3 = false;
            stateMagicLightningStrike.once4 = true;

            stateMagicLightningWave.once1 = false;
            stateMagicLightningWave.once2 = false;
            stateMagicLightningWave.once3 = false;
            stateMagicLightningWave.once4 = true;

            stateMagicLightningStorm.once1 = false;
            stateMagicLightningStorm.once2 = false;
            stateMagicLightningStorm.once3 = false;
            stateMagicLightningStorm.once4 = true;

            statePursue.once = false;

            stateWaiting.timer = 0;
            stateWaiting.once = false;
            stateWaiting.magicCoolDown = false;
            stateWaiting.start = false;

            once1 = true;
            once2 = false;
            timer = 0;
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
                        escapePos = hit.position;
                        found = true;
                    }
                } while (!found);

                once2 = true;
            }
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Knocked"))
        {
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                enemyBehaviour.agent.SetDestination(enemyBehaviour.gameObject.transform.position);

                randNum = Random.Range(0, 2);
                if (randNum == 0)
                {
                    enemyBehaviour.gameObject.transform.position = escapePos;
                }

                once1 = false;
                once2 = false;

                return stateShaman;
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
