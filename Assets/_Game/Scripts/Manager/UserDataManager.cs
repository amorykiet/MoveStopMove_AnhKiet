using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserDataManager : Singleton<UserDataManager>
{
    public static string keyUserData = "UserData";

    private UserData userData;

    private void OnDestroy()
    {
        SaveData();
    }

    public void OnInit()
    {
        if (!PlayerPrefs.HasKey(keyUserData))
        {
            userData = new UserData();
        }
        else
        {
            userData = JsonUtility.FromJson<UserData>(PlayerPrefs.GetString(keyUserData));
        }
        
    }

    public void SaveData()
    {
        string saveData = JsonUtility.ToJson(userData);
        PlayerPrefs.SetString(keyUserData, saveData);
    }

    public int GetLevelIndex()
    {
        return userData.level;
    }

    public int GetMoneyAmount()
    {
        return userData.money;
    }

    public bool IsItemPurchased(ShopItem item)
    {
        if (item is ShopItem<WeaponType> weapon)
        {
            return IsWeaponPurchased(weapon.type);
        }
        else if (item is ShopItem<HatType> hat)
        {
            return IsHatPurchased(hat.type);
        }
        else if (item is ShopItem<PantsType> pant)
        {
            return IsPantPurchased(pant.type);
        }
        else if (item is ShopItem<FullSetType> fullSet)
        {
            return IsFullSetPurchased(fullSet.type);
        }


        return false;
    }

    public bool IsItemEquipped(ShopItem item)
    {
        if (item is ShopItem<WeaponType> weapon)
        {
            return IsWeaponEquipped(weapon.type);
        }
        else if (item is ShopItem<HatType> hat)
        {
            return IsHatEquipped(hat.type);
        }
        else if (item is ShopItem<PantsType> pant)
        {
            return IsPantEquipped(pant.type);
        }
        else if (item is ShopItem<FullSetType> fullSet)
        {
            return IsFullSetEquipped(fullSet.type);
        }

        return false;

    }

    //Save Level
    public void SaveLevelIndex(int index)
    {
        userData.level = index;
        SaveData();
    }

    //Purchase and equip
    public void Equip(ShopItem item)
    {
        if (item is ShopItem<WeaponType> weapon)
        {
            userData.isFullSetEquipped = false;
            userData.weaponEquipped = weapon.type;
        }
        else if (item is ShopItem<HatType> hat)
        {
            userData.isFullSetEquipped = false;
            userData.hatEquipped = hat.type;
        }
        else if (item is ShopItem<PantsType> pant)
        {
            userData.isFullSetEquipped = false;
            userData.pantEquipped = pant.type;
        }
        else if (item is ShopItem<FullSetType> fullSet)
        {
            userData.isFullSetEquipped = true;
            userData.fullSetEquipped = fullSet.type;
        }

        SaveData();
    }

    public void Purchase(ShopItem item)
    {
        userData.money -= item.price;
        if (item is ShopItem<WeaponType> weapon)
        {
            userData.weaponPurchasedList.Add(weapon.type);
        }
        else if (item is ShopItem<HatType> hat)
        {
            userData.hatPurchasedList.Add(hat.type);
        }
        else if (item is ShopItem<PantsType> pant)
        {
            userData.pantPurchasedList.Add(pant.type);
        }
        else if (item is ShopItem<FullSetType> fullSet)
        {
            userData.fullSetPurchasedList.Add(fullSet.type);
        }

        SaveData();
    }

    //Add money
    public bool IsPurchaseable(int cost)
    { 
        if (cost > userData.money)
        {
            return false;
        }
        return true;
    }

    public void AddMoney(int amount)
    {
        userData.money += amount;
    }


    //Weapon
    public Weapon GetCurrentWeapon()
    {
        return ItemManager.Ins.GetWeaponPrefByType(userData.weaponEquipped);
    }

    private bool IsWeaponPurchased(WeaponType type)
    {
        return userData.weaponPurchasedList.Contains(type);
    }

    private bool IsWeaponEquipped(WeaponType type)
    {
        return userData.weaponEquipped == type;
    }

    //Hat
    public Hat GetCurrentHat()
    {
        return ItemManager.Ins.GetHatPrefByType(userData.hatEquipped);
    }

    private bool IsHatPurchased(HatType type)
    {
        return userData.hatPurchasedList.Contains(type);
    }

    private bool IsHatEquipped(HatType type)
    {
        return userData.hatEquipped == type;
    }

    //Pant
    public Pant GetCurrentPant()
    {
        return ItemManager.Ins.GetPantMatByType(userData.pantEquipped);
    }

    private bool IsPantPurchased(PantsType type)
    {
        return userData.pantPurchasedList.Contains(type);
    }

    private bool IsPantEquipped(PantsType type)
    {
        return userData.pantEquipped == type;
    }

    //FullSet
    public bool IsFullSetEquipped()
    {
        return userData.isFullSetEquipped;
    }

    public FullSet GetCurrentFullSet()
    {
        return ItemManager.Ins.GetFullSetByType(userData.fullSetEquipped);
    }

    private bool IsFullSetPurchased(FullSetType type)
    {
        return userData.fullSetPurchasedList.Contains(type);
    }

    private bool IsFullSetEquipped(FullSetType type)
    {
        return userData.fullSetEquipped == type;
    }

}



[Serializable]

public class UserData
{
    public int level;
    public int money;
    public bool isFullSetEquipped;
    public List<WeaponType> weaponPurchasedList;
    public WeaponType weaponEquipped;
    public List<HatType> hatPurchasedList;
    public HatType hatEquipped;
    public List<PantsType> pantPurchasedList;
    public PantsType pantEquipped;
    public List<FullSetType> fullSetPurchasedList;
    public FullSetType fullSetEquipped;

    //Other item...


    public UserData()
    {
        //Set up level
        level = 0;
        money = 100;
        isFullSetEquipped = false;

        //Setup weapon
        weaponPurchasedList = new List<WeaponType>();
        weaponEquipped = WeaponType.Hammer;
        weaponPurchasedList.Add(weaponEquipped);

        //Setup hat
        hatPurchasedList = new List<HatType>();
        hatEquipped = HatType.None;
        hatPurchasedList.Add(hatEquipped);

        //Setup pant
        pantPurchasedList = new List<PantsType>();
        pantEquipped = PantsType.None;
        pantPurchasedList.Add(pantEquipped);

        //Setup fullSet
        fullSetPurchasedList = new List<FullSetType>();
        fullSetEquipped = FullSetType.None;
        fullSetPurchasedList.Add(fullSetEquipped);


    }
}
