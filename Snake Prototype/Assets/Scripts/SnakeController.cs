using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SnakeController : MonoBehaviour
{
    IPathFinder pathFinder;
    public GameObject player;
    public int stepAtOnce = 2;
    GridMap gridMap;

    [Inject]
    public void GridMapConstruct(GridMap _gridMap)
    {
        gridMap = _gridMap;
    }

    private void Start()
    {
        pathFinder = new AStartPathFinder(gridMap);
        EventBroker.PlayerMoveHandler += MakeSnakeMove;
    }

    public void MakeSnakeMove(ICommand command)
    {
        List<Node> path = pathFinder.FindPath(this.transform.position, player.transform.position);
        if (path.Count > stepAtOnce-1)
        {
            this.transform.position = path[stepAtOnce-1].wordlPosition;
        }
        else
        {
            this.transform.position = path[path.Count-1].wordlPosition;
            EventBroker.CallGameOver();
        }
    }

    private void OnDestroy()
    {
        EventBroker.PlayerMoveHandler -= MakeSnakeMove;
    }
}
