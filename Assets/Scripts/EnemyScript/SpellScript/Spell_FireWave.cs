using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell_FireWave : BaseSpell
{
    public bool once1 = false;
    public bool once2 = false;
    public bool once3 = false;
    public bool once4 = false;

    public void ShowFirstWave(GameObject[] list)
    {
        ShowSpellEffect(list, list[0]);
    }

    public void ShowSecondWave(GameObject[] list)
    {
        list[0].SetActive(false);

        ShowSpellEffect(list, list[1]);
        ShowSpellEffect(list, list[2]);
        ShowSpellEffect(list, list[3]);
    }

    public void ShowThirdWave(GameObject[] list)
    {
        list[1].SetActive(false);
        list[2].SetActive(false);
        list[3].SetActive(false);

        ShowSpellEffect(list, list[4]);
        ShowSpellEffect(list, list[5]);
        ShowSpellEffect(list, list[6]);
        ShowSpellEffect(list, list[7]);
        ShowSpellEffect(list, list[8]);
    }

    public void HideThirdWave(GameObject[] list)
    {
        list[4].SetActive(false);
        list[5].SetActive(false);
        list[6].SetActive(false);
        list[7].SetActive(false);
        list[8].SetActive(false);
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
                ShowFirstWave(warningZone);
            }

            if (timer >= 1f && !once2)
            {
                once2 = true;
                ShowSecondWave(warningZone);
                ShowFirstWave(spellZone);
            }

            if (timer >= 2f && !once3)
            {
                once3 = true;
                ShowThirdWave(warningZone);
                ShowSecondWave(spellZone);
            }

            if (timer >= 3f && !once4)
            {
                once4 = true;
                HideThirdWave(warningZone);
                ShowThirdWave(spellZone);
            }

            if (timer >= 4f)
            {
                timer = 0;
                start = false;
                once1 = false;
                once2 = false;
                once3 = false;
                once4 = false;
                HideThirdWave(spellZone);
                ReturnOrigin(); 
                //this.gameObject.SetActive(false);
            }
        }
    }
}
