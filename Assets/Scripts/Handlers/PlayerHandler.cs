using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerHandler : MonoBehaviour {
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float sensitivity;

    void Update() {
        HandleControls();
    }

    private void HandleControls() {
        if(InputManager.IsTouching) {
            Debug.Log(InputManager.DragDelta);
            rb.velocity = new Vector3(InputManager.DragDelta.x, 0f, InputManager.DragDelta.y) * sensitivity / 100f;
        }
    }
}
