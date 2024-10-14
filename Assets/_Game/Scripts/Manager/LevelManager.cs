using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

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
        //UIManager.Ins.GetUI<CanvasGamePlay>().PlayerWin();
    }

    private void ChangeCanvasToLose()
    {
        //UIManager.Ins.GetUI<CanvasGamePlay>().PlayerLose();
    }

    //UNDONE
    public void OnInit()
    {
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
        
        Destroy(currentLevel.gameObject);
        

        foreach (var character in currentCharacterList)
        {
            if (character != null){

                Destroy(character.gameObject);
            }
        }

        currentCharacterList.Clear();
    }


}
