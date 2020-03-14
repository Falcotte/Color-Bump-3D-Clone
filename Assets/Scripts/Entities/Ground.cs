using UnityEngine;

public class Ground : MonoBehaviour {
    [SerializeField] private Renderer visualRenderer;
    public Renderer VisualRenderer => visualRenderer;

    public void SetColor() {
        visualRenderer.sharedMaterial.color = LevelSettings.Level.GetGroundColor(0);
    }
}
