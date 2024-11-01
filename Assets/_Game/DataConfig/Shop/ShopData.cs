using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopData", menuName = "ScriptableObjects/ShopData", order = 1)]
public class ShopData : ScriptableObject
{
    public List<ShopItem<HatType>> HatList;
    public List<ShopItem<PantsType>> PantsList;
    public List<ShopItem<ShieldType>> ShieldList;
    public List<ShopItem<FullSetType>> FullSetTypeList;
    public List<ShopItem<WeaponType>> WeaponList;

    public ShopItem<T> GetItem<T> (List<ShopItem<T>> list, T type) where T: Enum
    {
        return list.Where(o => o.type.Equals(type)).ElementAt(0);
    }

    public ShopItem<T> GetRandomItem<T>(List<ShopItem<T>> list) where T : Enum
    {
        return list.ElementAt(UnityEngine.Random.Range(0, list.Count));
    }

    //Get Weapon
    public ShopItem<WeaponType> GetWeapon(WeaponType type) => GetItem(WeaponList, type);
    public ShopItem<WeaponType> GetRandomWeapon() => GetRandomItem(WeaponList);

    //Get Hat
    public ShopItem<HatType> GetHat(HatType type) => GetItem(HatList, type);
    public ShopItem<HatType> GetRandomHat() => GetRandomItem(HatList);

    //Get Pant
    public ShopItem<PantsType> GetPant(PantsType type) => GetItem(PantsList, type);
    public ShopItem<PantsType> GetRandomPant() => GetRandomItem(PantsList);

}
