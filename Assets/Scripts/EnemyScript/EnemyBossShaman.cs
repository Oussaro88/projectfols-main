using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossShaman : EnemyMain
{
    public bool onceDeath = false;
    public bool die = false;
    //private AchievementManager achievementManager;

    public override void InitializeEnemy()
    {
        gameManager = GameManager.instance;
        //achievementManager = AchievementManager.Instance;
        //posOrigin = transform;
        GetCurrentHP = GetMaxHP;
        onceDeath = false;
        GetComponent<SpawnLoot>().spawned = false;
        cameraMain = gameManager.cameraMain;
        canvas.gameObject.SetActive(true);
        //GetComponent<EnemyBehaviour>().InitializeBehaviour();
    }

    public override void OnDeath()
    {
        if (!onceDeath)
        {
            //AchievementManager.Instance.goblinsKilled += 1;
            onceDeath = true;
            canvas.gameObject.SetActive(false);
            GetComponent<SpawnLoot>().spawned = true;
            //waveSpawnerObject.GetComponent<WaveSpawner>().EnemyCount(-1);
            waveSpawnerObject.GetComponent<BossSpawner>().SpawnTreasureChest();
            GetComponent<EnemyBehaviour>().SwitchStateDeath();
        }
    }

    public override void DisplayHealthBar()
    {
        canvas.transform.LookAt(cameraMain.transform);
        slider.value = GetCurrentHP * 100 / GetMaxHP;
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        InitializeEnemy();

    }

    // Update is called once per frame
    protected override void Update()
    {
        DisplayHealthBar();

        if (die)
        {
            die = false;
            GetCurrentHP = 0;
            OnDeath();
        }
    }

}