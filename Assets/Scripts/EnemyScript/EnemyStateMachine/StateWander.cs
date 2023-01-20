using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateWander : EnemyState
{
    public StatePursue statePursue; 
    public Vector3 wanderPos;
    public float wanderDistance = 0;
    public float playerDistance = 0;
    public bool once = false;
    public bool found = false;
    public float randomX;
    public float randomZ;
    public LayerMask mask;

    public override EnemyState RunState(EnemyBehaviour enemyBehaviour)
    {
        if (!once)
        {
            mask = LayerMask.GetMask("Ground");
            enemyBehaviour.agent.speed = 1f;
            enemyBehaviour.agent.acceleration = 2f;
            enemyBehaviour.enemyAnim.SetBool("IsWalking", true);
            once = true;
        }

        if (wanderDistance <= enemyBehaviour.agent.stoppingDistance) 
        {
            found = false;
            do //To Prevent WanderPos being Outside of NavMesh
            {
                randomX = Random.Range(enemyBehaviour.boundBox.center.x - enemyBehaviour.boundBox.extents.x + enemyBehaviour.agent.radius,
                    enemyBehaviour.boundBox.center.x + enemyBehaviour.boundBox.extents.x - enemyBehaviour.agent.radius);
                randomZ = Random.Range(enemyBehaviour.boundBox.center.z - enemyBehaviour.boundBox.extents.z + enemyBehaviour.agent.radius,
                    enemyBehaviour.boundBox.center.z + enemyBehaviour.boundBox.extents.z - enemyBehaviour.agent.radius);
                wanderPos = new Vector3(randomX, transform.position.y, randomZ);

                NavMeshHit hit;
                if (NavMesh.SamplePosition(wanderPos, out hit, 1f, NavMesh.AllAreas))
                {
                    if (Physics.Raycast(wanderPos, -transform.up, 2f, mask))
                    {
                        wanderPos = hit.position;
                        found = true;
                    }
                }
            } while (!found);

            /*randomX = Random.Range(enemyBehaviour.boundBox.center.x - enemyBehaviour.boundBox.extents.x + enemyBehaviour.agent.radius, 
                enemyBehaviour.boundBox.center.x + enemyBehaviour.boundBox.extents.x - enemyBehaviour.agent.radius); 
            randomZ = Random.Range(enemyBehaviour.boundBox.center.z - enemyBehaviour.boundBox.extents.z + enemyBehaviour.agent.radius, 
                enemyBehaviour.boundBox.center.z + enemyBehaviour.boundBox.extents.z - enemyBehaviour.agent.radius); 
            wanderPos = new Vector3(randomX, transform.position.y, randomZ);
            */
            enemyBehaviour.agent.SetDestination(wanderPos); 
            enemyBehaviour.agent.isStopped = false;
        }

        wanderDistance = Vector3.Distance(wanderPos, transform.position);

        playerDistance = Vector3.Distance(transform.position, enemyBehaviour.player.transform.position);

        //----- ----- Condition To Go To Script StatePursue ----- -----
        if (playerDistance <= 10f) 
		{
            wanderDistance = 0;
            playerDistance = 0;
            once = false;
            return statePursue; 
        }
		return this;
	}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(wanderPos, 0.3f);
    }
}
