using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerController : MonoBehaviour
{
    GridMap gridMap;
    ICommand command;

    [Inject]
    public void GridMapConstruct(GridMap _gridMap)
    {
        gridMap = _gridMap;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            PlayerStep(Vector3.forward);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            PlayerStep(Vector3.back);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            PlayerStep(Vector3.left);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            PlayerStep(Vector3.right);
        }
    }

    private void PlayerStep(Vector3 step)
    {
        if (gridMap.IsWalkable(transform.position + step))
        {
            command = new MoveCommand(step, gameObject);
            command.Execute();
            EventBroker.CallPlayerMove(command);
        }
    }
}
