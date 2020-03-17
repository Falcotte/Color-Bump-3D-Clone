using UnityEngine;

#pragma warning disable 0649
[ExecuteInEditMode]
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
    private float maxVelocity => SettingsManager.GameSettings.MaxVelocity;
    private float deceleration => SettingsManager.GameSettings.Deceleration;

    private Vector3 inputVector;
    private Vector3 currentVelocity;

    private float levelLength => LevelSettings.Level.GetLevelLength();
    private float levelStartPos = 5f;
    private Vector3 playerStartPos;

    [SerializeField] private int colorIndex;

    private void OnEnable() {
        GameManager.OnGameStart += SetInitialVelocity;
        GameManager.OnGameReset += ResetPlayer;
        GameManager.OnGameReset += ResetColorIndex;
        GameManager.OnGameReset += SetColor;
        Level.OnPlayerColorChanged += SetColor;
    }

    private void OnDisable() {
        GameManager.OnGameStart -= SetInitialVelocity;
        GameManager.OnGameReset -= ResetPlayer;
        GameManager.OnGameReset -= ResetColorIndex;
        GameManager.OnGameReset -= SetColor;
        Level.OnPlayerColorChanged -= SetColor;
    }

    private void Start() {
        playerStartPos = transform.position;
        SetColor();
    }

    private void Update() {
        if(GameManager.Instance.CurrentState == GameStates.Gameplay) {
            float levelCompletionPercentage = Mathf.Clamp01((transform.position.z - levelStartPos) / levelLength);
            UIManager.Instance.SetProgress(levelCompletionPercentage);
        }
    }

    private void FixedUpdate() {
        if(GameManager.Instance.CurrentState == GameStates.Gameplay) {
            HandleControls();
            SetVelocity();
            Move();
        }
    }

    private void ResetColorIndex() {
        colorIndex = 0;
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
                currentVelocity = currentVelocity.normalized * Mathf.Clamp(currentVelocity.magnitude, currentVelocity.magnitude, maxVelocity);
            }
            else if(inputVector.sqrMagnitude < Mathf.Epsilon){
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
            //if(Mathf.Abs(currentVelocity.z) <= 0.5f) {
            //    newZVelocity = 0f;
            //}

            currentVelocity = new Vector3(newXVelocity, 0f, Mathf.Min(newZVelocity, maxVelocity));
        }
    }

    private void Move() {
        rb.velocity = currentVelocity;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("ColorChanger")) {
            other.gameObject.GetComponent<Collider>().enabled = false;
            colorIndex++;
            SetColor();
        }

        else if(other.gameObject.layer == LayerMask.NameToLayer("FinishLine")) {
            Win();
        }
    }

    public void SetColor() {
        foreach(Renderer visualRenderer in visualRenderers) {
            visualRenderer.sharedMaterial.color = LevelSettings.Level.GetPlayerColor(colorIndex);
        }
    }

    private void ResetPlayer() {
        transform.position = playerStartPos;
        currentVelocity = Vector3.zero;
        rb.velocity = Vector3.zero;
        AdjustTimeScale(false);

        visualRenderers[0].enabled = true;
        GetComponent<SphereCollider>().enabled = true;
        trail.enabled = true;

        foreach(GameObject part in parts) {
            part.SetActive(false);
            part.GetComponent<Rigidbody>().velocity = currentVelocity;
        }
    }

    public void Die() {
        AdjustTimeScale(true);

        visualRenderers[0].enabled = false;
        GetComponent<SphereCollider>().enabled = false;
        trail.enabled = false;

        foreach(GameObject part in parts) {
            part.SetActive(true);
            part.GetComponent<Rigidbody>().velocity = currentVelocity;
        }

        rb.velocity = Vector3.zero;
        GameManager.Instance.CurrentState = GameStates.LevelEnd;

        UIManager.Instance.SetLevelEndPanelVisibility(true);

        if(DataManager.Instance.Vibration == 1) {
            Taptic.Failure();
        }
    }

    public void Win() {
        GameManager.Instance.LevelPassed = true;
        UIManager.Instance.SetLevelEndPanelVisibility(true);
        DataManager.Instance.Level++;
        GameManager.Instance.CurrentState = GameStates.LevelEnd;
        AdjustTimeScale(true);
        if(DataManager.Instance.Vibration == 1) {
            Taptic.Success();
        }
    }

    private void AdjustTimeScale(bool slowDown) {
        //TODO: Can add slow down scale to game settings
        if(slowDown) {
            Time.timeScale = .05f;
            Time.fixedDeltaTime = .05f * 0.01666667f;
        }
        else {
            Time.timeScale = 1f;
            Time.fixedDeltaTime = 0.01666667f;
        }
    }
}
