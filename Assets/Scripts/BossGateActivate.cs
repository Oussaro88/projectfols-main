using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGateActivate : MonoBehaviour
{
    public GameObject boss_GoblinWarrior;
    public GameObject boss_GoblinShaman;

    public bool isWarrior;
    public bool isShaman;

    public bool once = false;

    public bool start = false; //Test

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerEntity>())
        {
            if (!once)
            {
                once = true;
                ContactBoss();
            }
        }
    }

    public void ContactBoss()
    {
        if (isWarrior)
        {
            boss_GoblinWarrior.GetComponent<EnemyBehaviour>().StartBossAttack();
        }
        else if (isShaman)
        {
            boss_GoblinShaman.GetComponent<EnemyBehaviour>().StartBossAttack();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            start = false;
            ContactBoss();
        }
    }
}
