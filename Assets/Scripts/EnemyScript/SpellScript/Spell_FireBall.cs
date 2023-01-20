using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell_FireBall : BaseSpell
{
    public GameObject burn;
    public GameObject blast;

    public override void InitializeSpell()
    {
        burn.GetComponent<SpellDamageManager>().InitializeSpellEffect(10);
    }

    public override void ShowSpellEffect(GameObject[] list, GameObject zone)
    {
        throw new System.NotImplementedException();
    }


    public override void StartSpell()
    {
        InitializeSpell();
        start = true;
        burn.SetActive(true);
        blast.SetActive(false);
        timer = 0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerEntity>())
        {
            burn.SetActive(false);
            blast.SetActive(true);
            blast.GetComponent<SpellDamageManager>().InitializeSpellEffect(20);
        }
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            timer += Time.deltaTime;

            if (timer >= 3f)
            {
                timer = 0;
                start = false;
                ReturnOrigin(); 
                //this.gameObject.SetActive(false);
            }
        }
    }
}
