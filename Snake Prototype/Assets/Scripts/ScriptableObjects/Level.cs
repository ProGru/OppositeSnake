using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName ="Level",menuName ="Level")]
public class Level : ScriptableObject
{
    public string levelName;
    public List<Fruit> fruits;

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
}
