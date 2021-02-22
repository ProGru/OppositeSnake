using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class OnButtonClick : MonoBehaviour, IPointerClickHandler
{
    [Tooltip("Set audio clip to play on button click")]
    public AudioClip audioClip;

    public void OnClick()
    {
        SoundManager.Instance.PlayButton(audioClip);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClick();
        buttonClick?.Invoke();
    }

    public UnityEvent buttonClick;

}
