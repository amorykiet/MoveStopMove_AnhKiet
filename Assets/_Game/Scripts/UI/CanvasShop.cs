using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasShop : UICanvas
{
    [SerializeField] private ShopData shopData;
    [SerializeField] private ShopItemUI shopItemUIPref;
    [SerializeField] private Transform contentShopItemTF;

    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private TMP_Text buffDescriptionText;

    [SerializeField] private GameObject equipUI;
    [SerializeField] private GameObject buyUI;

    [SerializeField] private TMP_Text priceText;
    [SerializeField] private TMP_Text equipText;

    private CanvasMainMenu mainMenu;
    private ShopTab currentTab = ShopTab.None;
    private ShopItemUI currentShopItemUI = null;
    private ShopItemUI equipedShopItemUI = null;
    private List<ShopItemUI> currentListUI = new List<ShopItemUI>();

    private void OnEnable()
    {
        ShopItemUI.ShopItemUISelected += OnShopItemSelected;
    }

    private void OnDisable()
    {
        ShopItemUI.ShopItemUISelected -= OnShopItemSelected;
    }

    public void OnInit(CanvasMainMenu mainMenu)
    {
        this.mainMenu = mainMenu;
        UpdateMoneyText();
        OpenWeaponShop();
    }

    public void CloseShop()
    {
        Close(0);
        mainMenu.ShowMainUI();
    }

    public void OpenWeaponShop()
    {
        ChangeTab(ShopTab.Weapon);
    }

    public void OpenHatShop()
    {
        ChangeTab(ShopTab.Hat);
    }

    public void OpenPantShop()
    {
        ChangeTab(ShopTab.Pants);
    }

    public void Purchase()
    {
        if (!UserDataManager.Ins.IsPurchaseable(currentShopItemUI.shopItem.price)) return;
        currentShopItemUI.Purchase();
        SetupOptionOfShopItemUI(currentShopItemUI);
        UpdateMoneyText();
    }

    public void Equip()
    {
        if (currentShopItemUI != equipedShopItemUI)
        {
            currentShopItemUI.Equip();
            equipedShopItemUI.UnEquip();
        }
        SetupOptionOfShopItemUI(currentShopItemUI);
        SetupOptionOfShopItemUI(equipedShopItemUI);
    }

    private void UpdateMoneyText()
    {
        moneyText.text = UserDataManager.Ins.GetMoneyAmount().ToString();
    }

    private void ChangeTab(ShopTab tab)
    {
        if (tab == currentTab) return;
        
        ClearCurrentListShopItemUI();
        switch(tab)
        {
            case ShopTab.Weapon:
            {
                foreach (ShopItem<WeaponType> weaponItem in shopData.WeaponList)
                {
                    AddItemToShopUIList(weaponItem);
                }
                break;
            }

            case ShopTab.Hat:
            {
                foreach (ShopItem<HatType> hatItem in shopData.HatList)
                {
                    AddItemToShopUIList(hatItem);
                }
                break;
            }

            case ShopTab.Pants:
            {
                foreach (ShopItem<PantsType> pantItem in shopData.PantsList)
                {
                    AddItemToShopUIList(pantItem);
                }
                break;
            }
        }

        currentTab = tab;
    }


    private void OnShopItemSelected(ShopItemUI shopItemUI)
    {
        if (currentShopItemUI != null)
        {
            currentShopItemUI.UnSelected();
        }

        currentShopItemUI = shopItemUI;

        SetupOptionOfShopItemUI(shopItemUI);
    }

    private void SetupOptionOfShopItemUI(ShopItemUI shopItemUI)
    {
        ShopItem shopItem = shopItemUI.shopItem;

        if (shopItemUI.isPurchased)
        {
            equipUI.gameObject.SetActive(true);
            buyUI.gameObject.SetActive(false);

            if (shopItemUI.isEquipped)
            {
                equipText.text = Constants.EQUIPPED_OPTION;
                equipedShopItemUI = shopItemUI;
            }
            else
            {
                equipText.text = Constants.EQUIP_OPTION;
            }

        }
        else
        {
            equipUI.gameObject.SetActive(false);
            buyUI.gameObject.SetActive(true);
            priceText.text = shopItem.price.ToString();
        }

        buffDescriptionText.text = shopItem.GetBuffDescription();
    }

    private void ClearCurrentListShopItemUI()
    {
        foreach (ShopItemUI itemUI in currentListUI)
        {
            Destroy(itemUI.gameObject);
        }
        currentListUI.Clear();
    }

    private void AddItemToShopUIList(ShopItem _itemUI)
    {
        ShopItemUI itemUI = Instantiate(shopItemUIPref, contentShopItemTF);
        itemUI.OnInit(_itemUI);
        currentListUI.Add(itemUI);
    }
}

enum ShopTab
{
    None = 0,
    Weapon = 1,
    Hat = 2,
    Pants = 3,
    FullSet = 4,
    Shields = 5
}