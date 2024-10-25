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

    [SerializeField] private TMP_Text buffDescriptionText;
    [SerializeField] private TMP_Text priceText;

    private CanvasMainMenu mainMenu;
    private ShopTab currentTab = ShopTab.None;

    private void OnEnable()
    {
        ShopItemUI.ShopItemSelected += OnShopItemSelected;
    }

    private void OnDisable()
    {
        ShopItemUI.ShopItemSelected -= OnShopItemSelected;
    }

    private void OnShopItemSelected(ShopItem shopItem)
    {
        if (shopItem is ShopItem<WeaponType>)
        {
            ShopItem<WeaponType> weaponItem = shopItem as ShopItem<WeaponType>;
            Debug.Log(weaponItem.type + "Selected");
        }

        buffDescriptionText.text = shopItem.GetBuffDescription();
        priceText.text = shopItem.price.ToString();
    }

    public void OnInit(CanvasMainMenu mainMenu)
    {
        this.mainMenu = mainMenu;
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
                    //weaponItemUI.SetImage(weaponItem.sprite);
                    weaponItemUI.OnInit(weaponItem);
                }
                break;
            }
        }

        currentTab = tab;
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