using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AStartPathFinder : IPathFinder
{
    private GridMap gridMap;

    public Transform snake, player;

    public AStartPathFinder(GridMap _gridMap)
    {
        gridMap = _gridMap;
    }

    public List<Node> FindPath(Vector3 startPos, Vector3 targetPos)
    {
        Node startNode = gridMap.GetNode(startPos);
        Node endNode = gridMap.GetNode(targetPos);

        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Node currentNode = FindTheLowestCostNode(openSet);

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode == endNode)
            {
                return RetracePath(startNode, endNode);
            }

            foreach(Node neighbour in gridMap.GetNeightbours(currentNode))
            {
                if (!neighbour.walkable || closedSet.Contains(neighbour))
                {
                    continue;
                }

                int newMovementCostNeightbour = currentNode.gCost + GetDistance(currentNode, neighbour);
                if (newMovementCostNeightbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newMovementCostNeightbour;
                    neighbour.hCost = GetRealDistance(neighbour, endNode);
                    neighbour.parent = currentNode;

                    if (!openSet.Contains(neighbour))
                    {
                        openSet.Add(neighbour);
                    }
                }
            }

        }
        return new List<Node>();
    }

    private Node FindTheLowestCostNode(List<Node> openSet)
    {
        Node currentNode = openSet[0];
        for (int i = 1; i < openSet.Count; i++)
        {
            if (openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost)
            {
                currentNode = openSet[i];
            }
        }
        int countEquals = 0;
        for (int i = 0; i < openSet.Count; i++)
        {
            if( openSet[i].fCost == currentNode.fCost && openSet[i].hCost == currentNode.hCost)
            {
                countEquals++;
            }
        }
        Debug.Log(countEquals);
        return currentNode;
    }

    private int GetDistance(Node nodeA, Node nodeB)
    {
        float dstX = Mathf.Abs(nodeA.gridPos.x - nodeB.gridPos.x);
        float dstY = Mathf.Abs(nodeA.gridPos.y - nodeB.gridPos.y);
        if (dstX > dstY)
        {
            return Mathf.RoundToInt( 60 * dstY + 10 * (dstX - dstY));
        }
        return Mathf.RoundToInt(60 * dstX + 10 * (dstY - dstX));
    }

    private int GetRealDistance(Node nodeA, Node nodeB)
    {
        return Mathf.RoundToInt(Vector3.Distance(nodeA.wordlPosition, nodeB.wordlPosition));
    }

    private List<Node> RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while(currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }

        path.Reverse();
        return path;
    }
}
