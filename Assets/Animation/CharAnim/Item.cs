using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public enum ItemType
    {
        HealthPotion,
        ManaPotion
    }

    public static int GetPrice(ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.HealthPotion: return 500;
            case ItemType.ManaPotion: return 500;
        }
    }

    public static Sprite GetSprite(ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.HealthPotion: return ShopAssets.sa.s_HealthPotion;
            case ItemType.ManaPotion: return ShopAssets.sa.s_ManaPotion;
        }
    }
}
