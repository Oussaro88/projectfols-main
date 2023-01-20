using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPathPointManager : MonoBehaviour
{
    /* 0 5 1
     * 6 4 7
     * 2 8 3 */

    public static BossPathPointManager instance = null;

    public BossSensorManager sensorManager;
    public int foundBossID = 0;
    public int foundPlayerID = 0;

    public GameObject[] pointGroupTL; //Sensor Top Left - 0
    public GameObject[] pointGroupTR; //Sensor Top Right - 1
    public GameObject[] pointGroupBL; //Sensor Bottom Left - 2
    public GameObject[] pointGroupBR; //Sensor Bottom Right - 3
    public GameObject[] pointGroupCT; //Sensor Center - 4
    public GameObject[] pointGroupOT; //Outside Sensor Top - 5
    public GameObject[] pointGroupOL; //Outside Sensor Left - 6
    public GameObject[] pointGroupOR; //Outside Sensor Right - 7
    public GameObject[] pointGroupOB; //Outside Sensor Bottom - 8

    public int randNum1 = 0;
    public int randNum2 = 0;

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

    public GameObject CallPathPointMove()
    {
        foundBossID = sensorManager.CallSensorResultBoss();
        
        if (foundBossID < 4) //Go to Higher Zone
        {
            randNum1 = Random.Range(4, 9); //Choosing Point Group
            
            if (randNum1 != 4)
            {
                randNum2 = Random.Range(0, 6); //Choosing Path Point in Point Group Outer
            }
            else if (randNum1 == 4)
            {
                randNum2 = Random.Range(0, 12); //Choosing Path Point in Point Group Center
            }
        } 
        else if (foundBossID > 4) //Go to Lower Zone
        {
            randNum1 = Random.Range(0, 5);
            
            if (randNum1 != 4)
            {
                randNum2 = Random.Range(0, 9); //Choosing Path Point in Point Group Corner
            }
            else if (randNum1 == 4)
            {
                randNum2 = Random.Range(0, 12); //Choosing Path Point in Point Group Center
            }
        }
        else if (foundBossID == 4) //Go Anywhere
        {
            do
            {
                randNum1 = Random.Range(0, 9);
            } while (randNum1 == 4);

            if (randNum1 < 4)
            {
                randNum2 = Random.Range(0, 9); //Choosing Path Point in Point Group Corner
            }
            else if (randNum1 > 4)
            {
                randNum2 = Random.Range(0, 6); //Choosing Path Point in Point Group Outer
            }
        }

        switch(randNum1)
        {
            case 0:
                return pointGroupTL[randNum2];
            case 1:
                return pointGroupTR[randNum2];
            case 2:
                return pointGroupBL[randNum2];
            case 3:
                return pointGroupBR[randNum2];
            case 4:
                return pointGroupCT[randNum2];
            case 5:
                return pointGroupOT[randNum2];
            case 6:
                return pointGroupOL[randNum2];
            case 7:
                return pointGroupOR[randNum2];
            case 8:
                return pointGroupOB[randNum2];
            default:
                return pointGroupCT[4];
        }

        //return pointGroupCT[4];
    }

    public GameObject CallPathPointAttack(bool rand, int index)
    {
        foundPlayerID = sensorManager.CallSensorResultPlayer();
        
        if (rand)
        {
            if (foundPlayerID < 4)
            {
                randNum1 = Random.Range(0, 4);
                randNum2 = Random.Range(0, 9);
            }
            else if (foundPlayerID > 4)
            {
                randNum1 = Random.Range(5, 9);
                randNum2 = Random.Range(0, 6);
            }
            else if (foundPlayerID == 4)
            {
                randNum1 = Random.Range(0, 9);

                if(randNum1 < 4)
                {
                    randNum2 = Random.Range(0, 9);
                }
                else if (randNum1 > 4)
                {
                    randNum2 = Random.Range(0, 6);
                }
                else
                {
                    randNum2 = Random.Range(0, 12);
                }
            }

            switch (randNum1)
            {
                case 0:
                    return pointGroupTL[randNum2];
                case 1:
                    return pointGroupTR[randNum2];
                case 2:
                    return pointGroupBL[randNum2];
                case 3:
                    return pointGroupBR[randNum2];
                case 4:
                    return pointGroupCT[randNum2];
                case 5:
                    return pointGroupOT[randNum2];
                case 6:
                    return pointGroupOL[randNum2];
                case 7:
                    return pointGroupOR[randNum2];
                case 8:
                    return pointGroupOB[randNum2];
                default:
                    return pointGroupCT[4];
            }
        }
        else
        {
            switch (foundPlayerID)
            {
                case 0:
                    return pointGroupTL[index];
                case 1:
                    return pointGroupTR[index];
                case 2:
                    return pointGroupBL[index];
                case 3:
                    return pointGroupBR[index];
                case 4:
                    return pointGroupCT[index];
                case 5:
                    return pointGroupOT[index];
                case 6:
                    return pointGroupOL[index];
                case 7:
                    return pointGroupOR[index];
                case 8:
                    return pointGroupOB[index];
                default:
                    return pointGroupCT[4];
            }
        }

        //return pointGroupCT[4];
    }

    public GameObject CallPathPointCharge()
    {
        foundPlayerID = sensorManager.CallSensorResultPlayer();

        randNum2 = Random.Range(0, 9);

        switch(foundPlayerID)
        {
            case 0:
                return pointGroupTL[randNum2];
            case 1:
                return pointGroupTR[randNum2];
            case 2:
                return pointGroupBL[randNum2];
            case 3:
                return pointGroupBR[randNum2];
            default:
                return pointGroupCT[4];
        }

        //return pointGroupCT[4];
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
