using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMap
{
    private int mapSizeX;
    private int mapSizeZ;
    private float fieldSize;
    private Vector3 startPoint;
    private LayerMask obstacleMask;

    public GridMap(int _mapSizeX, int _mapSizeZ, float _fieldSize, Vector3 _startPoint, LayerMask _obstacleMask = default)
    {
        mapSizeX = _mapSizeX;
        mapSizeZ = _mapSizeZ;
        fieldSize = _fieldSize;
        startPoint = _startPoint;
        obstacleMask = _obstacleMask;
        CreateGrid();
    }

    public Node[,] mapGrid { get; set; }

    public int MapSizeX
    {
        get { return mapSizeX; }
    }

    public int MapSizeZ
    {
        get { return mapSizeZ; }
    }

    public float FieldSize
    {
        get { return fieldSize; }
    }

    /// <summary>
    /// Create a Grid Map full of Nodes where each Node have responding field in fieldSize, and grid is given by mapSize
    /// </summary>
    public void CreateGrid()
    {
        mapGrid = new Node[mapSizeZ, mapSizeX];

        Vector3 horizontalSize = Vector3.forward * fieldSize;
        Vector3 verticalSize = Vector3.right * fieldSize;
        Vector3 fixedStartPoint = startPoint + new Vector3(fieldSize / 2f, 0, fieldSize / 2f);

        for (int i = 0; i < mapSizeZ; i++)
        {
            for (int j = 0; j < mapSizeX; j++)
            {
                //Calculate cube center position on grid map
                Vector3 position = fixedStartPoint + horizontalSize * i + verticalSize * j;
                bool emptyField = !Physics.CheckSphere(position, 0.2f,obstacleMask);

                mapGrid[i, j] = new Node(position, emptyField, new Vector2(i,j));
            }
        }
    }

    /// <summary>
    /// return true if responding Node exist and is walkable
    /// </summary>
    /// <param name="x">x coordinate</param>
    /// <param name="z">y coordinate</param>
    /// <returns></returns>
    public bool IsWalkable(int x, int z) 
    {
        if (x < mapSizeX && x >= 0 && z < mapSizeZ && z >= 0)
        {
            return mapGrid[x, z].walkable;
        }
        else
        {
            return false;
        }
    }

    public Node GetNode(int x,int z)
    {
        if (x < mapSizeX && x >= 0 && z < mapSizeZ && z >= 0)
        {
            return mapGrid[x, z];
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// return true if responding Node exist and is walkable
    /// </summary>
    /// <param name="worldPosition">position in world space</param>
    /// <returns></returns>
    public bool IsWalkable(Vector3 worldPosition)
    {
        Node node = GetNode(worldPosition);
        if (node != null)
        {
            return node.walkable;
        }
        return false;
    }

    /// <summary>
    /// Return responding Node in coordinates from mapGrid
    /// </summary>
    /// <param name="worldPosition"> coordinates in wordl space</param>
    /// <returns></returns>
    public Node GetNode(Vector3 worldPosition)
    {
        if (worldPosition.x < mapSizeX && worldPosition.x >= 0 && worldPosition.z < mapSizeZ && worldPosition.z >= 0)
        {
            int x = Mathf.RoundToInt(((worldPosition.x-fieldSize/2f) / fieldSize));
            int z = Mathf.RoundToInt(((worldPosition.z-fieldSize/2f) / fieldSize));
            return mapGrid[z, x];
        }
        return null;
    }

    /// <summary>
    /// Set wordPosition responding Node in mapGrid to unwalkable
    /// </summary>
    /// <param name="worldPosition">Coordinates in world space</param>
    public void SetNodeToUnwalkable(Vector3 worldPosition)
    {
        Node node = GetNode(worldPosition);
        if (node != null)
        {
            node.walkable = false;
        }
    }

    public List<Node> GetNeightbours(Node node)
    {
        List<Node> neightbours = new List<Node>();
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                if (i == 0 && j == 0)
                {
                    continue;
                }
                else
                {

                    int x = (int)node.gridPos.x + i;
                    int z = (int)node.gridPos.y + j;
                    Node selected = GetNode(x, z);
                    if (selected != null)
                    {
                        neightbours.Add(selected);
                    }
                }

            }
        }

        return neightbours;
    }

}
