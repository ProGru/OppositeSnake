using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float walkingSpeed;

    GridMap gridMap;
    ICommand command;
    private bool _walkInterval = true;

    [Inject]
    public void GridMapConstruct(GridMap _gridMap)
    {
        gridMap = _gridMap;
    }

    public void UPMovement(InputAction.CallbackContext value)
    {
        PlayerStep(Vector3.forward);
    }

    public void DOWNMovement(InputAction.CallbackContext value)
    {
        PlayerStep(Vector3.back);
    }

    public void LEFTMovement(InputAction.CallbackContext value)
    {
        PlayerStep(Vector3.left);
    }

    public void RIGHTMovement(InputAction.CallbackContext value)
    {
        PlayerStep(Vector3.right);
    }

    private void PlayerStep(Vector3 step)
    {
        if (gridMap.IsWalkable(transform.position + step))
        {
            if (_walkInterval)
            {
                _walkInterval = false;
                command = new MoveCommand(step, gameObject);
                command.Execute();
                Invoke("ToggleInterval", walkingSpeed);
                EventBroker.CallPlayerMove(command);
            }
        }
    }

    private void ToggleInterval()
    {
        _walkInterval = true;
    }
}
