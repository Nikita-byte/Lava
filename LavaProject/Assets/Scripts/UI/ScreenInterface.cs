using System;
using UnityEngine.UI;
using UnityEngine;


public class ScreenInterface
{
    private static ScreenInterface _instance;

    private ScreenFactory _screenFactory;

    public static ScreenInterface GetInstance()
    {
        return _instance ?? (_instance = new ScreenInterface());
    }

    public ScreenInterface()
    {
        _screenFactory = new ScreenFactory();
    }

    public void Execute(GameMode gameMode)
    {
        _screenFactory.Canvas.SetMode(gameMode);
    }

    public Button GetFireModeButton()
    {
        return _screenFactory.Canvas.GetAimButton();
    }

    public Button GetMoveModeButton()
    {
        return _screenFactory.Canvas.GetMoveButton();
    }

    public Button GetFireButton()
    {
        return _screenFactory.Canvas.GetFireButton();
    }

    public Button GetRestartButton()
    {
        return _screenFactory.Canvas.GetRestartButton();
    }
}
