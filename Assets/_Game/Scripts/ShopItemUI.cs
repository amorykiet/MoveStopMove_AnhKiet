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
    [SerializeField] private Image lockImage; 

    //OnInit for weapon
    public void OnInit(ShopItem<WeaponType> weapon)
    {
        shopItem = weapon;
        if (UserDataManager.Ins.IsWeaponPurchased(weapon.type))
        {
            isPurchased = true;
            lockImage.gameObject.SetActive(false);
            
            if (UserDataManager.Ins.IsWeaponEquipped(weapon.type))
            {
                isEquipped = true;
                Select();
            }

        }
        this.image.sprite = shopItem.sprite;
    }

    public void Purchase()
    {
        if (shopItem is ShopItem<WeaponType>)
        {
            ShopItem<WeaponType> weaponItem = shopItem as ShopItem<WeaponType>;
            UserDataManager.Ins.Purchase(weaponItem.type, weaponItem.price);
        }

        isPurchased = true;
        lockImage.gameObject.SetActive(false);
    }

    public void Equip()
    {
        isEquipped = true;
        if (shopItem is ShopItem<WeaponType>)
        {
            ShopItem<WeaponType> weaponItem = shopItem as ShopItem<WeaponType>;
            UserDataManager.Ins.Equip(weaponItem.type);
        }
    }

    public void UnEquip()
    {
        isEquipped = false;
    }

    public void Select()
    {
        ShopItemUISelected?.Invoke(this);
        selectButton.interactable = false;
    }

    public void UnSelected()
    {
        selectButton.interactable = true;
    }

}
