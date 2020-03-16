using UnityEngine;
using UnityEngine.Events;

public enum GameStates {
    MainMenu,
    Gameplay,
    LevelEnd
}

public class GameManager : MonoSingleton<GameManager> {
    public static UnityAction OnGameStart, OnGameEnd, OnGameReset;

    private GameStates currentState;
    public GameStates CurrentState {
        get => currentState;
        set {
            if(currentState != value) {
                currentState = value;
            }
        }
    }

    public int currentGroundColorIndex;
    public int currentBoundaryColorIndex;
    public int currentBackgroundColorIndex;

    public bool LevelPassed;

    protected new void Awake() {
        base.Awake();

        SetFrameRate();
    }

    private void Start() {
        CurrentState = GameStates.MainMenu;
        LevelManager.Instance.LoadLevel(LevelManager.Instance.GetNextLevel());
    }

    private void Update() {
        if(Input.GetMouseButtonDown(0)) {
            if(currentState == GameStates.LevelEnd) {
                ResetGame();
            }
        }
    }

    public void StartGame() {
        OnGameStart?.Invoke();
        LevelPassed = false;
        CurrentState = GameStates.Gameplay;
        UIManager.Instance.SetSettingsButtonVisibility(false);
    }

    private void ResetGame() {
        CurrentState = GameStates.MainMenu;
        LevelManager.Instance.LoadLevel(LevelManager.Instance.GetNextLevel());
        UIManager.Instance.SetSettingsButtonVisibility(true);
        OnGameReset?.Invoke();
    }

    private void SetFrameRate() {
        QualitySettings.vSyncCount = 0;  // VSync must be disabled
        Application.targetFrameRate = 60;
    }
}
