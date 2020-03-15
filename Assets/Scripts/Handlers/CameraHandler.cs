using UnityEngine;

public class CameraHandler : MonoSingleton<CameraHandler> {
    private float velocity => SettingsManager.GameSettings.MinVelocity;

    [SerializeField] private Camera mainCamera;
    public Camera MainCamera => mainCamera;

    private int previousColorIndex;
    private int currentColorIndex;
    private bool changingColor;
    private float colorChangeDuration = 2f;
    private float timer;

    protected new void Awake() {
        base.Awake();
    }

    private void Start() {
        currentColorIndex = GameManager.Instance.currentBackgroundColorIndex;
    }

    private void Update() {
        if(currentColorIndex != GameManager.Instance.currentBackgroundColorIndex) {
            previousColorIndex = currentColorIndex;
            currentColorIndex = GameManager.Instance.currentBackgroundColorIndex;
            changingColor = true;
        }

        if(changingColor) {
            if(timer <= colorChangeDuration) {
                ChangeColor(previousColorIndex, timer);
                timer += Time.deltaTime;
            }
            else {
                ChangeColor(previousColorIndex, colorChangeDuration);
                timer = 0f;
                changingColor = false;
            }
        }
    }

    private void FixedUpdate() {
        if(GameManager.Instance.CurrentState == GameStates.Gameplay) {
            MoveForward(velocity);
        }
    }

    public void MoveForward(float amount) {
        transform.position += Vector3.forward * amount * Time.fixedDeltaTime;
    }

    private void ChangeColor(int previousColorIndex, float timer) {

        mainCamera.backgroundColor = Color.Lerp(LevelSettings.Level.GetBackgroundColor(previousColorIndex),
                                                LevelSettings.Level.GetBackgroundColor(GameManager.Instance.currentBackgroundColorIndex),
                                                timer / colorChangeDuration);
    }

    public void SetColor() {
        mainCamera.backgroundColor = LevelSettings.Level.GetBackgroundColor(GameManager.Instance.currentBackgroundColorIndex);
    }
}
