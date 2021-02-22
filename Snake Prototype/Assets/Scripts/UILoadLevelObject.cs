using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UILoadLevelObject : MonoBehaviour
{
    public TextMeshProUGUI levelNameText;

    private string _levelName;
    public string LevelName
    {
        get
        {
            return _levelName;
        }
        set
        {
            _levelName = value;
            levelNameText.text = value;
        }
    }

    public void LoadLevel()
    {
        GameManager.Instance.LoadScene(LevelName);
    }
}
