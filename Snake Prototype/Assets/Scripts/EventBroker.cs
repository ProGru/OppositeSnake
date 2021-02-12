using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class EventBroker
{
    public static event Action<ICommand> PlayerMoveHandler;

    public static event Action GameOverHandler;

    public static void CallPlayerMove(ICommand command)
    {
        PlayerMoveHandler?.Invoke(command);
    }

    public static void CallGameOver()
    {
        Debug.Log("GameOver");
        GameOverHandler?.Invoke();
    }
}
