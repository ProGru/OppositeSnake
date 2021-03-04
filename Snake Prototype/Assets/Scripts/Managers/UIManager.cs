using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    public Canvas startScreen;
    public Camera uIcamera;

    public Canvas loadingCanvas;

    public Canvas menuCanvas;

    public Canvas optionCanvas;

    public Canvas levelSelectCanvas;

    private Canvas _activeCanvas;

    private void Update()
    {
        if (startScreen.gameObject.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                GameManager.Instance.LoadScene(GameManager.Instance.defaultSceneName);
                ShowLoadingCanvas(true, "Ładuję..");

            }
        }
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_activeCanvas != startScreen)
            {
                ShowMenu(!menuCanvas.gameObject.activeSelf);
            }
        }
    }

    private void Start()
    {
        optionCanvas.gameObject.GetComponentInChildren<Toggle>().isOn = SoundManager.Instance.settings.isMuted;
        optionCanvas.gameObject.GetComponentInChildren<Scrollbar>().value = SoundManager.Instance.settings.volume;
        _activeCanvas = startScreen;
    }

    public void ShowLoadingCanvas(bool show, string text = null)
    {
        _activeCanvas.gameObject.SetActive(false);
        _activeCanvas = loadingCanvas;
        loadingCanvas.gameObject.SetActive(show);
        if (text != null)
        {
            loadingCanvas.GetComponentInChildren<TextMeshProUGUI>().text = text;
        }
        else
        {
            loadingCanvas.GetComponentInChildren<TextMeshProUGUI>().text = "Loading ...";
        }

        //set method for Loading Complete event
        if (show)
        {
            EventBroker.OnSceneLoadComplete += CloseCameraAndCanvas;
        }
        else
        {
            EventBroker.OnSceneLoadComplete -= CloseCameraAndCanvas;
        }

    }

    private void CloseCameraAndCanvas()
    {
        ShowLoadingCanvas(false);
        uIcamera.gameObject.SetActive(false);
    }

    public void RestartLevel()
    {
        GameManager.Instance.Reload();
    }

    public void ShowMenu(bool show)
    {
        _activeCanvas?.gameObject.SetActive(false);
        menuCanvas.gameObject.SetActive(show);
        if (show)
            _activeCanvas = menuCanvas;
    }

    public void ShowOptions(bool show)
    {
        _activeCanvas?.gameObject.SetActive(false);
        optionCanvas.gameObject.SetActive(show);
        if(show)
            _activeCanvas = optionCanvas;
    }

    public void ShowLevelLoad(bool show)
    {
        _activeCanvas?.gameObject.SetActive(false);
        levelSelectCanvas.gameObject.SetActive(show);
        if (show)
            _activeCanvas = levelSelectCanvas;
    }

    public void Exit()
    {
        GameManager.Instance.Exit();
    }

    public void Mute(Toggle mute)
    {
        SoundManager.Instance.Mute(mute.isOn);
    }

    public void SoundVolumeChange(Scrollbar scrollbar)
    {
        SoundManager.Instance.AdioVolume(scrollbar.value);
    }



}
