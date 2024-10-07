using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemData<T> where T : Enum
{
    public float price;
    public T type;
}

enum HatType
{
    
}

enum PantsType
{

}

enum ShieldType
{

}

enum FullSetType
{

}

enum WeaponType
{
    None = 0,
    Hammer = 1,
    Axe= 2,
    Candy = 3,
    Knife = 4,
    Boomerang = 5
}