using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DrawGridMap : MonoBehaviour
{

    public Transform snake;
    public Transform player;

    GridMap gridMap;

    public List<Node> path = new List<Node>();

    IPathFinder pathFinder;

    [Inject]
    public void GridMapConstruct(GridMap _gridMap)
    {
        gridMap = _gridMap;
    }

    private void Start()
    {
        gridMap.CreateGrid();
        pathFinder = new AStartPathFinder(gridMap);
    }

    private void Update()
    {
        path = pathFinder.FindPath(snake.position,player.position);
    }

    private void OnDrawGizmos()
    {
        if (gridMap.mapGrid != null)
        {
            foreach (Node i in gridMap.mapGrid)
            {
                Gizmos.color = i.walkable ? Color.blue : Color.red;
                if (path.Contains(i))
                {
                    Gizmos.color = Color.black;
                }
                Gizmos.DrawWireCube(i.wordlPosition, new Vector3(gridMap.FieldSize / 2f, gridMap.FieldSize / 2f, gridMap.FieldSize / 2f));
            }
        }
    }
}
