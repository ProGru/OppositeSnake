using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public List<GameObject> managerPrefabs;
    public GameState gameState;
    List<GameObject> _instanceOfManagers;
    private ILevelLoader levelLoader;

    public Level defaultScene;

    private Level currentLevel;

    private void Start()
    {
        levelLoader = new AsynLevelLoader();
        _instanceOfManagers = new List<GameObject>();
        EventBroker.GameOverHandler += GameOverState;
        EventBroker.WinHandler += GameWinState;
        LoadManagers();
    }

    public void LoadScene(Level level)
    {
        currentLevel = level;
        levelLoader.LoadScene(level.levelName);
        gameState = GameState.Play;
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
        EventBroker.GameOverHandler -= GameOverState;
        EventBroker.WinHandler -= GameWinState;
    }

    public void Reload()
    {
        SceneManager.LoadScene("BootScene");
        gameState = GameState.Play;
    }

    public void ReloadLevel()
    {
        currentLevel.Load();
        gameState = GameState.Play;
    }

    public void LoadNextLevel()
    {
        if (currentLevel.nextLevel == null) return;
        currentLevel.nextLevel.Load();
        gameState = GameState.Play;
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void GameOverState()
    {
        gameState = GameState.GameOver;
    }

    public void GameWinState()
    {
        gameState = GameState.Win;
    }

    public Level GetNextLevel()
    {
        return currentLevel.nextLevel;
    }

    public Level GetCurrentLevel()
    {
        return currentLevel;
    }

}
