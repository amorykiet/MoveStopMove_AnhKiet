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
    public Player player;

    [SerializeField] private List<Level> levelList = new();
    [SerializeField] private Player playerPref;
    [SerializeField] private Bot botPref;

    private Level currentLevel;
    private List<Character> currentCharacterList = new();
    private int currentLevelIndex;
    private List<string> currentNameList = new();

    public CameraFollow cam;
    public int currentCharacterNumber = 0;

    private void OnEnable()
    {
        Character.OnCharacterDead += Character_OnCharacterDead;
    }

    private void OnDisable()
    {
        Character.OnCharacterDead -= Character_OnCharacterDead;
    }

    private void Character_OnCharacterDead(Character character)
    {
        if (character is Player)
        {
            FailLevel();
            currentCharacterNumber--;
            UIManager.Ins.GetUI<CanvasGamePlay>().UpdateAliveNumber();
        }
        else if (character is Bot)
        {
            currentCharacterNumber--;
            UIManager.Ins.GetUI<CanvasGamePlay>().UpdateAliveNumber();
            if (currentCharacterNumber == 1)
            {
                WinLevel();
            }
        }
    }


    private void WinLevel()
    {
        if (currentLevelIndex < levelList.Count - 1)
        {
            UserDataManager.Ins.SaveLevelIndex(currentLevelIndex + 1);
        }

        player = currentCharacterList.ElementAt(0) as Player;
        player.Wining();
        UserDataManager.Ins.AddMoney(player.score);
        Invoke(nameof(ChangeCanvasToWin), 2);

    }

    private void FailLevel()
    {
        Player player = currentCharacterList.ElementAt(0) as Player;
        UserDataManager.Ins.AddMoney(player.score);
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

    public void OnInit()
    {
        currentCharacterNumber = 0;
        currentLevelIndex = UserDataManager.Ins.GetLevelIndex();

        PreviewLevel(currentLevelIndex);
    }

    public void StartLevel()
    {
        UIManager.Ins.OpenUI<CanvasGamePlay>().AttachJoyStick().OnInit().UpdateAliveNumber();
        player.joyStick = joystick;
        player.charUI.gameObject.SetActive(true);
        for (int i = 1; i < currentLevel.charNumberStartWith; i++)
        {
            currentCharacterList[i].gameObject.SetActive(true);
        }

        cam.OnPlaying();
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
        UIManager.Ins.GetUI<CanvasGamePlay>().UpdateAliveNumber();
        for (int i = 0; i < currentCharacterNumber - 1; i++)
        {
            currentNameList.Add(Constants.NAME_LIST[i]);
        }

        //Setup Player
        player = Instantiate(playerPref, transform);
        player.joyStick = joystick;
        player.AttachCam(cam);
        player.AssignName("You");

        cam.FollowToTarget(player.TF);
        currentCharacterList.Add(player);
        //Setup Bot
        for (int i = 0; i < currentLevel.charNumberStartWith - 1; i++)
        {
            Bot bot = Instantiate(botPref, transform);
            bot.AttachCam(cam);
            bot.AssignName(currentNameList[i]);
            currentCharacterList.Add(bot);
        }
        //Setup for character
        for (int i = 0; i < currentLevel.charNumberStartWith; i++)
        {
            currentCharacterList[i].TF.position = currentLevel.positionSpawnList[i];
            currentCharacterList[i].OnInit();
        }

        cam.OnPlaying();
    }

    public void PreviewLevel(int levelIndex)
    {
        //Setup Level
        currentLevel = Instantiate(levelList[levelIndex], transform);
        currentLevel.OnInit();
        currentCharacterNumber = currentLevel.charNumberStartWith;
        for (int i = 0; i < currentCharacterNumber - 1; i++)
        {
            currentNameList.Add(Constants.NAME_LIST[i]);
        }

        //Setup Player
        player = Instantiate(playerPref, transform);
        player.AttachCam(cam);
        player.AssignName("You");

        cam.FollowToTarget(player.TF);
        currentCharacterList.Add(player);

        //Setup Bot
        for (int i = 0; i < currentLevel.charNumberStartWith - 1; i++)
        {
            Bot bot = Instantiate(botPref, transform);
            bot.AttachCam(cam);
            bot.AssignName(currentNameList[i]);
            currentCharacterList.Add(bot);
        }

        //Setup for character
        for (int i = 0; i < currentLevel.charNumberStartWith; i++)
        {
            currentCharacterList[i].TF.position = currentLevel.positionSpawnList[i];
            currentCharacterList[i].OnInit();
        }

        for (int i = 1; i < currentLevel.charNumberStartWith; i++)
        {
            currentCharacterList[i].gameObject.SetActive(false);
        }


        cam.OnPreviewing();
        player.charUI.gameObject.SetActive(false);

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

        currentNameList.Clear();
        currentCharacterList.Clear();
    }


}
