using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPathFinder 
{
    List<Node> FindPath(Vector3 startPos, Vector3 targetPos);
}
