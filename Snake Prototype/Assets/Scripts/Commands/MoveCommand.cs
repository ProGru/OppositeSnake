using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : ICommand
{
    private Vector3 _step;
    private GameObject _player;

    public MoveCommand(Vector3 step, GameObject player)
    {
        _step = step;
        _player = player;
    }

    public void Execute()
    {
        _player.transform.Translate(_step);
    }

    public void Undo()
    {
        _player.transform.Translate(_step*-1);
    }

    public bool CanExecute()
    {
        return true;
    }
}
