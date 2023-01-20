using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData 
{
    private GameManager gameManager = GameManager.instance;

    //Inventory script currencies data
    public int coinsCount;
    public int gemsCount;
    public int keysCount;

    //Player data
    public Vector3 playerPos;
    public int hpData;
    public float manaData;
    public float staminaData;

    //Objectives data
    public SerializableDictionary<string, bool> objectiveTriggered;
    public string objectiveMission;

    //Doors data
    public SerializableDictionary<string, bool> doorsTriggered;

    //Scene data
    public int sceneIndex;

    //Waves Spawners data
    public SerializableDictionary<string, bool> wavesSpawner;

    //Pentagrams data
    public SerializableDictionary<string, bool> pentagramActivated;

    //Colliders Enclosing Fight data
    public SerializableDictionary<string, bool> collidersFight;

    //Portal data
    public bool unlockedNewLevel;

    //NewLevel Boss Gate data
    public bool bossOpen;

    public GameData()
    {
        this.coinsCount = 0;
        this.gemsCount = 0;
        this.keysCount = 0;

        this.playerPos = gameManager.player.GetComponent<PlayerEntity>().transform.position;
        this.hpData = gameManager.player.GetComponent<PlayerEntity>().GetMaxHP;
        this.manaData = gameManager.player.GetComponent<PlayerEntity>().GetMaxMana;
        this.staminaData = gameManager.player.GetComponent<PlayerEntity>().GetMaxStamina;

        objectiveTriggered = new SerializableDictionary<string, bool>();
        this.objectiveMission = null;

        doorsTriggered = new SerializableDictionary<string, bool>();

        this.sceneIndex = 0;

        wavesSpawner = new SerializableDictionary<string, bool>();

        pentagramActivated = new SerializableDictionary<string, bool>();

        collidersFight = new SerializableDictionary<string, bool>();

        unlockedNewLevel = false;

        bossOpen = false;
    }
}
