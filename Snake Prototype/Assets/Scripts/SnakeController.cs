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
    ICommand command;

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
        //check before move
        if (transform.position == player.transform.position)
        {
            EventBroker.CallGameOver();
        }

        List<Node> path = pathFinder.FindPath(this.transform.position, player.transform.position);
        if (path.Count > stepAtOnce-1)
        {

            command = new SnakeMoveCommand(path[stepAtOnce - 1].wordlPosition, gameObject);
            command.Execute();
            EventBroker.CallSnakeMove(command);
        }
        else
        {
            command = new SnakeMoveCommand(path[path.Count - 1].wordlPosition, gameObject);
            command.Execute();
            EventBroker.CallSnakeMove(command);
        }
        //check after move
        if (transform.position == player.transform.position)
        {
            EventBroker.CallGameOver();
        }
    }

    [ContextMenu("MakeSnakeMove")]
    public void MakeSnakeMove()
    {
        ICommand command;
        //check before move
        if (transform.position == player.transform.position)
        {
            EventBroker.CallGameOver();
        }

        List<Node> path = pathFinder.FindPath(this.transform.position, player.transform.position);
        if (path.Count > stepAtOnce - 1)
        {

            command = new SnakeMoveCommand(path[stepAtOnce - 1].wordlPosition, gameObject);
            command.Execute();
            EventBroker.CallSnakeMove(command);
        }
        else
        {
            command = new SnakeMoveCommand(path[path.Count - 1].wordlPosition, gameObject);
            command.Execute();
            EventBroker.CallSnakeMove(command);
        }
        //check after move
        if (transform.position == player.transform.position)
        {
            EventBroker.CallGameOver();
        }
    }

    private void OnDestroy()
    {
        EventBroker.PlayerMoveHandler -= MakeSnakeMove;
    }
}
