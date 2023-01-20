using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class EnemyMain : EnemyEntity
{
    //There is currentHP and MaxHP in BaseEntity
    //public int hp;
    //public int GetCurrentHP;
    //public int hpMax; 
    //public int dmg;

    public Vector3 posOrigin;
    public bool startOnce = true;

    //Need ATK, DEF & SPD?
    public GameManager gameManager;
    public GameObject waveSpawnerObject;
    public Animator animState;
    public Canvas canvas;
    public Slider slider;
    public GameObject cameraMain;
    public bool isPooling = false;
    //public GameObject drop;
    public bool canHurt = true;

    public ParticleSystem meleeImpact;
    public ParticleSystem slashImpact;

    //Don't know if I need those
    /*public int Hp { get => hp; set => hp = value; }
    public int HpMax { get => hpMax; set => hpMax = value; }
    public int Dmg { get => dmg; set => dmg = value; }*/

    public abstract void InitializeEnemy();
    //public abstract void AttackPlayer();
    //public abstract void OnAttack();
    //public abstract void IsAttacking();
    //public abstract void VerifyDeath(); 
    //public abstract void OnDeath();
    //public abstract void DropItem(); //Not Needed since Coin Existed
    //public abstract void DropCoin();
    public abstract void DisplayHealthBar();

    private void Awake()
    {
        /*if (isPooling)
        {
            posOrigin = this.gameObject.transform.position;
        }*/
    }

    protected override void Start()
    {
        if (startOnce)
        {
            posOrigin = gameObject.transform.position; 
            startOnce = false;
            gameObject.SetActive(false);
        }
    }

    public void ReturnOrigin()
    {
        gameObject.transform.position = posOrigin;
    }

    public override void OnHurt(int damage)
    {
        if (canHurt)
        {
            //if (damage != 0)
            //{
                //canHurt = false;
                GetCurrentHP -= damage;
            //canHurt = false;
            if (gameManager.slashHasBeenUsed)
            {
                slashImpact.Play();
            }

            else if (gameManager.meleeHasBeenUsed)
            {
                meleeImpact.Play();
            }

            if (GetCurrentHP <= 0)
            {
                OnDeath();
            }
            else
            {
                GetComponent<EnemyBehaviour>().SwitchStateKnocked();
            }
            //}
        }
    }
}
