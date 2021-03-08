using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class EventBroker
{

    public static Action OnSceneLoadComplete;
    public static Action OnSceneLoadStart;

    public static event Action<ICommand> PlayerMoveHandler;

    public static event Action GameOverHandler;

    public static event Action<ICommand> SnakeMoveHandler;

    public static event Action<ICommand> CommandHandler;

    public static event Action UndoStep;

    public static event Action WinHandler;

    public static void CallUndoStep()
    {
        UndoStep?.Invoke();
    }

    public static void CallCommandHandler(ICommand command)
    {
        CommandHandler?.Invoke(command);
    }

    public static void CallPlayerMove(ICommand command)
    {
        PlayerMoveHandler?.Invoke(command);
    }

    public static void CallGameOver()
    {
        Debug.Log("GameOver");
        GameOverHandler?.Invoke();
    }

    public static void CallWin()
    {
        WinHandler?.Invoke();
    }

    public static void CallSnakeMove(ICommand command)
    {
        SnakeMoveHandler?.Invoke(command);
    }

    public static void CallOnSceneLoadComplete()
    {
        OnSceneLoadComplete?.Invoke();
    }

    public static void CallOnSceneLoadStart()
    {
        OnSceneLoadStart?.Invoke();
    }

}
