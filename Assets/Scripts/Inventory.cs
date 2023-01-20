using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour, IDataPersistence
{

    public int coins = 0;
    public GameObject coinsO;
    private TMPro.TextMeshProUGUI coinsText;

    public int gems = 0;
    public GameObject gemsO;
    private TMPro.TextMeshProUGUI gemsText;

    public int keys = 0;
    public GameObject keysO;
    private TMPro.TextMeshProUGUI keysText;

    private bool opened;
    public int lootCollected;

    // Start is called before the first frame update
    void Start()
    {
        //coinsO.AddComponent<TMPro.TextMeshProUGUI>();
        coinsText = coinsO.GetComponent<TMPro.TextMeshProUGUI>();
        //gemsO.AddComponent<TMPro.TextMeshProUGUI>();
        gemsText = gemsO.GetComponent<TMPro.TextMeshProUGUI>();
        //keysO.AddComponent<TMPro.TextMeshProUGUI>();
        keysText = keysO.GetComponent<TMPro.TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        coinsText.text = coins.ToString();
        gemsText.text = gems.ToString();
        keysText.text = keys.ToString();

        if (opened)
        {
            if(lootCollected == 0)
            {
                opened = false;
                StartCoroutine(GetKey());
            }
        }
    }

    public void CoinPickup()
    {
        int ranNum = Random.Range(0,2);

        if (ranNum == 0)
        {
            coins += 10;
        }
        else if (ranNum == 1)
        {
            coins += 15;
        }
        else if (ranNum == 2)
        {
            coins += 20;
        }

        lootCollected -= 1;
    }

    public void GemPickUp()
    {
        gems += 1;
        lootCollected -= 1;
    }

    public void CommonChest()
    {
        StartCoroutine(StartCounting());
        //gems += 1;
        coins += 50;
        //keys += 1;
        //StartCoroutine(GetKey());
    }

    IEnumerator GetKey()
    {
        yield return new WaitForSeconds(1f);
        keys += 1;
    }

    IEnumerator StartCounting()
    {
        yield return new WaitForSeconds(1f);
        opened = true;
    }

    public void DoorOpened()
    {
        keys -= 1;
    }

    public void LoadData(GameData data)
    {
        this.coins = data.coinsCount;
        this.gems = data.gemsCount;
        this.keys = data.keysCount;
    }

    public void SaveData(GameData data)
    {
        data.coinsCount = this.coins;
        data.gemsCount = this.gems;
        data.keysCount = this.keys;
    }
}
