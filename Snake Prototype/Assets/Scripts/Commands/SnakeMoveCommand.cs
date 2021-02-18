using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMoveCommand : ICommand
{
    Vector3 _targetPoint;
    GameObject _snake;
    Vector3 _startPoint;

    public SnakeMoveCommand(Vector3 targetPoit,GameObject snake)
    {
        _snake = snake;
        _targetPoint = targetPoit;
        _startPoint = snake.transform.position;
    }

    public void Execute()
    {
        _snake.transform.position = _targetPoint;
    }

    public void Undo()
    {
        _snake.transform.position = _startPoint;
    }
}
