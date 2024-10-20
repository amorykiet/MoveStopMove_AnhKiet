using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserDataManager : Singleton<UserDataManager>
{
    public static string keyUserData = "UserData";

    [SerializeField] private UserData userData;

    //TEST
    void Start()
    {
        //OnInit();
    }

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


    //UNDONE
    public void SaveLevelIndex(int index)
    {
        userData.level = index;
        SaveData();
    }

    public void SaveData() 
    {
        string saveData = JsonUtility.ToJson(userData);
        PlayerPrefs.SetString(keyUserData, saveData);
    }

    public Weapon GetCurrentWeapon()
    {
        return ItemManager.Ins.GetWeaponPrefByType(userData.weaponAcquired);
    }

}



[Serializable]

public class UserData
{
    public int level;
    public List<WeaponType> weaponList;
    public WeaponType weaponAcquired;
    //Other item...


    public UserData()
    {
        //Set up level
        level = 0;

        //Setup weapon
        weaponList = new List<WeaponType>();
        weaponAcquired = WeaponType.Hammer;
        weaponList.Add(weaponAcquired);
    }
}
