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
    [SerializeField] private SelectButton equipUI;
    [SerializeField] private GameObject buyUI;
    [SerializeField] private SelectButton weaponTab;
    [SerializeField] private SelectButton HatTab;
    [SerializeField] private SelectButton PantTab;
    [SerializeField] private TMP_Text priceText;

    private CanvasMainMenu mainMenu;
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
        LevelManager.Ins.cam.OnShopping();
    }

    public void CloseShop()
    {
        Close(0);
        mainMenu.ShowMainUI();
        LevelManager.Ins.cam.OnPreviewing();
        SoundManager.Ins.OnButtonClick();
    }

    public void OpenWeaponShop()
    {
        ClearCurrentListShopItemUI();
        UnSelectAllTabUI();
        weaponTab.Select();
        foreach (ShopItem<WeaponType> weaponItem in shopData.WeaponList)
        {
            AddItemToShopUIList(weaponItem);
        }
        SoundManager.Ins.OnButtonClick();
    }

    public void OpenHatShop()
    {
        ClearCurrentListShopItemUI();
        UnSelectAllTabUI();
        HatTab.Select();
        foreach (ShopItem<HatType> hatItem in shopData.HatList)
        {
            AddItemToShopUIList(hatItem);
        }
        SoundManager.Ins.OnButtonClick();
    }

    public void OpenPantShop()
    {
        ClearCurrentListShopItemUI();
        UnSelectAllTabUI();
        PantTab.Select();
        foreach (ShopItem<PantsType> pantItem in shopData.PantsList)
        {
            AddItemToShopUIList(pantItem);
        }
        SoundManager.Ins.OnButtonClick();
    }

    public void UnSelectAllTabUI()
    {
        weaponTab.UnSelect();
        HatTab.UnSelect();
        PantTab.UnSelect();
    }

    public void Purchase()
    {
        if (!UserDataManager.Ins.IsPurchaseable(currentShopItemUI.shopItem.price)) return;
        currentShopItemUI.Purchase();
        SetupOptionOfShopItemUI(currentShopItemUI);
        UpdateMoneyText();
        SoundManager.Ins.OnButtonClick();
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
        SetupPlayerCustom();
        SoundManager.Ins.OnButtonClick();
    }

    public void SetupPlayerCustom()
    {
        LevelManager.Ins.player.SetupWeapon();
        LevelManager.Ins.player.SetupHat();
        LevelManager.Ins.player.SetupPant();
    }

    private void UpdateMoneyText()
    {
        moneyText.text = UserDataManager.Ins.GetMoneyAmount().ToString();
    }

    private void OnShopItemSelected(ShopItemUI shopItemUI)
    {
        if (currentShopItemUI != null)
        {
            currentShopItemUI.UnSelected();
        }

        currentShopItemUI = shopItemUI;

        SoundManager.Ins.OnButtonClick();
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
                equipUI.Select();
                equipedShopItemUI = shopItemUI;
            }
            else
            {
                equipUI.UnSelect();
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
