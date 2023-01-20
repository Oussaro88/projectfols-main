using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingManager : MonoBehaviour
{
    //Pooling System Instance
    public static PoolingManager instance = null;
    public static PoolingManager Instance { get => instance; set => instance = value; }

    public bool isPooling = false;
    public GameObject[] waveSpawnerList = new GameObject[2];
    public GameObject bossSpawner;

    //Enemy Character
    public GameObject[] goblinMeleeList = new GameObject[6];
    public GameObject[] goblinRangeList = new GameObject[6];
    public GameObject[] ghostRangeList = new GameObject[6];
    public GameObject goblinWarriorBoss;
    public GameObject goblinShamanBoss;
    //public GameObject golemBoss;

    //Enemy Range Attack
    public GameObject[] rSphereList = new GameObject[8];
    public GameObject[] rArrowList = new GameObject[4];
    public GameObject[] rLanceList = new GameObject[4];

    //Enemy Magic Attack
    public GameObject[] fireBallList = new GameObject[8];
    public GameObject[] fireBombList = new GameObject[4];
    public GameObject[] fireWallList = new GameObject[4];
    public GameObject[] fireFloorList = new GameObject[4];
    public GameObject[] fireWaveList = new GameObject[3];
    public GameObject[] fireMeteorList = new GameObject[12];
    public GameObject[] lightningStrikeList = new GameObject[3];
    public GameObject[] lightningFieldList = new GameObject[3];
    public GameObject[] lightningWaveList = new GameObject[3];
    public GameObject[] lightningStormList = new GameObject[12];
    public GameObject[] earthQuakeList = new GameObject[3];
    public GameObject[] earthQuakeV2List = new GameObject[3];
    public GameObject[] earthStompList = new GameObject[3];
    public GameObject[] earthWaveList = new GameObject[3];
    public GameObject[] boulderThrowList = new GameObject[8];
    public GameObject[] boulderFallList = new GameObject[12];
    public GameObject[] boulderBlastList = new GameObject[6];

    //Enemy VFX
    public GameObject[] spawnVFXList = new GameObject[8];
    public GameObject[] meleeVFXList = new GameObject[6];
    public GameObject[] rangeVFXList = new GameObject[6];

    //TreasureChest
    public GameObject[] treasureChestList = new GameObject[4];

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

    public void SetIsPooling(GameObject obj)
    {
        if (isPooling)
        {
            if (obj.GetComponent<EnemyMain>())
            {
                obj.GetComponent<EnemyMain>().isPooling = true;
            } 
            else if (obj.GetComponent<BaseProjectile>())
            {
                obj.GetComponent<BaseProjectile>().isPooling = true;
            }
            else if (obj.GetComponent<BaseSpell>())
            {
                obj.GetComponent<BaseSpell>().isPooling = true;
            }
        }
    }

    //Do I Need to Create a List for Confirmation, to prevent the same GameObject being called at the same time?

    public GameObject callGoblinMelee()
    {
        for (int i = 0; i < goblinMeleeList.Length; i++)
        {
            if (!goblinMeleeList[i].activeInHierarchy)
            {
                //Initialize
                SetIsPooling(goblinMeleeList[i]);
                return goblinMeleeList[i];
            }
        }

        SetIsPooling(goblinMeleeList[0]);
        return goblinMeleeList[0];
    }

    public GameObject callGoblinRange()
    {
        for (int i = 0; i < goblinRangeList.Length; i++)
        {
            if (!goblinRangeList[i].activeInHierarchy)
            {
                SetIsPooling(goblinRangeList[i]);
                return goblinRangeList[i];
            }
        }

        SetIsPooling(goblinRangeList[0]);
        return goblinRangeList[0];
    }

    public GameObject callGhostRange()
    {
        for (int i = 0; i < ghostRangeList.Length; i++)
        {
            if (!ghostRangeList[i].activeInHierarchy)
            {
                SetIsPooling(ghostRangeList[i]);
                return ghostRangeList[i];
            }
        }

        SetIsPooling(ghostRangeList[0]);
        return ghostRangeList[0];
    }

    public GameObject callGoblinWarrior()
    {
        SetIsPooling(goblinWarriorBoss);
        return goblinWarriorBoss;
    }

    public GameObject callGoblinShaman()
    {
        SetIsPooling(goblinShamanBoss);
        return goblinShamanBoss;
    }

    public GameObject callRangeSphere()
    {
        for (int i = 0; i < rSphereList.Length; i++)
        {
            if (!rSphereList[i].activeInHierarchy)
            {
                SetIsPooling(rSphereList[i]);
                return rSphereList[i];
            }
        }

        SetIsPooling(rSphereList[0]);
        return rSphereList[0];
    }

    public GameObject callRangeArrow()
    {
        for (int i = 0; i < rArrowList.Length; i++)
        {
            if (!rArrowList[i].activeInHierarchy)
            {
                SetIsPooling(rArrowList[i]);
                return rArrowList[i];
            }
        }

        SetIsPooling(rArrowList[0]);
        return rArrowList[0];
    }

    public GameObject callRangeLance()
    {
        for (int i = 0; i < rLanceList.Length; i++)
        {
            if (!rLanceList[i].activeInHierarchy)
            {
                SetIsPooling(rLanceList[i]);
                return rLanceList[i];
            }
        }

        SetIsPooling(rLanceList[0]);
        return rLanceList[0];
    }

    public GameObject callFireBall()
    {
        for (int i = 0; i < fireBallList.Length; i++)
        {
            if (!fireBallList[i].activeInHierarchy)
            {
                SetIsPooling(fireBallList[i]);
                return fireBallList[i];
            }
        }

        SetIsPooling(fireBallList[0]);
        return fireBallList[0];
    }

    public GameObject callFireBomb()
    {
        for (int i = 0; i < fireBombList.Length; i++)
        {
            if (!fireBombList[i].activeInHierarchy)
            {
                SetIsPooling(fireBombList[i]);
                return fireBombList[i];
            }
        }

        SetIsPooling(fireBombList[0]);
        return fireBombList[0];
    }

    public GameObject callFireWall()
    {
        for (int i = 0; i < fireWallList.Length; i++)
        {
            if (!fireWallList[i].activeInHierarchy)
            {
                SetIsPooling(fireWallList[i]);
                return fireWallList[i];
            }
        }

        SetIsPooling(fireWallList[0]);
        return fireWallList[0];
    }

    public GameObject callFireFloor()
    {
        for (int i = 0; i < fireFloorList.Length; i++)
        {
            if (!fireFloorList[i].activeInHierarchy)
            {
                SetIsPooling(fireFloorList[i]);
                return fireFloorList[i];
            }
        }

        SetIsPooling(fireFloorList[0]);
        return fireFloorList[0];
    }

    public GameObject callFireWave()
    {
        for (int i = 0; i < fireWaveList.Length; i++)
        {
            if (!fireWaveList[i].activeInHierarchy)
            {
                SetIsPooling(fireWaveList[i]);
                return fireWaveList[i];
            }
        }

        SetIsPooling(fireWaveList[0]);
        return fireWaveList[0];
    }

    public GameObject callFireMeteor()
    {
        for (int i = 0; i < fireMeteorList.Length; i++)
        {
            if (!fireMeteorList[i].activeInHierarchy)
            {
                SetIsPooling(fireMeteorList[i]);
                return fireMeteorList[i];
            }
        }

        SetIsPooling(fireMeteorList[0]);
        return fireMeteorList[0];
    }

    public GameObject callLightningStrike()
    {
        for (int i = 0; i < lightningStrikeList.Length; i++)
        {
            if (!lightningStrikeList[i].activeInHierarchy)
            {
                SetIsPooling(lightningStrikeList[i]);
                return lightningStrikeList[i];
            }
        }

        SetIsPooling(lightningStrikeList[0]);
        return lightningStrikeList[0];
    }

    public GameObject callLightningField()
    {
        for (int i = 0; i < lightningFieldList.Length; i++)
        {
            if (!lightningFieldList[i].activeInHierarchy)
            {
                SetIsPooling(lightningFieldList[i]);
                return lightningFieldList[i];
            }
        }

        SetIsPooling(lightningFieldList[0]);
        return lightningFieldList[0];
    }

    public GameObject callLightningWave()
    {
        for (int i = 0; i < lightningWaveList.Length; i++)
        {
            if (!lightningWaveList[i].activeInHierarchy)
            {
                SetIsPooling(lightningWaveList[i]);
                return lightningWaveList[i];
            }
        }

        SetIsPooling(lightningWaveList[0]);
        return lightningWaveList[0];
    }

    public GameObject callLightningStorm()
    {
        for (int i = 0; i < lightningStormList.Length; i++)
        {
            if (!lightningStormList[i].activeInHierarchy)
            {
                SetIsPooling(lightningStormList[i]);
                return lightningStormList[i];
            }
        }

        SetIsPooling(lightningStormList[0]);
        return lightningStormList[0];
    }

    public GameObject callEarthQuake()
    {
        for (int i = 0; i < earthQuakeList.Length; i++)
        {
            if (!earthQuakeList[i].activeInHierarchy)
            {
                SetIsPooling(earthQuakeList[i]);
                return earthQuakeList[i];
            }
        }

        SetIsPooling(earthQuakeList[0]);
        return earthQuakeList[0];
    }

    public GameObject callEarthQuakeV2()
    {
        for (int i = 0; i < earthQuakeV2List.Length; i++)
        {
            if (!earthQuakeV2List[i].activeInHierarchy)
            {
                SetIsPooling(earthQuakeV2List[i]);
                return earthQuakeV2List[i];
            }
        }

        SetIsPooling(earthQuakeV2List[0]);
        return earthQuakeV2List[0];
    }

    public GameObject callEarthStomp()
    {
        for (int i = 0; i < earthStompList.Length; i++)
        {
            if (!earthStompList[i].activeInHierarchy)
            {
                SetIsPooling(earthStompList[i]);
                return earthStompList[i];
            }
        }

        SetIsPooling(earthStompList[0]);
        return earthStompList[0];
    }

    public GameObject callEarthWave()
    {
        for (int i = 0; i < earthWaveList.Length; i++)
        {
            if (!earthWaveList[i].activeInHierarchy)
            {
                SetIsPooling(earthWaveList[i]);
                return earthWaveList[i];
            }
        }

        SetIsPooling(earthWaveList[0]);
        return earthWaveList[0];
    }

    public GameObject callBoulderThrow()
    {
        for (int i = 0; i < boulderThrowList.Length; i++)
        {
            if (!boulderThrowList[i].activeInHierarchy)
            {
                SetIsPooling(boulderThrowList[i]);
                return boulderThrowList[i];
            }
        }

        SetIsPooling(boulderThrowList[0]);
        return boulderThrowList[0];
    }
    public GameObject callBoulderFall()
    {
        for (int i = 0; i < boulderFallList.Length; i++)
        {
            if (!boulderFallList[i].activeInHierarchy)
            {
                SetIsPooling(boulderFallList[i]);
                return boulderFallList[i];
            }
        }

        SetIsPooling(boulderFallList[0]);
        return boulderFallList[0];
    }

    public GameObject callBoulderBlast()
    {
        for (int i = 0; i < boulderBlastList.Length; i++)
        {
            if (!boulderBlastList[i].activeInHierarchy)
            {
                SetIsPooling(boulderBlastList[i]);
                return boulderBlastList[i];
            }
        }

        SetIsPooling(boulderBlastList[0]);
        return boulderBlastList[0];
    }

    public GameObject callSpawnVFX()
    {
        for (int i = 0; i < spawnVFXList.Length; i++)
        {
            if (!spawnVFXList[i].activeInHierarchy)
            {
                SetIsPooling(spawnVFXList[i]);
                return spawnVFXList[i];
            }
        }

        SetIsPooling(spawnVFXList[0]);
        return spawnVFXList[0];
    }

    public GameObject callMeleeVFX()
    {
        for (int i = 0; i < meleeVFXList.Length; i++)
        {
            if (!meleeVFXList[i].activeInHierarchy)
            {
                SetIsPooling(meleeVFXList[i]);
                return meleeVFXList[i];
            }
        }

        SetIsPooling(meleeVFXList[0]);
        return meleeVFXList[0];
    }

    public GameObject callRangeVFX()
    {
        for (int i = 0; i < rangeVFXList.Length; i++)
        {
            if (!rangeVFXList[i].activeInHierarchy)
            {
                SetIsPooling(rangeVFXList[i]);
                return rangeVFXList[i];
            }
        }

        SetIsPooling(rangeVFXList[0]);
        return rangeVFXList[0];
    }

    public GameObject callTreasureChest()
    {
        for (int i = 0; i < spawnVFXList.Length; i++)
        {
            if (!treasureChestList[i].activeInHierarchy)
            {
                SetIsPooling(treasureChestList[i]);
                return treasureChestList[i];
            }
        }

        SetIsPooling(treasureChestList[0]);
        return treasureChestList[0];
    }

    // Start is called before the first frame update
    void Start()
    {
        if (isPooling)
        {
            for (int i = 0; i < waveSpawnerList.Length; i++)
            {
                waveSpawnerList[i].GetComponent<WaveSpawner>().isPooling = true;
            }
            bossSpawner.GetComponent<BossSpawner>().isPooling = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
