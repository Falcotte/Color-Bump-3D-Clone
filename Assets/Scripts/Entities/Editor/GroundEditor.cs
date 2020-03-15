using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Ground))]
public class GroundEditor : Editor {
    private Ground ground;
    
    private void OnEnable() {
        ground = (Ground)target;
    }

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        if(!Application.isPlaying) {
            ground.SetColor();
        }
    }
}
