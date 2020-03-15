using UnityEditor;

[CustomEditor(typeof(ColorChanger))]
public class ColorChangerEditor : Editor {
    private ColorChanger colorChanger;

    private void OnEnable() {
        colorChanger = (ColorChanger)target;
    }

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        colorChanger.SetColor();
    }
}
