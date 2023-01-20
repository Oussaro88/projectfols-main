using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFight : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange = 0.6f;
    public LayerMask enemiesLayer;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.GetComponent<PlayerEntity>().HasUsedMelee)
        {
            Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemiesLayer);

            foreach(Collider enemy in hitEnemies)
            {
                enemy.gameObject.GetComponent<EnemyMain>().canHurt = true;
                enemy.gameObject.GetComponent<EnemyMain>().OnHurt(this.gameObject.GetComponent<PlayerEntity>()._currentMeleeDamage);
                this.gameObject.GetComponent<PlayerEntity>().HasUsedMelee = false;
            }
        }
        else if (this.gameObject.GetComponent<PlayerEntity>().hasRequestedSlash)
        {
            Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemiesLayer);

            foreach (Collider enemy in hitEnemies)
            {
                enemy.gameObject.GetComponent<EnemyMain>().canHurt = true;
                //gameManager.slashImpact.Play();
                enemy.gameObject.GetComponent<EnemyMain>().OnHurt(this.gameObject.GetComponent<PlayerEntity>()._currentSlashDamage);
                this.gameObject.GetComponent<PlayerEntity>().hasRequestedSlash = false;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
