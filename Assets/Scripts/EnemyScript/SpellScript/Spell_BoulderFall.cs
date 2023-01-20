using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell_BoulderFall : BaseSpell
{
    public GameObject boulderProjectile;
    public GameObject boulderSpawn;

    public bool once1 = false;
    public bool once2 = false;

    public void showBoulderFall()
    {
        boulderProjectile.SetActive(true);
        boulderProjectile.GetComponent<Rigidbody>().velocity = Vector3.zero;
        boulderProjectile.transform.position = boulderSpawn.transform.position;
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
    }

    public void HideZone(GameObject[] list)
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

    public override void ShowSpellEffect(GameObject[] list, GameObject zone)
    {
        zone.SetActive(true);

        if (list == spellZone)
        {
            zone.GetComponent<SpellDamageManager>().InitializeSpellEffect(90);
        }
    }

    public override void StartSpell()
    {
        InitializeSpell();
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

            if (timer >= 0f && !once1)
            {
                once1 = true;
                ShowZone(warningZone);
                showBoulderFall();
            }

            if (timer >= 2f && !once2)
            {
                once2 = true;
                HideZone(warningZone);
                ShowZone(spellZone);
                HideBoulderFall();
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
