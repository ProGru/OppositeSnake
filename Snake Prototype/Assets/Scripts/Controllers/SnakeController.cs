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
        List<Node> path = pathFinder.FindPath(this.transform.position, player.transform.position);
        int pathSteps = 0;
        if (path.Count > stepAtOnce-1)
        {
            pathSteps = stepAtOnce - 1;
        }
        else
        {
            pathSteps = path.Count - 1;
        }

        for (int i = 0; i <= pathSteps; i++)
        {
            ICommand move = new SnakeMoveCommand(path[i].wordlPosition, gameObject);
            commandsToExecute.Add(move);
        }
        StartCoroutine(SnakeMoveExecutor());

    }

    private void CheckForGameOver()
    {
        if (transform.position == player.transform.position)
        {
            EventBroker.CallGameOver();
        }
    }

    IEnumerator SnakeMoveExecutor()
    {
        while (commandsToExecute.Count>0)
        {
            yield return new WaitForSeconds(snakeSpeed);
            command = commandsToExecute[0];
            command.Execute();
            EventBroker.CallSnakeMove(command);
            commandsToExecute.Remove(command);
            CheckForGameOver();
        }
    }

    private void OnDestroy()
    {
        EventBroker.PlayerMoveHandler -= MakeSnakeMove;
    }
}
