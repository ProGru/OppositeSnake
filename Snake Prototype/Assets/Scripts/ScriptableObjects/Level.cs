using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName ="Level",menuName ="Level")]
public class Level : ScriptableObject
{
    public string levelName;
    public List<Fruit> fruits;
    public Level nextLevel;
    [SerializeField]
    private int _bestResult;
    public int fruitForBestResult;

    public void Load()
    {
        GameManager.Instance.LoadScene(this);
        EventBroker.OnSceneLoadComplete += SetFruitManagerSettingsOnLevelLoad;
    }

    public void SetFruitManagerSettingsOnLevelLoad()
    {
        EventBroker.OnSceneLoadComplete -= SetFruitManagerSettingsOnLevelLoad;
        FruitManager.Instance.Settings = this;
    }

    public int GetBestResult()
    {
        return _bestResult;
    }

    public void UpdateBestResult(int fruits)
    {
        _bestResult = fruits;
    }
}
