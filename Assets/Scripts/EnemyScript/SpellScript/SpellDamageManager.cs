using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Caster { player, enemy }

public class SpellDamageManager : MonoBehaviour
{
    public Caster caster;
    public int dmg = 0;
    public bool canDmg = false;

    public void InitializeSpellEffect(int dmgSpell)
    {
        dmg = dmgSpell;
        canDmg = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (caster == Caster.player && other.gameObject.GetComponent<EnemyMain>())
        {
            //other.gameObject.GetComponent<EnemyMain>().OnHurt(40); //FIRE BURST is instanciated twice
        }
        else if (caster == Caster.enemy && other.gameObject.GetComponent<PlayerEntity>())
        {
            if (other.gameObject.GetComponent<PlayerEntity>().GetCurrentHP > 0)
            {
                if (!other.gameObject.GetComponent<PlayerEntity>().IsUsingShield && !other.gameObject.GetComponent<PlayerEntity>().isInvincible)
                {
                    if (canDmg)
                    {
                        other.gameObject.GetComponent<PlayerEntity>().OnHurt(dmg);
                        other.gameObject.GetComponent<PlayerEntity>().isKnocked = true;
                        other.gameObject.GetComponent<PlayerEntity>().Animator.SetBool("Knocked", true);
                        canDmg = false;
                    }
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
