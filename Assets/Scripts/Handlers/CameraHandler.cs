using UnityEngine;

public class CameraHandler : MonoSingleton<CameraHandler> {
    private float velocity => SettingsManager.GameSettings.MinVelocity;

    [SerializeField] private Camera mainCamera;
    public Camera MainCamera => mainCamera;

    protected new void Awake() {
        base.Awake();
    }

    private void FixedUpdate() {
        if(GameManager.Instance.CurrentState == GameStates.Gameplay) {
            MoveForward(velocity);
        }
    }

    public void MoveForward(float amount) {
        transform.position += Vector3.forward * amount * Time.fixedDeltaTime;
    }

    public void SetColor() {
        mainCamera.backgroundColor = LevelSettings.Level.GetBackgroundColor(0);
    }
}
