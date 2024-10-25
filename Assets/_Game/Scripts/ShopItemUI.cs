using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemUI : MonoBehaviour
{
    public static event Action<ShopItem> ShopItemSelected;

    [SerializeField] private Image image;



    private ShopItem shopItem;

    public void OnInit(ShopItem<WeaponType> weapon)
    {
        shopItem = weapon;
        this.image.sprite = shopItem.sprite;
    }

    public void Select()
    {
        ShopItemSelected?.Invoke(shopItem);
    }


    //public void SetImage(Sprite sprite)
    //{
    //    this.image.sprite = sprite;
    //}
}
