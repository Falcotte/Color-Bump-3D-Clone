using UnityEngine;

public class CameraHandler : MonoSingleton<CameraHandler> {
    private float velocity => SettingsManager.GameSettings.MinVelocity;

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
}
