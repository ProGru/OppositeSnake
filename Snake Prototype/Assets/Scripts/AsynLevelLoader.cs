using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AsynLevelLoader : ILevelLoader
{

    private string currentSceneName;

    List<AsyncOperation> loadOperations = new List<AsyncOperation>();

    void OnLoadOperationComplete(AsyncOperation ao)
    {
        if (loadOperations.Contains(ao))
        {
            loadOperations.Remove(ao);
        }
        EventBroker.CallOnSceneLoadComplete();
    }

    void OnUnloadOperationComplete(AsyncOperation ao)
    {
    }

    public void LoadScene(string sceneName)
    {
        if (currentSceneName != null)
        {
            EventBroker.CallOnSceneLoadStart();
            UnloadScene(currentSceneName);
        }
        AsyncOperation ao = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        if (ao == null)
        {
            return;
        }
        ao.completed += OnLoadOperationComplete;
        currentSceneName = sceneName;
        loadOperations.Add(ao);
    }

    public void UnloadScene(string sceneName)
    {
        AsyncOperation ao = SceneManager.UnloadSceneAsync(sceneName);
        if (ao == null)
        {
            Debug.LogError("[GameManager] Unable to unload scene.");
            return;
        }
        ao.completed += OnUnloadOperationComplete;
    }
}
