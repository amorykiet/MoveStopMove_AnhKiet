using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState {MainMenu, GamePlay, Finish}

public class GameManager : Singleton<GameManager>
{
    private static GameState gameState;

    public void ChangeState(GameState state)
    {
        gameState = state;
    }

    public bool IsState(GameState state) => gameState == state;

    private void Start()
    {
        OnInit();
    }

    public void OnInit()
    {
        UserDataManager.Ins.OnInit();
        UIManager.Ins.OpenUI<CanvasMainMenu>();
        ChangeState(GameState.MainMenu);
    }
}
