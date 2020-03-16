using UnityEngine;

public class InputManager : MonoSingleton<InputManager> {
    private Vector2 previousMousePos;

    private Vector2 dragDelta;
    private Vector2 touchPos;
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
        

#if UNITY_EDITOR
        dragDelta = Vector2.zero;


        Vector2 mousePos = Input.mousePosition;
        dragDelta = mousePos - previousMousePos;

        isTouching = Input.GetMouseButton(0);

        previousMousePos = Input.mousePosition;
#else
        dragDelta = Vector2.zero;
        touchPos = Vector2.zero;
        isTouching = false;

        if(Input.touchCount > 0) {
            isTouching = true;
            var mainTouch = Input.touches[0];
            if(mainTouch.phase == TouchPhase.Moved || mainTouch.phase == TouchPhase.Ended || mainTouch.phase == TouchPhase.Canceled) {
                dragDelta = mainTouch.deltaPosition;
            }
            touchPos = mainTouch.position;
        }
#endif
    }
}
