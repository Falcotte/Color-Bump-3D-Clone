using UnityEngine;

public class BoundaryBottom : MonoBehaviour {
    [SerializeField] private Rigidbody rb;
    private float velocity => SettingsManager.GameSettings.MinVelocity;

    private void FixedUpdate() {
        if(GameManager.Instance.CurrentState == GameStates.Gameplay) {
            MoveForward(velocity);
        }
    }

    public void MoveForward(float amount) {
        rb.MovePosition(rb.position + Vector3.forward * amount * Time.fixedDeltaTime);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos() {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(rb.position, transform.GetComponent<BoxCollider>().size);
        UnityEditor.Handles.Label(transform.position + Vector3.right * GetComponent<BoxCollider>().size.x / 1.8f, "Boundary Bottom");
    }
#endif
}
