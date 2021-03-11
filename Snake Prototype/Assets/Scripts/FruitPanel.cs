using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FruitPanel : MonoBehaviour
{
    public TextMeshProUGUI starsDisplay;

    public void UpdateStars(int stars)
    {
        starsDisplay.text = stars.ToString();
    }
}
