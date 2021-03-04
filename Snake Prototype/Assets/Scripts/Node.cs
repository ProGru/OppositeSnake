using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : IHeapItem<Node>
{
    public Vector3 wordlPosition { get; set; }
    public bool walkable { get; set; }
    public bool haveFruit { get; set; }

    public int gCost { get; set; }
    public int hCost { get; set; }

    public Vector2 gridPos { get; set; }
    public Node parent { get; set; }

    private int _heapIndex;

    public Node(Vector3 _wordlPosition, bool _walkable, Vector2 _gridPos)
    {
        wordlPosition = _wordlPosition;
        walkable = _walkable;
        haveFruit = false;
        gridPos = _gridPos;
    }

    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }

    public int HeapIndex { get { return _heapIndex; } set { _heapIndex = value; } }

    public int CompareTo(Node other)
    {
        int compare = fCost.CompareTo(other.fCost);
        if (compare == 0)
        {
            compare = hCost.CompareTo(other.hCost);
        }
        return -compare;
    }
}
