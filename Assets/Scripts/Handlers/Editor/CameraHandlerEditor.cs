using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CameraHandler))]
public class CameraHandlerEditor : Editor {
    private CameraHandler cameraHandler;

    private void OnEnable() {
        cameraHandler = (CameraHandler)target;
    }

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        if(!Application.isPlaying) {
            cameraHandler.SetColor();
        }
    }
}
