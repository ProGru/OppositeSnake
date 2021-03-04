using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelLoadDisplay : MonoBehaviour
{
    public List<Level> Levels;
    
    public GameObject levelPrefab;
    public GameObject parentPanel;
    private List<GameObject> LevelInstantions;

    private void OnEnable()
    {
        LevelDisplay();
    }

    private void LevelDisplay()
    {
        if (LevelInstantions == null)
        {
            LevelInstantions = new List<GameObject>();
        }
        foreach(Level lvl in Levels)
        {
            GameObject levelInstance = Instantiate(levelPrefab, parentPanel.transform);
            levelInstance.GetComponent<UILoadLevelObject>().LevelSettings = lvl;
            levelInstance.GetComponent<OnButtonClick>().buttonClick.AddListener(HideWindow);
            LevelInstantions.Add(levelInstance);
        }
    }

    public void HideWindow()
    {
        UIManager.Instance.ShowLevelLoad(false);
    }

    private void OnDisable()
    {
        LevelHide();
    }

    private void LevelHide()
    {
        foreach(GameObject instance in LevelInstantions)
        {
            Destroy(instance);
        }
    }
}
