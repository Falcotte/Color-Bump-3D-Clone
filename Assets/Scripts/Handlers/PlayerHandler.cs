using UnityEngine;

#pragma warning disable 0649
[RequireComponent(typeof(Rigidbody))]
public class PlayerHandler : MonoBehaviour {
    [SerializeField] private Renderer[] visualRenderers;
    public Renderer[] VisualRenderer => visualRenderers;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject[] parts;

    [SerializeField] private TrailRenderer trail;

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
        if(GameManager.Instance.CurrentState == GameStates.Gameplay) {
            HandleControls();
            SetVelocity();
            Move();
        }
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

    public void SetColor() {
        foreach(Renderer visualRenderer in visualRenderers) {
            visualRenderer.sharedMaterial.color = LevelSettings.Level.GetPlayerColor(0);
        }
    }

    public void Die() {
        visualRenderers[0].enabled = false;
        GetComponent<SphereCollider>().enabled = false;
        trail.enabled = false;

        foreach(GameObject part in parts) {
            part.SetActive(true);
            part.GetComponent<Rigidbody>().velocity = currentVelocity;
        }

        rb.velocity = Vector3.zero;
        GameManager.Instance.CurrentState = GameStates.LevelEnd;
    }
}
