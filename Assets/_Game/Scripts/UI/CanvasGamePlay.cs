using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasGamePlay : UICanvas
{
    [SerializeField] private DynamicJoystick joystick;
    [SerializeField] private Button settingButton;
    [SerializeField] private TMP_Text AliveNumberText;

    public CanvasGamePlay AttachJoyStick()
    {
        LevelManager.Ins.joystick = joystick;
        return this;
    }

    public void OnPlayerWin()
    {
        UIManager.Ins.CloseAll();
        UIManager.Ins.OpenUI<CanvasVictory>();
        GameManager.Ins.ChangeState(GameState.Finish);
        SoundManager.Ins.OnVictory();
    }

    public void OnPlayerLose()
    {
        UIManager.Ins.CloseAll();
        UIManager.Ins.OpenUI<CanvasDefeated>();
        GameManager.Ins.ChangeState(GameState.Finish);
        SoundManager.Ins.OnLose();
    }

    public void Setting()
    {
        settingButton.gameObject.SetActive(false);
        UIManager.Ins.OpenUI<CanvasSetting>().SetButton(settingButton).OnInit(this);
        SoundManager.Ins.OnButtonClick();
    }

    new public CanvasGamePlay OnInit()
    {
        base.OnInit();
        settingButton.gameObject.SetActive(true);
        return this;
    }

    public void UpdateAliveNumber()
    {
        AliveNumberText.text = LevelManager.Ins.currentCharacterNumber.ToString();
    }
}
