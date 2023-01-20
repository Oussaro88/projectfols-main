using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell_FireArrow : BaseSpell
{
    public GameObject burn;

    public override void InitializeSpell()
    {
        burn.GetComponent<SpellDamageManager>().InitializeSpellEffect(30);
    }

    public override void ShowSpellEffect(GameObject[] list, GameObject zone)
    {
        throw new System.NotImplementedException();
    }


    public override void StartSpell()
    {
        InitializeSpell();
        start = true;
        timer = 0f;
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
