using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {
    public static InputManager Instance { get; private set; }

    private Vector2 previousMousePos;

    private Vector2 dragDelta;
    public static Vector2 DragDelta => Instance.dragDelta;

    private bool isTouching;
    public static bool IsTouching => Instance.isTouching;

    private void Awake() {
        if(Instance != null && Instance != this) {
            Destroy(gameObject);
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update() {
        HandleInput();
    }

    private void HandleInput() {
        dragDelta = Vector2.zero;

#if UNITY_EDITOR
        Vector2 mousePos = Input.mousePosition;
        dragDelta = mousePos - previousMousePos;

        isTouching = Input.GetMouseButton(0);

        previousMousePos = Input.mousePosition;
#endif
    }
}
