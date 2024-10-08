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

    public ShopItem<WeaponType> GetWeapon(WeaponType type)
    {
        return WeaponList.Where(o => o.type == type).ElementAt(0);
    }
    public ShopItem<WeaponType> GetWeapon()
    {
        return WeaponList.ElementAt(Random.Range(0, WeaponList.Count - 1));
    }

    //Other item get function...
}