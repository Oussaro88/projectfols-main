using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable 
{
    public abstract void OnDeath();
    public virtual void OnHurt(int damage) { }
}
