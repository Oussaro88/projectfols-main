using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell_FireBomb : BaseSpell
{
    //public GameObject burn;
    public bool once1 = false;
    public bool once2 = false;

    public void ShowBlast(GameObject[] list)
    {
        ShowSpellEffect(list, list[0]);
    }

    public void HideBlast(GameObject[] list)
    {
        list[0].SetActive(false);
    }

    public override void InitializeSpell()
    {
        for (int i = 0; i < warningZone.Length; i++)
        {
            warningZone[i].SetActive(false);
        }

        for (int i = 0; i < spellZone.Length; i++)
        {
            spellZone[i].SetActive(false);
        }
    }

    public override void StartSpell()
    {
        InitializeSpell();
        start = true;
        timer = 0;
        once1 = false;
    }

    public override void ShowSpellEffect(GameObject[] list, GameObject zone)
    {
        zone.SetActive(true);

        if (list == spellZone)
        {
            zone.GetComponent<SpellDamageManager>().InitializeSpellEffect(50);
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

            if (timer >= 0f && !once1)
            {
                once1 = true;
                ShowBlast(warningZone);
                //burn.SetActive(true);
                //burn.GetComponent<SpellDamageManager>().InitializeSpellEffect(20);
            }

            if (timer >= 2f && !once2)
            {
                once2 = true;
                //burn.SetActive(false);
                HideBlast(warningZone);
                ShowBlast(spellZone);
            }

            if(timer >= 3f)
            {
                HideBlast(spellZone);
                start = false;
                once1 = false;
                once2 = false;
                timer = 0;
                ReturnOrigin(); 
                //this.gameObject.SetActive(false);
            }
        }
    }
}