using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopAssets : MonoBehaviour
{
    private static ShopAssets shopA;

    public static ShopAssets sa
    {
        get
        {
            if(shopA == null)
            {
                shopA = (Instantiate(Resources.Load("ShopAssets")) as GameObject).GetComponent<ShopAssets>();
            }
            return shopA;
        }
    }

    public Sprite s_HealthPotion;
    public Sprite s_ManaPotion;
}
