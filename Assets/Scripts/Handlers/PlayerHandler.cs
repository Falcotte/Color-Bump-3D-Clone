using UnityEngine;

#pragma warning disable 0649
[RequireComponent(typeof(Rigidbody))]
public class PlayerHandler : MonoBehaviour {
    [SerializeField] private Rigidbody rb;

    private float inputXSensitivity => SettingsManager.GameSettings.InputXSensitivity;
    private float inputYSensitivity => SettingsManager.GameSettings.InputYSensitivity;
    private float minVelocity => SettingsManager.GameSettings.MinVelocity;
    private float deceleration => SettingsManager.GameSettings.Deceleration;

    private Vector3 inputVector;
    private Vector3 currentVelocity;

    private void OnEnable() {
        GameManager.OnGameStart += SetInitialVelocity;
    }

    private void OnDisable() {
        GameManager.OnGameStart -= SetInitialVelocity;
    }

    private void FixedUpdate() {
        HandleControls();
        SetVelocity();
        Move();
    }

    private void HandleControls() {
        if(InputManager.IsTouching) {
            inputVector = new Vector3(InputManager.DragDelta.x * inputXSensitivity,
                                      0f,
                                      InputManager.DragDelta.y * inputYSensitivity);
        }
        else {
            inputVector = Vector3.zero;
        }
    }

    private void SetInitialVelocity() {
        currentVelocity = Vector3.forward * minVelocity;
    }

    private void SetVelocity() {
        if(InputManager.IsTouching) {
            if(inputVector.sqrMagnitude > 0f) {
                currentVelocity = (Vector3.forward * minVelocity) + inputVector;
            }
            else {
                currentVelocity = Vector3.forward * minVelocity;
            }
        }
        else {
            float newXVelocity;
            float newZVelocity;

            //Normalizing x velocity to 0
            if(currentVelocity.x > 0) {
                newXVelocity = currentVelocity.x - deceleration * Time.fixedDeltaTime;
            }
            else {
                newXVelocity = currentVelocity.x + deceleration * Time.fixedDeltaTime;
            }
            if(Mathf.Abs(currentVelocity.x) <= 0.5f) {
                newXVelocity = 0f;
            }

            //Normalizing z velocity to minVelocity
            if(currentVelocity.z > minVelocity) {
                newZVelocity = currentVelocity.z - deceleration * Time.fixedDeltaTime;
            }
            else {
                newZVelocity = currentVelocity.z + deceleration * Time.fixedDeltaTime;
            }
            if(Mathf.Abs(currentVelocity.z) <= 0.5f) {
                newZVelocity = 0f;
            }

            currentVelocity = new Vector3(newXVelocity, 0f, newZVelocity);
        }
    }

    private void Move() {
        rb.velocity = currentVelocity;
    }
}
