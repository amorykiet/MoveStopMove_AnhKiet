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

    //OnInit
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
