using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasSetting : UICanvas
{
    private Button settingButton;

    [SerializeField] private Button closeButton;
    [SerializeField] private Button closeButtonInGamePlay;
    [SerializeField] private Button mainMenuButton;

    [SerializeField] private SwitchButton soundButton;
    [SerializeField] private SwitchButton vibButton;

    private bool isSoundOn = true;
    private bool isVibOn = true; 

    public void Close()
    {
        Time.timeScale = 1;
        settingButton.gameObject.SetActive(true);
        SoundManager.Ins.OnButtonClick();
        Close(0);
    }

    public CanvasSetting SetButton(Button settingButton)
    {
        this.settingButton = settingButton;
        return this;
    }

    public void MainMenu()
    {
        UIManager.Ins.CloseAll();
        LevelManager.Ins.ClearLevel();
        UIManager.Ins.OpenUI<CanvasMainMenu>();
        GameManager.Ins.ChangeState(GameState.MainMenu);
        SoundManager.Ins.OnButtonClick();
    }

    public CanvasSetting OnInit(UICanvas UIParent)
    {
        Pause();
        if (UIParent is CanvasGamePlay)
        {
            mainMenuButton.gameObject.SetActive(true);
            closeButtonInGamePlay.gameObject.SetActive(true);
            closeButton.gameObject.SetActive(false);
        }
        else
        {
            mainMenuButton.gameObject.SetActive(false);
            closeButtonInGamePlay.gameObject.SetActive(false);
            closeButton.gameObject.SetActive(true);

        }
        return this;
    }
    public void Pause()
    {
        Time.timeScale = 0;

    }

    public void SwitchSound()
    {
        isSoundOn = !isSoundOn;
        SoundManager.Ins.OnButtonClick();

        if (isSoundOn)
        {
            soundButton.SetOn();
            SoundManager.Ins.UnMute();
        }
        else
        {
            soundButton.SetOff();
            SoundManager.Ins.Mute();
        }

    }
    public void SwitchVib()
    {
        isVibOn = !isVibOn;
        if (isVibOn)
        {
            vibButton.SetOn();
        }
        else
        {
            vibButton.SetOff();
        }

        SoundManager.Ins.OnButtonClick();
    }
}
