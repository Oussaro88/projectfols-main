using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Striker { player, enemy }

public class BaseMelee : MonoBehaviour
{
    private GameManager gameManager;
    public PoolingManager poolingManager;
    //private AchievementManager achievementManager;
    public Striker striker;
    public bool canDmg = false;
    public GameObject otherObject;
    public int dmg;
    public GameObject hitEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (striker == Striker.enemy && other.gameObject.GetComponent<PlayerEntity>() && canDmg)
        {
            if (other.gameObject.GetComponent<PlayerEntity>().GetCurrentHP > 0)
            {
                if (!other.gameObject.GetComponent<PlayerEntity>().IsUsingShield)
                {
                    if (gameManager.player.GetComponent<PlayerEntity>().isInvincible)
                    {
                        other.gameObject.GetComponent<PlayerEntity>().Animator.SetBool("Knocked", false);
                        other.gameObject.GetComponent<PlayerEntity>().OnHurt(0);
                        canDmg = false;
                    }
                    else
                    {
                        other.gameObject.GetComponent<PlayerEntity>().OnHurt(dmg);
                        other.gameObject.GetComponent<PlayerEntity>().isKnocked = true;
                        other.gameObject.GetComponent<PlayerEntity>().Animator.SetBool("Knocked", true);
                        hitEffect = poolingManager.callMeleeVFX();
                        hitEffect.SetActive(true);
                        hitEffect.transform.position = new Vector3(other.transform.position.x, gameObject.transform.position.y, other.transform.position.z);
                        hitEffect.GetComponent<EnemyHitVFX>().StartVFX();
                        canDmg = false;
                    }
                }
            }
        }

        //if (striker == Striker.player && other.gameObject.GetComponent<EnemyMain>())
        //{
        //    if (gameManager.player.GetComponent<PlayerEntity>().HasUsedMelee)
        //    {
        //        other.gameObject.GetComponent<EnemyMain>().OnHurt(gameManager.player.GetComponent<PlayerEntity>()._currentMeleeDamage);
        //        gameManager.player.GetComponent<PlayerEntity>().HasUsedMelee = false;
        //    }

        //    if (gameManager.player.GetComponent<PlayerEntity>().hasRequestedSlash)
        //    {
        //        gameManager.slashImpact.Play();
        //        other.gameObject.GetComponent<EnemyMain>().OnHurt(gameManager.player.GetComponent<PlayerEntity>()._currentSlashDamage);
        //        gameManager.player.GetComponent<PlayerEntity>().hasRequestedSlash = false;
        //    }
        //}

        if (striker == Striker.player && other.gameObject.GetComponent<EnemyMain>())
        {
            if (gameManager.meleeHasBeenUsed)
            {
                if (!gameManager.player.GetComponent<PlayerEntity>().damagedEnemiesList.Contains(other.gameObject))
                {
                    gameManager.player.GetComponent<PlayerEntity>().damagedEnemiesList.Add(other.gameObject);
                    other.gameObject.GetComponent<EnemyMain>().OnHurt(gameManager.player.GetComponent<PlayerEntity>()._currentMeleeDamage);
                    //gameManager.player.GetComponent<PlayerEntity>().HasUsedMelee = false;
                    //this.gameObject.GetComponent<BoxCollider>().enabled = false;
                    if (other.gameObject.GetComponent<EnemyMain>().GetCurrentHP <= 0)
                    {
                        gameManager.player.GetComponent<PlayerEntity>().damagedEnemiesList.Remove(other.gameObject);
                    }
                }
            }

            if (gameManager.slashHasBeenUsed)
            {
                if (!gameManager.player.GetComponent<PlayerEntity>().damagedEnemiesList.Contains(other.gameObject))
                {
                    //gameManager.slashImpact.Play();
                    gameManager.player.GetComponent<PlayerEntity>().damagedEnemiesList.Add(other.gameObject);
                    other.gameObject.GetComponent<EnemyMain>().OnHurt(gameManager.player.GetComponent<PlayerEntity>()._currentSlashDamage);
                    gameManager.player.GetComponent<PlayerEntity>().hasRequestedSlash = false;
                    if (other.gameObject.GetComponent<EnemyMain>().GetCurrentHP <= 0)
                    {
                        gameManager.player.GetComponent<PlayerEntity>().damagedEnemiesList.Remove(other.gameObject);
                    }
                }
            }
        }
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if (striker == Striker.player && other.gameObject.GetComponent<EnemyMain>())
    //    {
    //        this.gameObject.GetComponent<Collider>().enabled = false;
    //        other.gameObject.GetComponent<EnemyMain>().OnHurt(0);
    //        gameManager.player.GetComponent<PlayerEntity>().HasUsedMelee = false;
    //        gameManager.player.GetComponent<PlayerEntity>().hasRequestedSlash = false;
    //    }

    //    //otherObject = other.gameObject;
    //    //if (gameManager.player.GetComponent<PlayerEntity>().HasUsedMelee)
    //    //{
    //    //    if (gameManager.player.GetComponent<PlayerEntity>().damagedEnemiesList.Contains(otherObject))
    //    //    {
    //    //        otherObject.GetComponent<EnemyMain>().OnHurt(0);
    //    //    }
    //    //}

    //    //if (gameManager.player.GetComponent<PlayerEntity>().hasRequestedSlash)
    //    //{
    //    //    if (gameManager.player.GetComponent<PlayerEntity>().damagedEnemiesList.Contains(otherObject))
    //    //    {
    //    //        otherObject.GetComponent<EnemyMain>().OnHurt(0);
    //    //    }
    //    //}
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (striker == Striker.player && other.gameObject.GetComponent<EnemyMain>())
    //    {
    //        this.gameObject.GetComponent<Collider>().enabled = false;
    //        gameManager.player.GetComponent<PlayerEntity>().HasUsedMelee = false;
    //        gameManager.player.GetComponent<PlayerEntity>().hasRequestedSlash = false;
    //    }
    //}

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
        poolingManager = PoolingManager.instance;
        //achievementManager = AchievementManager.Instance;
        canDmg = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if (gameManager.player.GetComponent<PlayerEntity>().resetMeleeInputTimer >= 0.3f)
        //if (gameManager.player.GetComponent<PlayerEntity>().meleeTime >= 0.3f)


        if (gameManager.testTimer >= 0.6f)
        {
            gameManager.meleeHasBeenUsed = false;
            if (gameManager.player.GetComponent<PlayerEntity>().damagedEnemiesList.Count > 0)
            {
                gameManager.player.GetComponent<PlayerEntity>().damagedEnemiesList.Clear();
            }
        }

        //if (gameManager.player.GetComponent<PlayerEntity>().resetSlashInputTimer >= 0.5f)
        if (gameManager.testTimer2 >= 0.6f)
        {
            gameManager.slashHasBeenUsed = false;
            if (gameManager.player.GetComponent<PlayerEntity>().damagedEnemiesList.Count > 0)
            {
                gameManager.player.GetComponent<PlayerEntity>().damagedEnemiesList.Clear();
            }
        }


        //if (striker == Striker.player && (gameManager.player.GetComponent<PlayerEntity>().HasUsedMelee || gameManager.player.GetComponent<PlayerEntity>().hasRequestedSlash) || gameManager.inventoryscript.keys >= 1)
        //{
        //    this.gameObject.GetComponent<Collider>().enabled = true;
        //}
        //else if (striker == Striker.player && !gameManager.player.GetComponent<PlayerEntity>().HasUsedMelee && !gameManager.player.GetComponent<PlayerEntity>().hasRequestedSlash)
        //{
        //    this.gameObject.GetComponent<Collider>().enabled = false;
        //}

    }

}
