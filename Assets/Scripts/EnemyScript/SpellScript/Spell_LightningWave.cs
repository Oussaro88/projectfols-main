using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell_LightningWave : BaseSpell
{
    public bool once1 = false;
    public bool once2 = false;
    public bool once3 = false;
    public bool once4 = false;
    public bool once5 = false;
    public bool once6 = false;

    public void ShowFirstWave(GameObject[] list)
    {
        ShowSpellEffect(list, list[0]);
    }

    public void ShowOtherWave(GameObject[] list, int i, int j)
    {
        list[i].SetActive(false);

        ShowSpellEffect(list, list[j]);
    }

    public void HideFifthWave(GameObject[] list)
    {
        list[4].SetActive(false);
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
        once5 = false;
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
                ShowOtherWave(warningZone, 0, 1);
                ShowFirstWave(spellZone);
            }

            if (timer >= 2f && !once3)
            {
                once3 = true;
                ShowOtherWave(warningZone, 1, 2);
                ShowOtherWave(spellZone, 0, 1);
            }

            if (timer >= 3f && !once4)
            {
                once4 = true;
                ShowOtherWave(warningZone, 2, 3);
                ShowOtherWave(spellZone, 1, 2);
            }

            if (timer >= 4f && !once5)
            {
                once5 = true;
                ShowOtherWave(warningZone, 3, 4);
                ShowOtherWave(spellZone, 2, 3);
            }

            if (timer >= 5f && !once6)
            {
                once6 = true;
                HideFifthWave(warningZone);
                ShowOtherWave(spellZone, 3, 4);
            }

            if (timer >= 6f)
            {
                timer = 0;
                start = false;
                once1 = false;
                once2 = false;
                once3 = false;
                once4 = false;
                once5 = false;
                once6 = false;
                HideFifthWave(spellZone);
                ReturnOrigin(); 
                //this.gameObject.SetActive(false);
            }
        }
    }
}