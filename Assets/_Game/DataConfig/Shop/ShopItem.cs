using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ShopItem<T> where T : Enum
{
    public float price;
    public T type;
    public BuffType buffType;
    public float buffValue;

}

public enum HatType
{
    None = 0,
    Arrow = 1,
    Cowboy = 2,
    Crown = 3,
    Ear = 4,
    Hat = 5,
    HatCap = 6,
    HatYellow = 7,
    Headphone = 8,
    Horn = 9,
    Mustache = 10
}

public enum PantsType
{
    None = 0,
    Batman = 1,
    Chambi = 2,
    Comy = 3,
    Dabao = 4,
    Onion = 5,
    Pokemon = 6,
    Rainbow = 7,
    Skull = 8,
    Vantim = 9
}

public enum ShieldType
{
    None = 0,
    Round = 1,
    Oval = 2
}

public enum FullSetType
{
    None = 0,
    Devil = 1,
    Angel = 2,
    Witch = 3,
    Deadpool = 4,
    Thor = 5
}

public enum WeaponType
{
    None = 0,
    Hammer = 1,
    Axe= 2,
    Candy = 3,
    Knife = 4,
    Boomerang = 5
}

public enum BuffType
{
    None = 0,
    MoveSpeed = 1,
    AttackSpeed = 2,
    RangePercent = 3,
    RangePlus = 4,
    Gold = 5
}