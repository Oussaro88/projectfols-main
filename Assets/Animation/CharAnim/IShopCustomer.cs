using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShopCustomer
{
    void BuyItem(Item.ItemType itemType);
    bool SpendCoin(int coinAmount);
}
