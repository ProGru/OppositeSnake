using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandManager : MonoBehaviour
{
    public List<ICommand> commandList = new List<ICommand>();

    public void AddCommand(ICommand command)
    {
        commandList.Add(command);
    }

    public void Reset()
    {
        commandList = new List<ICommand>();
    }

    public void RedoStepBack()
    {
        if (commandList.Count > 0)
        {
            ICommand command = commandList[commandList.Count - 1];
            commandList.Remove(command);
            command.Undo();
        }
    }


    private void Start()
    {
        EventBroker.UndoStep += RedoStepBack;
        EventBroker.CommandHandler += AddCommand;
        EventBroker.PlayerMoveHandler += AddCommand;
        EventBroker.SnakeMoveHandler += AddCommand;
    }

    private void OnDestroy()
    {
        EventBroker.UndoStep -= RedoStepBack;
        EventBroker.CommandHandler -= AddCommand;
        EventBroker.PlayerMoveHandler -= AddCommand;
        EventBroker.SnakeMoveHandler -= AddCommand;
    }
}
