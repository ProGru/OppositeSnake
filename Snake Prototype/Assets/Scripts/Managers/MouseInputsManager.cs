using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class MouseInputsManager : Singleton<MouseInputsManager>
{

    private GridMap _gridMap;

    [Inject]
    public void GridMapConstruct(GridMap gridMap)
    {
        _gridMap = gridMap;
    }

    private void Start()
    {
        _main = Camera.main;
    }

    private Camera _main;
    public LayerMask mask;

    public Vector3 GetMouseWorldPosition()
    {
        Ray ray = _main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, mask))
        {
            return raycastHit.point;
        }
        else
        {
            return new Vector3(-1, -1, -1);
        }
    }

    public Node VectorPositionToGridNode(Vector3 postion)
    {
        return _gridMap.GetNode(GetMouseWorldPosition());
    }

    public Node GetNodeGridPostion()
    {
        return VectorPositionToGridNode(GetMouseWorldPosition());
    }

}
