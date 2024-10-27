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

        switch(tab)
        {
            case ShopTab.Weapon:
            {
                foreach (ShopItem<WeaponType> weaponItem in shopData.WeaponList)
                {
                    ShopItemUI weaponItemUI = Instantiate(shopItemUIPref, contentShopItemTF);
                    weaponItemUI.OnInit(weaponItem);
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