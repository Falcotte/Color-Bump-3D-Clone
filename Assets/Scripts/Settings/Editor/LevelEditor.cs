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
        }

        if(GUILayout.Button("Force Update Colors")) {
            level.GroundColorChanged();
            level.BoundaryColorChanged();
            level.BackgroundColorChanged();
            level.PlayerColorChanged();
            level.ObstacleColorChanged();
        }
    }
}
