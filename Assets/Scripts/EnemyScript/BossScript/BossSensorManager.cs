using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSensorManager : MonoBehaviour
{
    /* 0 5 1
     * 6 4 7
     * 2 8 3 */

    public static BossSensorManager instance = null;

    public bool[] sensorFoundPlayer;
    public bool[] sensorFoundBoss;
    public int foundID = 0;
    public int foundPlayer = 0;
    public int foundBoss = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }

    public void ReceiveSensorResultPlayer(bool found, int sensorID)
    {
        sensorFoundPlayer[sensorID] = found;
    }

    public void ReceiveSensorResultBoss(bool found, int sensorID)
    {
        sensorFoundBoss[sensorID] = found;
    }

    public int CallSensorResultPlayer()
    {
        foundID = 4;
        for (int i = 0; i < sensorFoundPlayer.Length; i++)
        {
            if(sensorFoundPlayer[i] == true)
            {
                foundID = i;
            }
        }

        return foundID;
    }
    public int CallSensorResultBoss()
    {
        foundID = 4;
        for (int i = 0; i < sensorFoundBoss.Length; i++)
        {
            if (sensorFoundBoss[i] == true)
            {
                foundID = i;
            }
        }

        return foundID;
    }

    public bool CheckPlayerDistanceFar()
    {
        foundPlayer = CallSensorResultPlayer();
        foundBoss = CallSensorResultBoss();

        if (foundPlayer < 4 && foundBoss < 4 && foundPlayer != foundBoss)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
