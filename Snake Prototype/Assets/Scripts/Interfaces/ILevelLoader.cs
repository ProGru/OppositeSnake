using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILevelLoader
{
    void LoadScene(string sceneName);
    void UnloadScene(string sceneName);
}
