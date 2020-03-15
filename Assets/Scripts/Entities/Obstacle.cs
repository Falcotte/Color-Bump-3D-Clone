using UnityEngine;
using DG.Tweening;

#pragma warning disable 0649
[ExecuteInEditMode]
public class Obstacle : MonoBehaviour {
    [SerializeField] private Renderer visualRenderer;
    public Renderer VisualRenderer => visualRenderer;

    [SerializeField] private Rigidbody rb;

    [SerializeField] private bool isLethal;
    public bool IsLethal => isLethal;

    [SerializeField] private bool isMoving;
    public bool IsMoving => isMoving;

    [SerializeField] private bool isRotating;
    public bool IsRotating => isRotating;

    [SerializeField] private Material obstacleMaterial;
    [SerializeField] private Material obstacleLethalMaterial;

    [SerializeField] private int colorIndex;

    private void OnEnable() {
        Level.OnObstacleColorChanged += SetColor;
        Level.OnLethalObstacleColorChanged += SetColor;
    }

    private void OnDisable() {
        Level.OnObstacleColorChanged -= SetColor;
        Level.OnLethalObstacleColorChanged -= SetColor;
    }

    private void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player")) {
            rb.useGravity = true;

            if(isLethal) {
                Debug.Log("Game Over");
                collision.gameObject.GetComponent<PlayerHandler>().Die();
            }
        }

        else if(collision.gameObject.layer == LayerMask.NameToLayer("Obstacle")) {
            if(isMoving || isRotating) {
                DOTween.Kill(transform);
            }
            rb.useGravity = true;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("ColorChanger")) {
            colorIndex++;
            ChangeMaterial();
            SetColor();
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
    
    public void ChangeMaterial() {
        if(isLethal) {
            if(visualRenderer.material != obstacleMaterial) {
                visualRenderer.material = obstacleMaterial;
            }
        }
        else {
            if(visualRenderer.material != obstacleLethalMaterial) {
                visualRenderer.material = obstacleLethalMaterial;
            }
        }
    }

    public void SetColor() {
        if(IsLethal) {
            visualRenderer.sharedMaterial.color = LevelSettings.Level.GetLethalObstacleColor(colorIndex);
        }
        else {
            visualRenderer.sharedMaterial.color = LevelSettings.Level.GetObstacleColor(colorIndex);
        }
    }

    private void OnDrawGizmos() {
        if(LevelManager.Instance != null) {
            if(LevelManager.Instance.ShowObstacleIcons) {
                if(isLethal) {
                    Gizmos.DrawIcon(transform.position, "sv_icon_dot14_pix16_gizmo");
                }
                else {
                    Gizmos.DrawIcon(transform.position, "sv_icon_dot10_pix16_gizmo");
                }
            }
        }
    }
}
