using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Level))]
public class LevelEditor : Editor {
    private Level level;

    private void OnEnable() {
        level = (Level)target;
    }

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        if(level != null) {
            if(level.PreviousGroundColor != level.GetGroundColor(0)) {
                level.GroundColorChanged();
                level.PreviousGroundColor = level.GetGroundColor(0);
            }
            if(level.PreviousBoundaryColor != level.GetBoundaryColor(0)) {
                level.BoundaryColorChanged();
                level.PreviousBoundaryColor = level.GetBoundaryColor(0);
            }
            if(level.PreviousBackgroundColor != level.GetBackgroundColor(0)) {
                level.BackgroundColorChanged();
                level.PreviousBackgroundColor = level.GetBackgroundColor(0);
            }
            if(level.PreviousPlayerColor != level.GetPlayerColor(0)) {
                level.PlayerColorChanged();
                level.PreviousPlayerColor = level.GetPlayerColor(0);
            }
            if(level.PreviousObstacleColor != level.GetObstacleColor(0)) {
                level.ObstacleColorChanged();
                level.PreviousObstacleColor = level.GetObstacleColor(0);
            }
            if(level.PreviousLethalObstacleColor != level.GetLethalObstacleColor(0)) {
                level.LethalObstacleColorChanged();
                level.PreviousLethalObstacleColor = level.GetLethalObstacleColor(0);
            }
        }

        EditorGUILayout.Space();

        if(GUILayout.Button("Force Update Colors")) {
            level.GroundColorChanged();
            level.BoundaryColorChanged();
            level.BackgroundColorChanged();
            level.PlayerColorChanged();
            level.ObstacleColorChanged();
            level.LethalObstacleColorChanged();
        }
        
        EditorGUILayout.Space();

        if(GUILayout.Button("Update Level Length")) {
            int groundLength = 0;

            foreach(Ground ground in GameObject.FindObjectsOfType<Ground>()) {
                if(!ground.isAdditional) {
                    groundLength += 20;
                }
            }

            level.Length = groundLength;
        }
    }
}
