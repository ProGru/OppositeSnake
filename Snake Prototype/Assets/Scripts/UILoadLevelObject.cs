using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UILoadLevelObject : MonoBehaviour
{
    public TextMeshProUGUI levelNameText;

    private Level _levelSettings;
    public Level LevelSettings
    {
        get
        {
            return _levelSettings;
        }
        set
        {
            _levelSettings = value;
            levelNameText.text = value.levelName;
        }
    }

    public void LoadLevel()
    {
        LevelSettings.Load();
    }
}
