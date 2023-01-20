using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MeleeWeapon { Knife, Sword, Spear, Hammer }

public class EnemyMelee : EnemyMain
{
    public bool canAttack; 
    public GameObject[] melee = new GameObject[2];
    public Animator meleeAnim;
    //public Animator animState;
    public MeleeWeapon typeMelee;
    private float timer;
    private int randNum;
    public GameObject coin;
    public bool onceDeath = false;

    private AchievementManager achievementManager;

    public override void InitializeEnemy() 
    {
        gameManager = GameManager.instance;
        achievementManager = AchievementManager.Instance;
        GetCurrentHP = GetMaxHP;
        canAttack = true; 
        timer = 0;
        onceDeath = false; 
        GetComponent<SpawnLoot>().spawned = false;
        cameraMain = gameManager.cameraMain;
        canvas.gameObject.SetActive(true);
        RandomWeapon(); 
    }

    public void RandomWeapon()
    {
        randNum = Random.Range(0, 2);
        switch (randNum)
        {
            case 0:
                melee[0].SetActive(true);
                melee[1].SetActive(false);
                break;
            default:
                melee[0].SetActive(false);
                melee[1].SetActive(true);
                break;
        }
    }

    public GameObject SendWeaponUsed()
    {
        return melee[randNum];
    }

    public void CanDamage()
    {
        melee[randNum].GetComponent<BaseMelee>().canDmg = true;
    }

    public void CannotDamage()
    {
        melee[randNum].GetComponent<BaseMelee>().canDmg = false;
    }

    public override void OnDeath()
    {
        if (!onceDeath)
        {
            AchievementManager.Instance.GoblinMeleeKilled();
            //transform.position = posOrigin.position;
            onceDeath = true;
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
