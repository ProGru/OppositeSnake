using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public List<GameObject> managerPrefabs;
    List<GameObject> _instanceOfManagers;
    private ILevelLoader levelLoader;

    public Level defaultScene;

    private Level currentLevel;

    private void Start()
    {
        levelLoader = new AsynLevelLoader();
        _instanceOfManagers = new List<GameObject>();
        LoadManagers();
    }

    public void LoadScene(Level level)
    {
        currentLevel = level;
        levelLoader.LoadScene(level.levelName);
    }

    public void UnloadScene(string sceneName)
    {
        levelLoader.UnloadScene(sceneName);
    }

    public void UnloadScene(Level level)
    {
        levelLoader.UnloadScene(level.levelName);
    }

    void LoadManagers()
    {
        foreach (GameObject managerPrefab in managerPrefabs)
        {
            GameObject instance = Instantiate(managerPrefab);
            _instanceOfManagers.Add(instance);
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        foreach (GameObject managerInstance in _instanceOfManagers)
        {
            Destroy(managerInstance);
        }
        _instanceOfManagers.Clear();
    }

    public void Reload()
    {
        SceneManager.LoadScene("BootScene");
    }

    public void ReloadLevel()
    {
        LoadScene(currentLevel);
    }

    public void Exit()
    {
        Application.Quit();
    }

}
