using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelManager : Singleton<LevelManager>
{
    public DynamicJoystick joystick;

    [SerializeField] private List<Level> levelList = new();
    [SerializeField] private Player playerPref;
    [SerializeField] private Bot botPref;
    [SerializeField] private CameraFollow cam;

    private Level currentLevel;
    private List<Character> currentCharacterList = new();
    private int currentLevelIndex;

    public int currentCharacterNumber = 0;

    private void OnEnable()
    {
        Character.OnCharacterDead += Character_OnCharacterDead;
    }

    private void OnDisable()
    {
        Character.OnCharacterDead -= Character_OnCharacterDead;
    }

    private void Character_OnCharacterDead(Character obj)
    {
        if (obj is Player)
        {
            FailLevel();
            currentCharacterNumber--;
        }
        else if (obj is Bot)
        {
            currentCharacterNumber--;
            if (currentCharacterNumber < 2)
            {
                WinLevel();
            }
        }
    }


    //UNDONE
    private void WinLevel()
    {
        if (currentLevelIndex < levelList.Count - 1)
        {
            PlayerPrefs.SetInt("currentLevelIndex", currentLevelIndex + 1);
        }
        Invoke(nameof(ChangeCanvasToWin), 2);
    }

    private void FailLevel()
    {
        Invoke(nameof(ChangeCanvasToLose), 2);
    }

    private void ChangeCanvasToWin()
    {
        UIManager.Ins.GetUI<CanvasGamePlay>().OnPlayerWin();
    }

    private void ChangeCanvasToLose()
    {
        UIManager.Ins.GetUI<CanvasGamePlay>().OnPlayerLose();
    }

    //UNDONE
    public void OnInit()
    {
        currentCharacterNumber = 0;
        if (PlayerPrefs.HasKey("currentLevelIndex"))
        {
            currentLevelIndex = PlayerPrefs.GetInt("currentLevelIndex");
        }
        else
        {
            currentLevelIndex = 0;
        }

        LoadLevel(currentLevelIndex);
    }

    public void ReloadLevel()
    {
        ClearLevel();
        LoadLevel(currentLevelIndex);
    }

    public void LoadLevel(int levelIndex)
    {
        //Setup Level
        currentLevel = Instantiate(levelList[levelIndex], transform);
        currentLevel.OnInit();
        currentCharacterNumber = currentLevel.charNumberStartWith;

        //Setup Player
        Player player = Instantiate(playerPref, transform);
        player.joyStick = joystick;
        cam.FollowToTarget(player.TF);
        currentCharacterList.Add(player);
        //Setup Bot
        for (int i = 0; i < currentLevel.charNumberStartWith - 1; i++)
        {
            Bot bot = Instantiate(botPref, transform);
            currentCharacterList.Add(bot);
        }
        //Setup for character
        for (int i = 0; i < currentLevel.charNumberStartWith; i++)
        {
            currentCharacterList[i].TF.position = currentLevel.positionSpawnList[i];
            currentCharacterList[i].OnInit();
        }
    }

    public void LoadNextLevel()
    {
        ClearLevel();
        if (currentLevelIndex == levelList.Count - 1)
        {
            LoadLevel(currentLevelIndex);
            return;
        }
        LoadLevel(++currentLevelIndex);
    }

    public void ClearLevel()
    {
        cam.FollowToTarget(cam.transform);
        HBPool.CollectAll();
        

        foreach (var character in currentCharacterList)
        {
            if (character != null){

                Destroy(character.gameObject);
            }
        }

        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
        }

        

        currentCharacterList.Clear();
    }


}
