using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SnakeMultiStepCommand : ICommand
{
    public List<ICommand> snakeListCommands;

    public SnakeMultiStepCommand(List<ICommand> commands)
    {
        snakeListCommands = commands;
    }

    public void Execute()
    {
        foreach(ICommand command in snakeListCommands)
        {
            command.Execute();
        }
    }

    public void Undo()
    {
        foreach (ICommand command in snakeListCommands)
        {
            command.Undo();
        }
        EventBroker.CallUndoStep();
    }
}
