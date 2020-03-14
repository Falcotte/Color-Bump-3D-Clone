using UnityEngine;

#pragma warning disable 0649
public class Obstacle : MonoBehaviour {
    [SerializeField] private Renderer visualRenderer;
    public Renderer VisualRenderer => visualRenderer;

    [SerializeField] private bool isLethal;
    public bool IsLethal => isLethal;

    [SerializeField] private Material obstacleMaterial;
    [SerializeField] private Material obstacleLethalMaterial;

    private void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player")) {
            if(isLethal) {
                Debug.Log("Game over");
                GameManager.Instance.CurrentState = GameStates.LevelEnd;
            }
        }
    }

    public void SetMaterial() {
        if(isLethal) {
            if(visualRenderer.sharedMaterial != obstacleLethalMaterial) {
                visualRenderer.sharedMaterial = obstacleLethalMaterial;
            }
        }
        else {
            if(visualRenderer.sharedMaterial != obstacleMaterial) {
                visualRenderer.sharedMaterial = obstacleMaterial;
            }
        }
    }
    public void SetColor() {
        if(IsLethal) {
            visualRenderer.sharedMaterial.color = LevelSettings.Level.GetObstacleColor(0);
        }
        else {
            visualRenderer.sharedMaterial.color = LevelSettings.Level.GetPlayerColor(0);
        }
    }
}
