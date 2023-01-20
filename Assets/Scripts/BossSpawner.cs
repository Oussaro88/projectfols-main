using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public PoolingManager poolingManager;
    public GameObject bossGate;

    public GameObject bossSpawner;
    public GameObject chestSpawner;

    public GameObject boss_Warrior;
    public GameObject boss_Shaman;

    public GameObject bossInstant;
    public GameObject bossPooled;

    public GameObject treasureChest;
    public GameObject chestInstant;
    public GameObject chestPooled;

    public Bounds boundBox;

    public bool isPooling = false;
    public bool isWarrior = false;
    public bool isShaman = false;

    public bool spawnOnce = false; //used in GameManager for NewLevel
    public float timer = 0;


    public void SpawnBoss()
    {
        spawnOnce = true;
        if (!isPooling)
        {
            if (isWarrior)
            {
                bossInstant = Instantiate(boss_Warrior, bossSpawner.transform.position, bossSpawner.transform.rotation);
            }
            else if (isShaman)
            {
                bossInstant = Instantiate(boss_Shaman, bossSpawner.transform.position, bossSpawner.transform.rotation);
            }
            bossInstant.gameObject.GetComponent<EnemyBehaviour>().SetBoundBox(boundBox); 
            bossInstant.gameObject.GetComponent<EnemyMain>().waveSpawnerObject = gameObject; 
        }
        else if (isPooling)
        {
            if (isWarrior)
            {
                bossPooled = poolingManager.callGoblinWarrior();
            }
            else if (isShaman)
            {
                bossPooled = poolingManager.callGoblinShaman();
            }
            bossPooled.transform.position = bossSpawner.transform.position;
            bossPooled.transform.rotation = bossSpawner.transform.rotation;
            bossPooled.SetActive(true);
            bossPooled.GetComponent<EnemyMain>().InitializeEnemy();
            bossPooled.GetComponent<EnemyMain>().waveSpawnerObject = gameObject;
            bossPooled.GetComponent<EnemyBehaviour>().InitializeBehaviour();
            bossPooled.GetComponent<EnemyBehaviour>().SetBoundBox(boundBox);
        }
    }

    public void SpawnTreasureChest()
    {
        if (isPooling)
        {
            chestPooled = poolingManager.callTreasureChest();
            chestPooled.SetActive(true);
            chestPooled.transform.position = chestSpawner.transform.position;
            chestPooled.transform.rotation = chestSpawner.transform.rotation;
            chestPooled.GetComponent<LootBox>().Boss1Defeated = true;
        }
        else
        {
            chestInstant = Instantiate(treasureChest, chestSpawner.transform.position, chestSpawner.transform.rotation);
            chestInstant.GetComponent<LootBox>().Boss1Defeated = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        poolingManager = PoolingManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        //if (!spawnOnce)
        //{
        //    timer += Time.deltaTime;

        //    if (timer >= 5f)
        //    {
        //        timer = 0;
        //        spawnOnce = true;
        //        SpawnBoss();
        //    }
        //}
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(boundBox.center, boundBox.size);
    }
}
