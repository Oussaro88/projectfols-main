using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePursue_GoblinWarrior : StatePursue
{
	public GameObject pathPoint;
	public float pathDistance;

	public override EnemyState RunState(EnemyBehaviour enemyBehaviour)
	{
		if (!once)
		{
			enemyBehaviour.agent.speed = 2f;
			enemyBehaviour.agent.acceleration = 4f;
			enemyBehaviour.enemyAnim.SetBool("IsWalking", true);
			enemyBehaviour.enemyAnim.SetTrigger("IsWalkingTrigger");
			once = true;
			timer = 0;
			pathPoint = enemyBehaviour.pathManager.CallPathPointMove();
			enemyBehaviour.agent.SetDestination(pathPoint.transform.position);
		}

		playerDistance = Vector3.Distance(transform.position, enemyBehaviour.player.transform.position);
		pathDistance = Vector3.Distance(transform.position, pathPoint.transform.position);

		//----- ----- Condition To Go To Script StateAttack ----- -----
		if (enemyBehaviour.gameObject.GetComponent<EnemyBossWarrior>() && stateWarrior)
		{
			if (playerDistance <= 8f || pathDistance <= enemyBehaviour.agent.stoppingDistance)
			{
				enemyBehaviour.agent.SetDestination(transform.position); 
				enemyBehaviour.enemyAnim.SetBool("IsWalking", false);
				once = false;
				return stateWarrior;
			}
		}

		if (!enemyBehaviour.enemyAnim.GetCurrentAnimatorStateInfo(0).IsName("MWalking"))
		{
			timer += Time.deltaTime;

			if (timer >= 1f)
			{
				timer = 0;
				once = false;
			}
		}

		return this;
	}



}
