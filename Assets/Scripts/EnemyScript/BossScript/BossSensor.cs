using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSensor : MonoBehaviour
{
    public BossSensorManager sensorManager;
    public int sensorID;

    public bool enter;
    public bool exit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerEntity>())
        {
            sensorManager.ReceiveSensorResultPlayer(true, sensorID);
        } 
        else if (other.GetComponent<BaseMelee>()) //Because Weapon is Detected and Character is Not Detected, due to RigidBody. Need to fix that
        {
            sensorManager.ReceiveSensorResultBoss(true, sensorID);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerEntity>())
        {
            sensorManager.ReceiveSensorResultPlayer(false, sensorID);
        }
        else if (other.GetComponent<BaseMelee>())
        {
            sensorManager.ReceiveSensorResultBoss(false, sensorID);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        sensorManager = BossSensorManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if(enter)
        {
            enter = false;
            sensorManager.GetComponent<BossSensorManager>().ReceiveSensorResultBoss(true, sensorID);
        }
        if (exit)
        {
            exit = false;
            sensorManager.GetComponent<BossSensorManager>().ReceiveSensorResultBoss(false, sensorID);
        }
    }
}
