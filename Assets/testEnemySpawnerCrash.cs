using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testEnemySpawnerCrash : MonoBehaviour
{
    public PoolingManager poolingManager;
    public GameObject enemySpawned;
    public Bounds boundBox;

    public bool spawnGhost = false;
    public bool spawnMelee = false;
    public bool spawnRange = false;

    public bool start = false;

    public void SpawnEnemy()
    {
        if(spawnGhost)
        {
            enemySpawned = poolingManager.callGhostRange();
        } 
        else if (spawnMelee)
        {
            enemySpawned = poolingManager.callGoblinMelee();
        } 
        else if (spawnRange)
        {
            enemySpawned = poolingManager.callGoblinRange();
        } 

        enemySpawned.transform.position = gameObject.transform.position;
        enemySpawned.transform.rotation = gameObject.transform.rotation;
        enemySpawned.SetActive(true);
        enemySpawned.GetComponent<EnemyMain>().InitializeEnemy();
        //enemySpawned.GetComponent<EnemyMain>().waveSpawnerObject = gameObject;
        enemySpawned.GetComponent<EnemyMain>().isPooling = true;
        enemySpawned.GetComponent<EnemyBehaviour>().InitializeBehaviour();
        enemySpawned.GetComponent<EnemyBehaviour>().SetBoundBox(boundBox);
        //enemySpawned.SetActive(true);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(boundBox.center, boundBox.size);
    }

    // Start is called before the first frame update
    void Start()
    {
        poolingManager = PoolingManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            start = false;
            SpawnEnemy();
        }
    }
}
