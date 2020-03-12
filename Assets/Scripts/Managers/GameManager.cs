using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }

    private void Awake() {
        if(Instance != null && Instance != this) {
            Destroy(gameObject);
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        SetFrameRate();
    }

    private void Start() {
        LevelManager.Instance.LoadLevel(LevelManager.Instance.GetNextLevel());
    }

    private void SetFrameRate() {
        QualitySettings.vSyncCount = 0;  // VSync must be disabled
        Application.targetFrameRate = 60;
    }
}
