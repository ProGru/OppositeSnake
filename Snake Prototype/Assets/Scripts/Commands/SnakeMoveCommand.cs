using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMoveCommand : ICommand
{
    Node _targetNode;
    SnakeController _snake;
    Node _startNode;

    public SnakeMoveCommand(Node targetNode,SnakeController snake)
    {
        _snake = snake;
        _targetNode = targetNode;
        _startNode = snake.standingNode;
    }

    public bool CanExecute()
    {
        return _targetNode.walkable;
    }

    public void Execute()
    {
        _snake.standingNode.walkable = true;
        _targetNode.walkable = false;
        _snake.standingNode = _targetNode;
        _snake.transform.position = _targetNode.wordlPosition;
    }

    public void Undo()
    {
        _snake.standingNode.walkable = true;
        _startNode.walkable = false;
        _snake.standingNode = _startNode;
        _snake.transform.position = _startNode.wordlPosition;
    }
}
