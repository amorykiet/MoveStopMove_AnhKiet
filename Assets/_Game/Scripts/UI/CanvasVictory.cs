using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasVictory : UICanvas
{
    [SerializeField] private Button settingButton;

    public void Setting()
    {
        settingButton.gameObject.SetActive(false);
        UIManager.Ins.OpenUI<CanvasSetting>().SetButton(settingButton).OnInit(this);
    }

    public void NextLevel()
    {
        UIManager.Ins.CloseAll();
        LevelManager.Ins.ClearLevel();
        LevelManager.Ins.LoadNextLevel();
        UIManager.Ins.OpenUI<CanvasGamePlay>();
        GameManager.Ins.ChangeState(GameState.GamePlay);
    }    

    public void Retry()
    {
        UIManager.Ins.CloseAll();
        LevelManager.Ins.ReloadLevel();
        UIManager.Ins.OpenUI<CanvasGamePlay>().OnInit();
        GameManager.Ins.ChangeState(GameState.GamePlay);
    }

    public void MainMenu()
    {
        UIManager.Ins.CloseAll();
        UIManager.Ins.OpenUI<CanvasMainMenu>();
        LevelManager.Ins.ClearLevel();
        GameManager.Ins.ChangeState(GameState.MainMenu);
    }
}
