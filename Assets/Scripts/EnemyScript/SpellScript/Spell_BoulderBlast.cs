using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell_BoulderBlast : BaseSpell
{
    public GameObject boulderProjectile;
    public GameObject boulderSpawn;

    public bool once1 = false;
    public bool once2 = false;

    public void showBoulderBlast()
    {
        //0boulderProjectile.SetActive(true);
        //boulderProjectile.GetComponent<Rigidbody>().velocity = Vector3.zero;
        //boulderProjectile.transform.position = boulderSpawn.transform.position;
        boulderProjectile.GetComponent<SpellDamageManager>().InitializeSpellEffect(30);
    }

    public void HideBoulderFall()
    {
        boulderProjectile.GetComponent<Rigidbody>().velocity = Vector3.zero;
        boulderProjectile.SetActive(false);
    }

    public void ShowZone(GameObject[] list)
    {
        ShowSpellEffect(list, list[0]);
        ShowSpellEffect(list, list[1]);
        ShowSpellEffect(list, list[2]);
        ShowSpellEffect(list, list[3]);
        ShowSpellEffect(list, list[4]);
        ShowSpellEffect(list, list[5]);
        ShowSpellEffect(list, list[6]);
        ShowSpellEffect(list, list[7]);
        ShowSpellEffect(list, list[8]);
    }

    public void HideZone(GameObject[] list)
    {
        list[0].SetActive(false);
        list[1].SetActive(false);
        list[2].SetActive(false);
        list[3].SetActive(false);
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

    public override void ShowSpellEffect(GameObject[] list, GameObject zone)
    {
        zone.SetActive(true);

        if (list == spellZone)
        {
            if (zone == list[0])
            {
                zone.GetComponent<SpellDamageManager>().InitializeSpellEffect(25);
            }
            else
            {
                zone.GetComponent<SpellDamageManager>().InitializeSpellEffect(15);
            }
        }
    }

    public override void StartSpell()
    {
        InitializeSpell();
        boulderProjectile.GetComponent<SpellDamageManager>().InitializeSpellEffect(50);
        start = true;
        timer = 0;
        once1 = false;
        once2 = false;
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

            if (timer >= 0.5f && !once1)
            {
                once1 = true;
                ShowZone(warningZone);
            }

            if (timer >= 1.5f && !once2)
            {
                once2 = true;
                HideZone(warningZone);
                ShowZone(spellZone);
            }

            if (timer >= 3f)
            {
                HideZone(spellZone);
                start = false;
                once1 = false;
                once2 = false;
                timer = 0;
                ReturnOrigin();
            }
        }
    }
}
