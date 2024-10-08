using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemManager : Singleton<ItemManager>
{
    [SerializeField] private List<Weapon> weaponPrefList;
    [SerializeField] private ShopData shopData;

    public Weapon GetWeaponPrefByType (WeaponType type)
    {
        return weaponPrefList.Where(o => o.type == type).ElementAt(0);
    }

    public Weapon GetWeaponPrefRandom()
    {
        ShopItem<WeaponType> randomTypeWeapon = shopData.GetWeapon();
        return GetWeaponPrefByType(randomTypeWeapon.type);
    }
}
