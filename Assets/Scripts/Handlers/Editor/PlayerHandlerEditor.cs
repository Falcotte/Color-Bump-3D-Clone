using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerHandler))]
public class PlayerHandlerEditor : Editor
{
    private PlayerHandler player;

    private void OnEnable() {
        player = (PlayerHandler)target;
    }

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        player.SetColor();
    }
}
