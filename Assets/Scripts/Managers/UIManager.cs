using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UIManager : MonoSingleton<UIManager> {
    [SerializeField] private Canvas UICanvas;
    [SerializeField] private GameObject levelWinPanel;
    [SerializeField] private GameObject levelFailPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private TextMeshProUGUI stageText;
    [SerializeField] private Image progressBar;
    [SerializeField] private GameObject startGameButton;
    [SerializeField] private GameObject settingsButton;
    [SerializeField] private TextMeshProUGUI currentLevelText;
    [SerializeField] private TextMeshProUGUI nextLevelText;
    [SerializeField] private TextMeshProUGUI goldAmountText;
    [SerializeField] private GameObject handImage;
    [SerializeField] private GameObject vibrationImage;

    private void Start() {
        currentLevelText.text = (DataManager.Instance.Level + 1).ToString();
        nextLevelText.text = (DataManager.Instance.Level + 2).ToString();
    }

    private void OnEnable() {
        GameManager.OnGameStart += SetHandImageOff;
        GameManager.OnGameStart += SetStartGameButtonOff;
        GameManager.OnGameStart += SetProgressBarColor;
        GameManager.OnGameReset += SetStartGameButtonOn;
        GameManager.OnGameReset += SetLevelEndPanelVisibilityOff;
        GameManager.OnGameReset += SetHandImageOn;
    }

    private void OnDisable() {
        GameManager.OnGameStart -= SetHandImageOff;
        GameManager.OnGameStart -= SetStartGameButtonOff;
        GameManager.OnGameStart -= SetProgressBarColor;
        GameManager.OnGameReset -= SetStartGameButtonOn;
        GameManager.OnGameReset -= SetLevelEndPanelVisibilityOff;
        GameManager.OnGameReset -= SetHandImageOn;
    }

    private void SetStartGameButtonOff() {
        startGameButton.SetActive(false);
    }

    private void SetStartGameButtonOn() {
        startGameButton.SetActive(true);
    }

    private void SetLevelEndPanelVisibilityOff() {
        SetLevelEndPanelVisibility(false);
    }

    public void SetLevelEndPanelVisibility(bool visible) {
        if(visible) {
            if(GameManager.Instance.LevelPassed) {
                levelWinPanel.SetActive(true);
            }
            else {
                levelFailPanel.SetActive(true);
                stageText.text = "STAGE " + (DataManager.Instance.Level + 1).ToString();
            }
        }
        else {
            if(GameManager.Instance.LevelPassed) {
                levelWinPanel.SetActive(false);
            }
            else {
                levelFailPanel.SetActive(false);
            }
        }
    }

    public void SetVibrationToggleStatus() {
        if(DataManager.Instance.Vibration == 0) {
            DataManager.Instance.Vibration = 1;
            vibrationImage.SetActive(true);
            Taptic.Vibrate();
        }
        else {
            DataManager.Instance.Vibration = 0;
            vibrationImage.SetActive(false);
        }
    }

    public void OpenSettingsPanel() {
        settingsPanel.SetActive(true);
        if(DataManager.Instance.Vibration == 0) {
            vibrationImage.SetActive(false);
        }
        else {
            vibrationImage.SetActive(true);
        }
    }

    public void CloseSettingsPanel() {
        settingsPanel.SetActive(false);
    }

    public void SetProgress(float progress) {
        progressBar.fillAmount = progress;
    }

    public void SetProgressBarColor() {
        progressBar.color = LevelSettings.Level.GetPlayerColor(0);
    }

    public void SetSettingsButtonVisibility(bool visible) {
        if(visible) {
            settingsButton.SetActive(true);
        }
        else {
            settingsButton.SetActive(false);
        }
    }

    public void SetGoldAmount(int goldAmount) {
        goldAmountText.text = goldAmount.ToString();
    }

    private void SetHandImageOn() {
        SetHandImageVisibility(true);
    }

    private void SetHandImageOff() {
        SetHandImageVisibility(false);
    }

    public void SetHandImageVisibility(bool visible) {
        if(visible) {
            handImage.SetActive(true);
            handImage.transform.DORestart();
        }
        else {
            handImage.transform.DOPause();
            handImage.SetActive(false);
        }
    }
}
