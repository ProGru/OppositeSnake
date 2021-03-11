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
        if (GameManager.Instance.gameState != GameState.Play && GameManager.Instance.gameState != GameState.Pause) return;
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
        GameManager.Instance.gameState = GameState.Play;
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
        DeactivateActivCanvas();
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
        DeactivateActivCanvas();
        GameManager.Instance.ReloadLevel();
    }

    public void ShowMenu(bool show)
    {
        DeactivateActivCanvas();
        _menuCanvas.SetActive(show);
        if (show)
        {
            _activeCanvas = menuCanvas;
            GameManager.Instance.gameState = GameState.Pause;
        }
        else
        {
            GameManager.Instance.gameState = GameState.Play;
        }
    }

    public void ShowOptions(bool show)
    {
        DeactivateActivCanvas();
        _optionCanvas.SetActive(show);
        if(show)
            _activeCanvas = optionCanvas;
    }

    public void ShowLevelLoad(bool show)
    {
        DeactivateActivCanvas();
        _levelSelectCanvas.SetActive(show);
        if (show)
            _activeCanvas = levelSelectCanvas;
    }

    public void ShowWinCanvas(bool show)
    {
        DeactivateActivCanvas();
        _winCanvas.SetActive(show);
        if (show)
            _activeCanvas = WinCanvas;
    }

    public void ShowWinCanvas()
    {
        DeactivateActivCanvas();
        _winCanvas.SetActive(true);
        _activeCanvas = WinCanvas;
    }

    public void ShowGameOver()
    {
        DeactivateActivCanvas();
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

    private void DeactivateActivCanvas()
    {
        _activeCanvas?.gameObject.SetActive(false);
        SoundManager.Instance.PlayButton();
    }

}
