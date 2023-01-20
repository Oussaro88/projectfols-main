using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalEntity : BaseEntity
{

    private float movementSpeed;
    private int attackPower;
    private int defensePower;

    public float GetMovementSpeed { get => movementSpeed; set => movementSpeed = value; }
    public int GetAttackPower { get => attackPower; set => attackPower = value; }
    public int GetDefensePower { get => defensePower; set => defensePower = value; }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        base.GetCurrentHP = base.GetMaxHP;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    public override void OnDeath()
    {
        //What happens when Physical Entities die by default (Animations? Destroy GameObject? Drop Items?)
        //Subclasses can override this method as well to define their own OnDeath()
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

    public virtual void OnHeal()
    {
        //Define heal
    }
    public virtual void OnAttack()
    {
        //What happens when Physical Entities attack by default
    }

    public virtual void OnBlock()
    {
        //Define how Physical Entities defend by default
    }

    public virtual void OnChangeSpeed()
    {
        //In what situation do Physical Entities change their movement speed by default
    }

    public virtual void Move()
    {
        //How do Physical Entities move by default
    }
}
