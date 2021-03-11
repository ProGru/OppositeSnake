using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SnakeController : MonoBehaviour
{
    IPathFinder pathFinder;
    public GameObject player;
    public int stepAtOnce = 1;
    public float snakeSpeed;
    GridMap gridMap;
    ICommand command;
    public Node standingNode;

    private List<ICommand> commandsToExecute;

    [Inject]
    public void GridMapConstruct(GridMap _gridMap)
    {
        gridMap = _gridMap;
    }

    private void Start()
    {
        commandsToExecute = new List<ICommand>();
        pathFinder = new AStartPathFinder(gridMap);
        EventBroker.PlayerMoveHandler += MakeSnakeMove;
        EventBroker.UndoStep += ClearCommands;
        standingNode = gridMap.GetNode(this.transform.position);
    }

    public void MakeSnakeMove(ICommand command)
    {
        CheckForGameOver();

        if (commandsToExecute.Count > 0)
        {
            Debug.LogWarning("Snake is to slow, can't add more commands" +
                " because there still is some to do (Command will be no executed)", gameObject);
            return;
        }
        List<Node> path = getSnakePath(2);
        int pathSteps = 0;
        if (path.Count > stepAtOnce-1)
        {
            pathSteps = stepAtOnce - 1;
        }
        else
        {
            pathSteps = path.Count - 1;
        }

        List<ICommand> multipleSnakeMove = new List<ICommand>();

        for (int i = 0; i <= pathSteps; i++)
        {
            ICommand move = new SnakeMoveCommand(path[i], this);
            multipleSnakeMove.Add(move);
            commandsToExecute.Add(move);
        }
        ICommand multiSnakeMoveCommand = new SnakeMultiStepCommand(multipleSnakeMove);
        EventBroker.CallSnakeMove(multiSnakeMoveCommand);
        StartCoroutine(SnakeMoveExecutor());

    }

    private List<Node> getSnakePath(int dificultyMode)
    {
        List<Node> path = pathFinder.FindPath(this.transform.position, player.transform.position);
        float pathLenght = path.Count / dificultyMode;
        foreach(Fruit fruit in FruitManager.Instance.fruitsOnMap)
        {
            List<Node> crPath = pathFinder.FindPath(this.transform.position, fruit.gridPositionNode.wordlPosition);

            if (crPath.Count < pathLenght)
            {
                pathLenght = crPath.Count;
                path = crPath;
            }
        }
        return path;
    }

    private void CheckForGameOver()
    {
        if (transform.position == player.transform.position)
        {
            EventBroker.CallGameOver();
            return;
        }
        List<Node> path = pathFinder.FindPath(this.transform.position, player.transform.position);
        if (path.Count < 2)
        {
            EventBroker.CallGameOver();
            return;
        }
    }

    IEnumerator SnakeMoveExecutor()
    {
        while (commandsToExecute.Count > 0)
        {
            yield return new WaitForSeconds(snakeSpeed);
            if (commandsToExecute.Count == 0) break;
            command = commandsToExecute[0];
            if (!command.CanExecute())
            {
                commandsToExecute.Clear();
                break;
            }
            command.Execute();
            commandsToExecute.Remove(command);
            CheckForGameOver();

        }
    }

    public void ClearCommands()
    {
        commandsToExecute.Clear();
    }

    private void OnDestroy()
    {
        EventBroker.PlayerMoveHandler -= MakeSnakeMove;
        EventBroker.UndoStep -= ClearCommands;

    }
}
