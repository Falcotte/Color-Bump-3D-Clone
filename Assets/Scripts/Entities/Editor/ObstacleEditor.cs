using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Obstacle))]
public class ObstacleEditor : Editor {
    private Renderer visualRenderer;
    private Obstacle obstacle;

    private void OnEnable() {
        obstacle = (Obstacle)target;
        visualRenderer = obstacle.VisualRenderer;
    }

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        visualRenderer.sharedMaterial.color = obstacle.VisualColor;
    }
}
