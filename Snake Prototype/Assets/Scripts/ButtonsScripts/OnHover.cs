using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class OnHover : OnButtonClick, IPointerEnterHandler, IPointerExitHandler
{
    public Animation animationClip;

    public void OnHoverEnter()
    {
        animationClip?.Play();
        SoundManager.Instance.PlayButton(audioClip);
    }

    public void OnHoverExit()
    {
        animationClip?.Rewind();
        animationClip?.Play();
        animationClip?.Sample();
        animationClip?.Stop();

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnHoverEnter();
        hoverOn?.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnHoverExit();
        hoverOff?.Invoke();
    }

    public UnityEvent hoverOn;

    public UnityEvent hoverOff;
}
