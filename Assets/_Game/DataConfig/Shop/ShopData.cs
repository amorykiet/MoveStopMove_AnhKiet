using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopData", menuName = "ScriptableObjects/ShopData", order = 1)]
public class ShopData : ScriptableObject
{
    public List<ShopItem<HatType>> HatList;
    public List<ShopItem<PantsType>> PantsList;
    public List<ShopItem<ShieldType>> ShieldList;
    public List<ShopItem<FullSetType>> FullSetTypeList;
    public List<ShopItem<WeaponType>> WeaponList;


}
