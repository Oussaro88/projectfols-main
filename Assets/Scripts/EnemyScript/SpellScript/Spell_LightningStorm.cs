using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell_LightningStorm : BaseSpell
{
    public bool once1 = false;
    public bool once2 = false;
    public bool once3 = false;
    public bool once4 = false;

    public void ShowZone(GameObject[] list, int i)
    {
        ShowSpellEffect(list, list[i]);
    }

    public void HideZone(GameObject[] list, int i)
    {
        list[i].SetActive(false);
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
        once2 = false;
        once3 = false;
        once4 = false;
    }

    public override void ShowSpellEffect(GameObject[] list, GameObject zone)
    {
        zone.SetActive(true);

        if (list == spellZone)
        {
            zone.GetComponent<SpellDamageManager>().InitializeSpellEffect(90);
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
                ShowZone(warningZone, 0);
                ShowZone(spellZone, 0);
            }

            if (timer >= 0.5f && !once2)
            {
                once2 = true;
                HideZone(spellZone, 0);
                ShowZone(spellZone, 1);
            }

            if (timer >= 1f && !once3)
            {
                once3 = true;
                HideZone(spellZone, 1);
                ShowZone(spellZone, 2);
            }

            if (timer >= 1.5f && !once4)
            {
                once4 = true;
                HideZone(warningZone, 0);
                HideZone(spellZone, 2);
                ShowZone(spellZone, 3);
            }

            if (timer >= 2f)
            {
                timer = 0;
                start = false;
                once1 = false;
                once2 = false;
                once3 = false;
                once4 = false;
                HideZone(spellZone, 3);
                ReturnOrigin();
            }
        }
    }
}