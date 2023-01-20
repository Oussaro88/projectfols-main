using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRange : EnemyMain
{
    public bool isGhost;
    public bool canAttack;
    private float timer;
    public int randNum;
    public GameObject coin;
    public bool onceDeath = false;

    private AchievementManager achievementManager;

    public override void InitializeEnemy() 
    {
        gameManager = GameManager.instance;
        achievementManager = AchievementManager.Instance;
        //posOrigin = transform;
        GetCurrentHP = GetMaxHP;
        canAttack = true; 
        timer = 0;
        onceDeath = false;
        GetComponent<SpawnLoot>().spawned = false;
        cameraMain = gameManager.cameraMain;
        canvas.gameObject.SetActive(true);
        //GetComponent<EnemyBehaviour>().InitializeBehaviour();
    }

    public override void OnDeath()
    {
        if(!onceDeath)
        {
            onceDeath = true;
            achievementManager.GoblinRangedKilled();
            canvas.gameObject.SetActive(false);
            GetComponent<SpawnLoot>().spawned = true;
            waveSpawnerObject.GetComponent<WaveSpawner>().EnemyCount(-1);
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
    }
}
