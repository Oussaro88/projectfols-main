using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProjectileType { sphere, cube, capsule, fire }
public class ProjectileManager : MonoBehaviour
{
    public ProjectileType projectileType;

    private ParticleSystem vfx;
    private bool vfxDestroyed;

    private GameManager gameManager;


    // Start is called before the first frame update
    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.gameObject.GetComponent<PlayerEntity>())
    //    {
    //        if (!other.gameObject.GetComponent<PlayerEntity>().isStealingAttack)
    //        { 
    //            other.gameObject.GetComponent<PlayerEntity>().GetCurrentHP -= 5; 
    //        }
            
    //        if (other.gameObject.GetComponent<PlayerEntity>().isStealingAttack && !other.gameObject.GetComponent<PlayerEntity>().hasReturnedAttack) // A REVOIR
    //        {
    //            other.gameObject.GetComponent<PlayerEntity>().GetCurrentHP -= 5;
    //        }
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        //gameObject.SetActive(false);
        vfxDestroyed = true;
        if (vfxDestroyed)
        {
            //Instantiate(gameManager.fireBurstVfx, transform.position, transform.rotation);
            GameObject obj = Instantiate(gameManager.explosion, transform.position, transform.rotation);
            //Collider[] hitEnemies = Physics.OverlapSphere(transform.position, 1.5f, LayerMask.GetMask("Enemy"));
            
            //foreach (Collider enemy in hitEnemies)
            //{
            //    enemy.gameObject.GetComponent<EnemyMain>().canHurt = true;
            //    enemy.gameObject.GetComponent<EnemyMain>().OnHurt(20);
            //}
            Destroy(obj, 2);
        }
    }    
    
    //private void OnParticleCollision(GameObject other)
    //{
    //    Destroy(gameObject);
    //    //gameObject.SetActive(false);
    //    vfxDestroyed = true;
    //    if (vfxDestroyed)
    //    {
    //        Instantiate(gameManager.fireBurstVfx, transform.position, transform.rotation);
    //    }
    //}

    // Update is called once per frame
    void Update()
    {

    }
}
