using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasMainMenu : UICanvas
{
    [SerializeField] private Button settingButton;
    [SerializeField] private GameObject mainUI;
    [SerializeField] private TMP_Text moneyText;

    public override void Open()
    {
        base.Open();
        UpdateMoneyText();
        Time.timeScale = 1;
        LevelManager.Ins.OnInit();
    }

    public void Play()
    {
        Close(0);
        Time.timeScale = 1;
        LevelManager.Ins.StartLevel();
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
        UpdateMoneyText();
    }

    private void UpdateMoneyText()
    {
        moneyText.text = UserDataManager.Ins.GetMoneyAmount().ToString();
    }
}
