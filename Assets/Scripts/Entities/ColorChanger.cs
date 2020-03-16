using UnityEngine;

[ExecuteInEditMode]
public class ColorChanger : MonoBehaviour {
    [SerializeField] private Renderer[] poleRenderers;
    public Renderer[] PoleRenderers => poleRenderers;
    [SerializeField] private Renderer[] wireRenderers;
    public Renderer[] WireRenderers => wireRenderers;

    [SerializeField] private int colorIndex;

    private void OnEnable() {
        Level.OnColorChangerColorChanged += SetColor;
    }

    private void OnDisable() {
        Level.OnColorChangerColorChanged -= SetColor;
    }

    public void SetColor() {
        foreach(Renderer poleRenderer in poleRenderers) {
            poleRenderer.sharedMaterial.color = LevelSettings.Level.GetBoundaryColor(0);
        }

        for(int i = 0; i < wireRenderers.Length; i++) {
            if(i % 2 == 0) {
                wireRenderers[i].sharedMaterial.color = LevelSettings.Level.GetObstacleColor(colorIndex);
            }
            else {
                wireRenderers[i].sharedMaterial.color = LevelSettings.Level.GetLethalObstacleColor(colorIndex);
            }
        }
    }
}
