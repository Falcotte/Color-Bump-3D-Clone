using UnityEngine;
using UnityEngine.Events;

public enum GameStates {
    MainMenu,
    Gameplay,
    LevelEnd
}

public class GameManager : MonoSingleton<GameManager> {
    public static UnityAction OnGameStart, OnGameEnd;

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

    protected new void Awake() {
        base.Awake();

        SetFrameRate();
    }

    private void Start() {
        CurrentState = GameStates.MainMenu;
        LevelManager.Instance.LoadLevel(LevelManager.Instance.GetNextLevel());
        StartGame();
    }

    public void StartGame() {
        OnGameStart?.Invoke();
        CurrentState = GameStates.Gameplay;
    }

    private void SetFrameRate() {
        QualitySettings.vSyncCount = 0;  // VSync must be disabled
        Application.targetFrameRate = 60;
    }
}
