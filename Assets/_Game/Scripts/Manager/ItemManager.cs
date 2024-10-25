using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemManager : Singleton<ItemManager>
{
    [SerializeField] private List<Weapon> weaponPrefList;
    [SerializeField] private ShopData shopData;

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

    public void GetBuffWeaponByType(WeaponType type, out BuffType buffType , out float value)
    {
        ShopItem<WeaponType> weaponShopItem = shopData.GetWeapon(type);
        value = weaponShopItem.buffValue;
        buffType = weaponShopItem.buffType;
    }


    //Hat

    //public Weapon GetWeaponPrefByType(WeaponType type)
    //{
    //    return weaponPrefList.Where(o => o.type == type).ElementAt(0);
    //}

    //public Weapon GetWeaponPrefRandom()
    //{
    //    ShopItem<WeaponType> randomTypeWeapon = shopData.GetRandomWeapon();
    //    return GetWeaponPrefByType(randomTypeWeapon.type);
    //}

}
