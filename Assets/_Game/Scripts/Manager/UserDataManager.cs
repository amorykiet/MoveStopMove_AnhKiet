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
            Debug.Log("New user data");
            userData = new UserData();
        }
        else
        {
            userData = JsonUtility.FromJson<UserData>(PlayerPrefs.GetString(keyUserData));
        }
        
    }
    
    public int GetLevelIndex()
    {
        return userData.level;
    }

    public int GetMoneyAmount()
    {
        return userData.money;
    }

    //Save Level
    public void SaveLevelIndex(int index)
    {
        userData.level = index;
        SaveData();
    }

    //Purchase and equip weapon
    public void EquipWeapon(WeaponType weapon)
    {
        userData.weaponEquipped = weapon;
        SaveData();
    }

    //UNDONE
    public void PurchaseWeapon(WeaponType weapon, int cost)
    {
        userData.money -= cost;
        userData.weaponPurchasedList.Add(weapon);
        SaveData();
    }

    //Add money
    public void AddMoney(int amount)
    {
        userData.money += amount;
        Debug.Log("+ " + amount + " money");
    }

    public void SaveData() 
    {
        string saveData = JsonUtility.ToJson(userData);
        PlayerPrefs.SetString(keyUserData, saveData);
    }

    public Weapon GetCurrentWeapon()
    {
        return ItemManager.Ins.GetWeaponPrefByType(userData.weaponEquipped);
    }

}



[Serializable]

public class UserData
{
    public int level;
    public int money;
    public List<WeaponType> weaponPurchasedList;
    public WeaponType weaponEquipped;

    //Other item...


    public UserData()
    {
        //Set up level
        level = 0;
        money = 0;

        //Setup weapon
        weaponPurchasedList = new List<WeaponType>();
        weaponEquipped = WeaponType.Hammer;
        weaponPurchasedList.Add(weaponEquipped);
        
        
    }
}
