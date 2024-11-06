using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemManager : Singleton<ItemManager>
{
    [SerializeField] private ShopData shopData;
    [SerializeField] private List<Weapon> weaponPrefList;
    [SerializeField] private List<Hat> hatPrefList;
    [SerializeField] private List<Pant> pantMatList;
    [SerializeField] private List<FullSet> fullSetList = new();

    //Weapon
    public Weapon GetWeaponPrefByType (WeaponType type)
    {
        return weaponPrefList.Where(o => o.type == type).ElementAt(0);
    }

    public Weapon GetWeaponPrefRandom()
    {
        ShopItem<WeaponType> randomTypeWeapon = shopData.GetRandomWeapon();
        return GetWeaponPrefByType(randomTypeWeapon.type);
    }


    //Hat
    public Hat GetHatPrefByType(HatType type)
    {
        
        return hatPrefList.Where(o => o.type == type).First();
    }

    public Hat GetHatPrefRandom()
    {
        ShopItem<HatType> randomTypeHat = shopData.GetRandomHat();
        return GetHatPrefByType(randomTypeHat.type);
    }
    
    //Pant
    public Pant GetPantMatByType(PantsType type)
    {
        return pantMatList.Where(o => o.type == type).First();
    }

    public Pant GetPantMatRandom()
    {
        ShopItem<PantsType> randomTypePant = shopData.GetRandomPant();
        return GetPantMatByType(randomTypePant.type);
    }

    //FullSet

    public FullSet GetFullSetByType(FullSetType type)
    {
        return fullSetList.Where(o => o.type == type).First();
    }

}
