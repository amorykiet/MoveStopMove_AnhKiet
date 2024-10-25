using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserDataManager : Singleton<UserDataManager>
{
    public static string keyUserData = "UserData";

    [SerializeField] private UserData userData;

    private void OnDestroy()
    {
        SaveData();
    }

    public void OnInit()
    {
        if (userData == null)
        {
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

    public void PurchaseWeapon(WeaponType weapon)
    {
        userData.weaponPurchasedList.Add(weapon);
        SaveData();
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
    public List<WeaponType> weaponPurchasedList;
    public WeaponType weaponEquipped;

    //Other item...


    public UserData()
    {
        //Set up level
        level = 0;

        //Setup weapon
        weaponPurchasedList = new List<WeaponType>();
        weaponEquipped = WeaponType.Hammer;
        weaponPurchasedList.Add(weaponEquipped);
        
        
    }
}
