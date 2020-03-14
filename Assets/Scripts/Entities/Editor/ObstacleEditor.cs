using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Obstacle))]
public class ObstacleEditor : Editor {
    private Obstacle obstacle;

    private void OnEnable() {
        obstacle = (Obstacle)target;
    }

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        obstacle.SetMaterial();
        obstacle.SetColor();
    }
}
