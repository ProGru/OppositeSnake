using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{


    public int mapSizeX;
    public int mapSizeZ;
    public float fieldSize;
    public Vector3 startPoint;
    public LayerMask obstacleMask;


    public override void InstallBindings()
    {
        Container.Bind<GridMap>().FromInstance(new GridMap(mapSizeX, mapSizeZ, fieldSize, startPoint, obstacleMask)).AsSingle();
    }

}
