using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEntity : PhysicalEntity
{
    private Transform playerTarget;

    [SerializeField] private float distance;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        Move();
    }

    public override void OnDeath()
    {
        //Enemies disappear after an animation maybe and drop items
    }

    public override void OnHurt(int damage)
    {
        //Default definition for now
        base.GetCurrentHP -= damage;
        if (base.GetCurrentHP <= 0)
        {
            OnDeath();
        }
    }

    public override void OnHeal()
    {
        //If the enemies can heal, else we can remove this method or override this method in one of its subclasses if only some specific enemies can heal
    }

    public override void OnAttack()
    {
        //What happens when Enemies attack by default
    }

    public override void OnBlock()
    {
        //If enemies can block attacks, else we can remove this method or override this method in one of its subclasses if only some specific enemies can block
    }

    public override void OnChangeSpeed()
    {
        //Maybe when player is in range, change their movement speed 
    }

    public override void Move()
    {
        //How Enemies move by default
        if(Vector3.Distance(transform.position, playerTarget.position) < distance)
        {
            OnChangeSpeed();
            transform.position = Vector3.MoveTowards(transform.position, playerTarget.position, base.GetMovementSpeed * Time.deltaTime);
        }

        //Else maybe walking randomly or patrolling some places while returning their movement speed to default
    }
}
