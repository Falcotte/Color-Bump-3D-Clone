using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Boundary))]
public class BoundaryEditor : Editor {
    private Boundary boundary;

    private void OnEnable() {
        boundary = (Boundary)target;
    }

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        if(!Application.isPlaying) {
            boundary.SetDimensions();
            boundary.SetColor();
        }
    }
}
