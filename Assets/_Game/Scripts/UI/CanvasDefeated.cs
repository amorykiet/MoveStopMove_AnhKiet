using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasDefeated : UICanvas
{
    [SerializeField] private Button settingButton;

    public void Setting()
    {

        settingButton.gameObject.SetActive(false);
        UIManager.Ins.OpenUI<CanvasSetting>().SetButton(settingButton).OnInit(this);
        SoundManager.Ins.OnButtonClick();
    }

    public void Retry()
    {
        UIManager.Ins.CloseAll();
        UIManager.Ins.OpenUI<CanvasGamePlay>().OnInit();
        LevelManager.Ins.ReloadLevel(); 
        GameManager.Ins.ChangeState(GameState.GamePlay);
        SoundManager.Ins.OnButtonClick();
    }

    public void MainMenu()
    {
        UIManager.Ins.CloseAll();
        LevelManager.Ins.ClearLevel();
        UIManager.Ins.OpenUI<CanvasMainMenu>();
        GameManager.Ins.ChangeState(GameState.MainMenu);
        SoundManager.Ins.OnButtonClick();
    }
}
