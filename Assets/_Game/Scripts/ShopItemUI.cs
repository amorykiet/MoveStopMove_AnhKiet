using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemUI : MonoBehaviour
{
    public static event Action<ShopItemUI> ShopItemUISelected;

    public ShopItem shopItem;

    [SerializeField] private Image image;
    [SerializeField] private Button selectButton;
    [SerializeField] private Image lockImage;   




    public void OnInit(ShopItem<WeaponType> weapon)
    {
        shopItem = weapon;
        this.image.sprite = shopItem.sprite;
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


    //public void SetImage(Sprite sprite)
    //{
    //    this.image.sprite = sprite;
    //}
}
