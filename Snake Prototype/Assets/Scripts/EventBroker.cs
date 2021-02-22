using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class EventBroker
{

    public static Action OnSceneLoadComplete;

    public static event Action<ICommand> PlayerMoveHandler;

    public static event Action GameOverHandler;

    public static event Action<ICommand> SnakeMoveHandler;

    public static void CallPlayerMove(ICommand command)
    {
        PlayerMoveHandler?.Invoke(command);
    }

    public static void CallGameOver()
    {
        Debug.Log("GameOver");
        GameOverHandler?.Invoke();
    }

    public static void CallSnakeMove(ICommand command)
    {
        SnakeMoveHandler?.Invoke(command);
    }

    public static void CallOnSceneLoadComplete()
    {
        OnSceneLoadComplete?.Invoke();
    }

}
