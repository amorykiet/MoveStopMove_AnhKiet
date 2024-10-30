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
        LevelManager.Ins.ClearLevel();
        UIManager.Ins.OpenUI<CanvasMainMenu>();
        GameManager.Ins.ChangeState(GameState.MainMenu);
    }
}
