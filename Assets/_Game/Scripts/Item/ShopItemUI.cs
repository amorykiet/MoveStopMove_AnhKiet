using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemUI : MonoBehaviour
{
    public static event Action<ShopItemUI> ShopItemUISelected;

    public ShopItem shopItem;
    public bool isPurchased = false;
    public bool isEquipped = false;

    [SerializeField] private Image image;
    [SerializeField] private Button selectButton;
    [SerializeField] private GameObject selectBorder;
    [SerializeField] private Image lockImage;

    public void OnInit(ShopItem _shopItem)
    {
        shopItem = _shopItem;
        if (UserDataManager.Ins.IsItemPurchased(shopItem))
        {
            isPurchased = true;
            lockImage.gameObject.SetActive(false);
            
            if (UserDataManager.Ins.IsItemEquipped(shopItem))
            {
                isEquipped = true;
                Select();
            }

        }
        this.image.sprite = shopItem.sprite;
    }

    public void Purchase()
    { 
        UserDataManager.Ins.Purchase(shopItem);
        isPurchased = true;
        lockImage.gameObject.SetActive(false);
    }

    public void Equip()
    {
        isEquipped = true;
        UserDataManager.Ins.Equip(shopItem);
    }

    public void UnEquip()
    {
        isEquipped = false;
    }

    public void Select()
    {
        //Preview
        if (shopItem is ShopItem<WeaponType> weapon)
        {
            Weapon weaponPref = ItemManager.Ins.GetWeaponPrefByType(weapon.type);
            LevelManager.Ins.player.SetupWeapon(weaponPref);
        }
        else if (shopItem is ShopItem<HatType> hat)
        {
            Hat hatPref = ItemManager.Ins.GetHatPrefByType(hat.type);
            LevelManager.Ins.player.SetupHat(hatPref);
        }
        else if (shopItem is ShopItem<PantsType> pant)
        {
            Pant pantPref = ItemManager.Ins.GetPantMatByType(pant.type);
            LevelManager.Ins.player.SetupPant(pantPref);
        }
        else if (shopItem is ShopItem<FullSetType> fullSet)
        {
            FullSet fullSetPref = ItemManager.Ins.GetFullSetByType(fullSet.type);
            LevelManager.Ins.player.SetupFullSet(fullSetPref);
        }

        selectBorder.SetActive(true);
        ShopItemUISelected?.Invoke(this);
        selectButton.enabled = false;
    }

    public void UnSelected()
    {
        selectBorder.SetActive(false);
        selectButton.enabled = true;
    }

}
