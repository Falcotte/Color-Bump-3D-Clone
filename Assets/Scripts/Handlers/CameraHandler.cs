using UnityEngine;

[ExecuteInEditMode]
public class CameraHandler : MonoSingleton<CameraHandler> {
    private float velocity => SettingsManager.GameSettings.MinVelocity;

    [SerializeField] private Camera mainCamera;
    public Camera MainCamera => mainCamera;

    private int previousColorIndex;
    private int currentColorIndex;
    private bool changingColor;
    private float colorChangeDuration = 2f;
    private float timer;

    private Vector3 startPosition;

    protected new void Awake() {
        base.Awake();
    }

    private void OnEnable() {
        GameManager.OnGameReset += ResetCameraPosition;
        Level.OnBackgroundColorChanged += SetColor;
    }

    private void OnDisable() {
        GameManager.OnGameReset -= ResetCameraPosition;
        Level.OnBackgroundColorChanged -= SetColor;
    }

    private void Start() {
        startPosition = transform.position;
        currentColorIndex = GameManager.Instance.currentBackgroundColorIndex;
    }

    private void ResetCameraPosition() {
        transform.position = startPosition;
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
        if(LevelSettings.Level != null) {
            mainCamera.backgroundColor = LevelSettings.Level.GetBackgroundColor(GameManager.Instance.currentBackgroundColorIndex);
        }
    }
}
