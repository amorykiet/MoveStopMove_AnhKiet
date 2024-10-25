using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasMainMenu : UICanvas
{
    [SerializeField] private Button settingButton;
    [SerializeField] private GameObject mainUI;

    public void Play()
    {
        Close(0);
        Time.timeScale = 1;
        UIManager.Ins.OpenUI<CanvasGamePlay>().AttachJoyStick().OnInit();
        LevelManager.Ins.OnInit();
        GameManager.Ins.ChangeState(GameState.GamePlay);
    }

    public void Setting()
    {
        settingButton.gameObject.SetActive(false);
        UIManager.Ins.OpenUI<CanvasSetting>().SetButton(settingButton).OnInit(this);
    }

    public void OpenShop()
    {
        mainUI.SetActive(false);
        UIManager.Ins.OpenUI<CanvasShop>().OnInit(this);
    }

    public void ShowMainUI()
    {
        mainUI.SetActive(true);
    }
}
