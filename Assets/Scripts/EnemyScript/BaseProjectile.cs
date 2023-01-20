using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Shooter { player, enemy }

public enum RangeWeapon { Sphere, Arrow, Lance }

public class BaseProjectile : MonoBehaviour
{
    public PoolingManager poolingManager;

    public Shooter shooter;
    public RangeWeapon typeRange;

    public Vector3 posOrigin;
    public bool isPooling;
    public bool startOnce = true;

    public int dmg = 5;
    public bool canDmg;
    public bool canDmgSphere;
    public bool canDmgArrow;
    public bool canDmgLance;

    public bool useRange = false;
    public float timer;

    public GameObject hitEffect;

    private void Awake()
    {
        if (isPooling)
        {
            posOrigin = this.gameObject.transform.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (shooter == Shooter.player && other.gameObject.GetComponent<EnemyMain>())
        {
            other.gameObject.GetComponent<EnemyMain>().OnHurt(40); //FIRE BURST is instanciated twice
        }
        else if (shooter == Shooter.enemy)
        {
            if (other.gameObject.GetComponent<PlayerEntity>())
            {
                if (other.gameObject.GetComponent<PlayerEntity>().GetCurrentHP > 0)
                {
                    if (!other.gameObject.GetComponent<PlayerEntity>().IsUsingShield && !other.gameObject.GetComponent<PlayerEntity>().isInvincible)
                    {
                        switch (typeRange)
                        {
                            case RangeWeapon.Sphere:
                                if (other.gameObject.GetComponent<PlayerEntity>() && canDmgSphere)
                                {
                                    //Direct Damage
                                    //other.gameObject.GetComponent<Player>().Hp -= 20;
                                    //Indirect Damage - Player near Explosion from Sphere
                                    //other.gameObject.GetComponent<Player>().Hp -= 10;
                                    other.gameObject.GetComponent<PlayerEntity>().OnHurt(dmg);
                                    other.gameObject.GetComponent<PlayerEntity>().isKnocked = true;
                                    other.gameObject.GetComponent<PlayerEntity>().Animator.SetBool("Knocked", true);

                                    hitEffect = poolingManager.callRangeVFX();
                                    hitEffect.SetActive(true);
                                    hitEffect.transform.position = gameObject.transform.position;
                                    hitEffect.GetComponent<EnemyHitVFX>().StartVFX();

                                    canDmg = false;

                                    if (isPooling)
                                    {
                                        SetInactiveRange();
                                    }
                                    else
                                    {
                                        Destroy(gameObject);
                                    }
                                }
                                Invoke("SetInactiveRange", 1f);
                                break;
                            case RangeWeapon.Arrow:
                                if (other.gameObject.GetComponent<PlayerEntity>() && canDmgArrow)
                                {
                                    //other.gameObject.GetComponent<Player>().Hp -= 30;
                                    other.gameObject.GetComponent<PlayerEntity>().OnHurt(dmg);
                                    other.gameObject.GetComponent<PlayerEntity>().isKnocked = true;
                                    other.gameObject.GetComponent<PlayerEntity>().Animator.SetBool("Knocked", true);

                                    hitEffect = poolingManager.callRangeVFX();
                                    hitEffect.SetActive(true);
                                    hitEffect.transform.position = gameObject.transform.position;
                                    hitEffect.GetComponent<EnemyHitVFX>().StartVFX();

                                    canDmg = false;
                                }
                                Invoke("SetInactiveRange", 1f);
                                break;
                            case RangeWeapon.Lance:
                                if (other.gameObject.GetComponent<PlayerEntity>() && canDmgLance)
                                {
                                    //other.gameObject.GetComponent<Player>().Hp -= 40;
                                    other.gameObject.GetComponent<PlayerEntity>().OnHurt(dmg);
                                    other.gameObject.GetComponent<PlayerEntity>().isKnocked = true;
                                    other.gameObject.GetComponent<PlayerEntity>().Animator.SetBool("Knocked", true);

                                    hitEffect = poolingManager.callRangeVFX();
                                    hitEffect.SetActive(true);
                                    hitEffect.transform.position = gameObject.transform.position;
                                    hitEffect.GetComponent<EnemyHitVFX>().StartVFX();

                                    canDmg = false;
                                }
                                Invoke("SetInactiveRange", 1f);
                                break;
                        }
                    }
                }
            }
        }
        /*else
        {
            //SetInactiveRange();
            Destroy(gameObject);
        }*/

    }

    public void SetInactiveRange() //For Pooling, Destroy for Instantiate
    {
        this.gameObject.transform.position = posOrigin; 
        this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        canDmg = true;
        gameObject.SetActive(false); 
    }

    // Start is called before the first frame update
    void Start()
    {
        poolingManager = PoolingManager.instance; 

        canDmg = true;
        canDmgSphere = true;
        canDmgArrow = true;
        canDmgLance = true;

        if (startOnce && shooter == Shooter.enemy)
        {
            posOrigin = gameObject.transform.position;
            startOnce = false;
            gameObject.SetActive(false);
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        if (useRange)
        {
            timer += Time.deltaTime;

            if (timer >= 2f)
            {
                timer = 0;
                useRange = false;
                SetInactiveRange();
            }
        }
    }
}
