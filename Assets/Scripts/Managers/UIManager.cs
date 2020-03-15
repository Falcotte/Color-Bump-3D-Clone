using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UIManager : MonoSingleton<UIManager> {
    [SerializeField] private GameObject levelEndPanel;
    [SerializeField] private Image progressBar;
    [SerializeField] private GameObject settingsButton;
    [SerializeField] private TextMeshProUGUI currentLevelText;
    [SerializeField] private TextMeshProUGUI nextLevelText;
    [SerializeField] private TextMeshProUGUI goldAmountText;
    [SerializeField] private Image handImage;

    private void Start() {
        currentLevelText.text = (int.Parse(LevelSettings.Level.name.Substring(5)) + 1).ToString();
        nextLevelText.text = (int.Parse(LevelSettings.Level.name.Substring(5)) + 2).ToString();
    }

    private void OnEnable() {
        GameManager.OnGameStart += SetHandImageOff;
        GameManager.OnGameReset += SetLevelEndPanelVisibilityOff;
    }

    private void OnDisable() {
        GameManager.OnGameStart -= SetHandImageOff;
        GameManager.OnGameReset -= SetLevelEndPanelVisibilityOff;
    }

    private void SetLevelEndPanelVisibilityOff() {
        SetLevelEndPanelVisibility(false);
    }

    public void SetLevelEndPanelVisibility(bool visible) {
        if(visible) {
            levelEndPanel.SetActive(true);
        }
        else {
            levelEndPanel.SetActive(false);
        }
    }

    public void SetProgress(float progress) {
        progressBar.fillAmount = progress;
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
            handImage.gameObject.SetActive(true);
            handImage.transform.DORestart();
        }
        else {
            handImage.transform.DOPause();
            handImage.gameObject.SetActive(false);
        }
    }
}
