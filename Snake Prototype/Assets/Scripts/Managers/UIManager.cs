using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class UIManager : Singleton<UIManager>
{
    public Canvas startScreen;
    public Camera uIcamera;
    private GameObject _uiCamera;
    private GameObject _optionCanvas;
    private GameObject _menuCanvas;
    private GameObject _levelSelectCanvas;
    private GameObject _winCanvas;
    private GameObject _gameOverCanvas;

    public Canvas loadingCanvas;
    public Canvas menuCanvas;
    public Canvas optionCanvas;
    public Canvas levelSelectCanvas;
    public Canvas WinCanvas;
    public Canvas GameOverCanvas;

    private Canvas _activeCanvas;

    public void EscapeAction(InputAction.CallbackContext value)
    {
        if (!value.started) return;
        if (_activeCanvas != startScreen)
        {
            ShowMenu(!_menuCanvas.activeSelf);
        }
    }

    public void PButtonAction(InputAction.CallbackContext value)
    {
        if (!value.started) return;
        if (!startScreen.gameObject.activeSelf) return;
        EventBroker.OnSceneLoadComplete += CloseCameraAndCanvas;
        GameManager.Instance.defaultScene.Load();
        ShowLoadingCanvas(true, "Ładuję..");
    }

    private void Start()
    {
        _optionCanvas = optionCanvas.gameObject;
        _optionCanvas.GetComponentInChildren<Toggle>().isOn = SoundManager.Instance.settings.isMuted;
        _optionCanvas.GetComponentInChildren<Scrollbar>().value = SoundManager.Instance.settings.volume;
        _activeCanvas = startScreen;
        _uiCamera = uIcamera.gameObject;
        _menuCanvas = menuCanvas.gameObject;
        _levelSelectCanvas = levelSelectCanvas.gameObject;
        _winCanvas = WinCanvas.gameObject;
        _gameOverCanvas = GameOverCanvas.gameObject;
        EventBroker.OnSceneLoadStart += ShowCamera;
        EventBroker.OnSceneLoadComplete += HideCamera;
        EventBroker.WinHandler += ShowWinCanvas;
        EventBroker.GameOverHandler += ShowGameOver;
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
        HideCamera();
    }

    private void ShowCamera()
    {
        _uiCamera.SetActive(true);
    }
    private void HideCamera()
    {
        _uiCamera.SetActive(false);
    }

    public void RestartLevel()
    {
        _activeCanvas?.gameObject.SetActive(false);
        GameManager.Instance.ReloadLevel();
    }

    public void ShowMenu(bool show)
    {
        _activeCanvas?.gameObject.SetActive(false);
        _menuCanvas.SetActive(show);
        if (show)
            _activeCanvas = menuCanvas;
    }

    public void ShowOptions(bool show)
    {
        _activeCanvas?.gameObject.SetActive(false);
        _optionCanvas.SetActive(show);
        if(show)
            _activeCanvas = optionCanvas;
    }

    public void ShowLevelLoad(bool show)
    {
        _activeCanvas?.gameObject.SetActive(false);
        _levelSelectCanvas.SetActive(show);
        if (show)
            _activeCanvas = levelSelectCanvas;
    }

    public void ShowWinCanvas()
    {
        _activeCanvas?.gameObject.SetActive(false);
        _winCanvas.SetActive(true);
        _activeCanvas = WinCanvas;
    }

    public void ShowGameOver()
    {
        _activeCanvas?.gameObject.SetActive(false);
        _gameOverCanvas.SetActive(true);
        _activeCanvas = GameOverCanvas;
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

    protected override void OnDestroy()
    {
        base.OnDestroy();
        EventBroker.OnSceneLoadComplete -= CloseCameraAndCanvas;
        EventBroker.OnSceneLoadStart -= ShowCamera;
        EventBroker.OnSceneLoadComplete -= HideCamera;
        EventBroker.WinHandler -= ShowWinCanvas;
        EventBroker.GameOverHandler -= ShowGameOver;
    }

}
