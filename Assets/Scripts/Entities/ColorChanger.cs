using UnityEngine;

public class ColorChanger : MonoBehaviour {
    [SerializeField] private Renderer[] poleRenderers;
    public Renderer[] PoleRenderers => poleRenderers;
    [SerializeField] private Renderer[] wireRenderers;
    public Renderer[] WireRenderers => wireRenderers;

    private int colorIndex;

    public void SetColor() {
        foreach(Renderer poleRenderer in poleRenderers) {
            poleRenderer.sharedMaterial.color = LevelSettings.Level.GetBoundaryColor(0);
        }

        for(int i = 0; i < wireRenderers.Length; i++) {
            if(i % 2 == 0) {
                wireRenderers[i].sharedMaterial.color = LevelSettings.Level.GetPlayerColor(colorIndex);
            }
            else {
                wireRenderers[i].sharedMaterial.color = LevelSettings.Level.GetObstacleColor(colorIndex);
            }
        }
    }
}
