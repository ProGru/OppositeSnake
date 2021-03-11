using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinDisplay : MonoBehaviour
{
    public TextMeshProUGUI nextLevelName;
    public TextMeshProUGUI statResult;

    public void OnNextLevelSelect()
    {
        GameManager.Instance.LoadNextLevel();
        UIManager.Instance.ShowWinCanvas(false);
    }

    private void OnEnable()
    {
        Level nextLevel = GameManager.Instance.GetNextLevel();
        Level currentLEvel = GameManager.Instance.GetCurrentLevel();
        nextLevelName.text = nextLevel.levelName;
        int currentScore = FruitManager.Instance.GetScore();
        statResult.text = currentScore.ToString();
        if (currentLEvel.GetBestResult() < currentScore)
        {
            currentLEvel.UpdateBestResult(currentScore);
        }
    }
}
