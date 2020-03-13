using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameSettings))]
public class GameSettingsEditor : Editor {
    private GameSettings gameSettings;

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        gameSettings = (GameSettings)target;

        EditorGUILayout.Space();

        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        if(GUILayout.Button("Write to PlayerPrefs")) {
            gameSettings.WriteToPlayerPrefs();
        }
        EditorGUILayout.HelpBox("Sets the values stored in PlayerPrefs to these values. Use with care!", MessageType.Warning);

        EditorGUILayout.EndVertical();
    }
}
