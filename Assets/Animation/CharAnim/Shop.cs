using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour, IBaseMenu
{
    private Transform container;
    private Transform shopItemTemplate;

    private IShopCustomer shopCustomer;

    public AudioClip buy;
    public AudioSource audioCoin;

    private GameManager manager;

    public List<Button> shopB;

    private void Awake()
    {
        container = transform.Find("Container");
        shopItemTemplate = container.Find("ShopItemTemplate");
    }

    // Start is called before the first frame update
    void Start()
    {
        manager = GameManager.Instance;

        CreateItemButton(Item.ItemType.HealthPotion, Item.GetSprite(Item.ItemType.HealthPotion), "Health Potion", Item.GetPrice(Item.ItemType.HealthPotion), 0);
        CreateItemButton(Item.ItemType.ManaPotion, Item.GetSprite(Item.ItemType.ManaPotion), "Mana Potion", Item.GetPrice(Item.ItemType.ManaPotion), 1);
        audioCoin = GetComponent<AudioSource>();
        HideShop();

        NavigateShopItems();
    }

    private void CreateItemButton(Item.ItemType itemType, Sprite itemSprite, string itemName, int itemPrice, int positionIndex)
    {
        Transform shopItemT = Instantiate(shopItemTemplate, container);
        RectTransform shopItemRT = shopItemT.GetComponent<RectTransform>();

        float shopItemHeight = 100f;
        shopItemRT.anchoredPosition = new Vector2(0, -shopItemHeight * positionIndex);

        shopItemT.Find("nameText").GetComponent<TextMeshProUGUI>().SetText(itemName);
        shopItemT.Find("priceText").GetComponent<TextMeshProUGUI>().SetText(itemPrice.ToString());

        shopItemT.Find("itemImage").GetComponent<Image>().sprite = itemSprite;

        shopB.Add(shopItemT.GetComponent<Button>());

        shopItemT.GetComponent<Button>().onClick.AddListener(delegate { BuyShopItem(itemType); });

    }

    private void NavigateShopItems()
    {
        Navigation navigation = shopB[0].GetComponent<Button>().navigation;
        navigation.mode = Navigation.Mode.Explicit;
        navigation.selectOnDown = shopB[1].GetComponent<Button>();
        navigation.selectOnUp = shopB[1].GetComponent<Button>();
        shopB[0].GetComponent<Button>().navigation = navigation;

        Navigation navigation1 = shopB[1].GetComponent<Button>().navigation;
        navigation1.mode = Navigation.Mode.Explicit;
        navigation1.selectOnDown = shopB[0].GetComponent<Button>();
        navigation1.selectOnUp = shopB[0].GetComponent<Button>();
        shopB[1].GetComponent<Button>().navigation = navigation1;
    }

    private void BuyShopItem(Item.ItemType itemType)
    {
        if(itemType == Item.ItemType.HealthPotion && manager.player.GetComponent<PlayerEntity>().GetCurrentHP < manager.player.GetComponent<PlayerEntity>().GetMaxHP)
        {
            if (shopCustomer.SpendCoin(Item.GetPrice(itemType)))
            {
                shopCustomer.BuyItem(itemType);
                audioCoin.PlayOneShot(buy);
            }
        }
        else if (itemType == Item.ItemType.ManaPotion && manager.player.GetComponent<PlayerEntity>().GetCurrentMana < manager.player.GetComponent<PlayerEntity>().GetMaxMana)
        {
            if (shopCustomer.SpendCoin(Item.GetPrice(itemType)))
            {
                shopCustomer.BuyItem(itemType);
                audioCoin.PlayOneShot(buy);
            }
        }

    }

    public void ShowShop(IShopCustomer shopCustomer)
    {
        this.shopCustomer = shopCustomer;
        gameObject.SetActive(true);
        shopB[0].Select();
        MenuON();
    }

    public void HideShop()
    {
        gameObject.SetActive(false);
        MenuOFF();
    }

    public void MenuON()
    {
        manager.player.GetComponent<CharacterController>().enabled = false;
        manager.player.GetComponent<Animator>().enabled = false;
        manager.menuOpened = true;
    }

    public void MenuOFF()
    {
        manager.player.GetComponent<CharacterController>().enabled = true;
        manager.player.GetComponent<Animator>().enabled = true;
        manager.menuOpened = false;
    }
}
