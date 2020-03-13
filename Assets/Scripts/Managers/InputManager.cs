using UnityEngine;

public class InputManager : MonoSingleton<InputManager> {
    private Vector2 previousMousePos;

    private Vector2 dragDelta;
    public static Vector2 DragDelta => Instance.dragDelta;

    private bool isTouching;
    public static bool IsTouching => Instance.isTouching;

    protected new void Awake() {
        base.Awake();
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
