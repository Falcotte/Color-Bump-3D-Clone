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

    [SerializeField] private Material[] obstacleMaterials;
    [SerializeField] private Material[] obstacleLethalMaterials;

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
                isMoving = false;
                isRotating = false;
                DOTween.Kill(transform);
            }
            rb.useGravity = true;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("ColorChanger")) {
            colorIndex++;
            SetMaterial();
            SetColor();
        }
    }

    public void SetMaterial() {
        if(isLethal) {
            if(visualRenderer.sharedMaterial != obstacleLethalMaterials[colorIndex % obstacleLethalMaterials.Length]) {
                visualRenderer.sharedMaterial = obstacleLethalMaterials[colorIndex % obstacleLethalMaterials.Length];
            }
        }
        else {
            if(visualRenderer.sharedMaterial != obstacleMaterials[colorIndex % obstacleMaterials.Length]) {
                visualRenderer.sharedMaterial = obstacleMaterials[colorIndex % obstacleMaterials.Length];
            }
        }
    }

    //public void ChangeMaterial() {
    //    if(isLethal) {
    //        if(visualRenderer.material != obstacleMaterial) {
    //            visualRenderer.material = obstacleMaterial;
    //        }
    //    }
    //    else {
    //        if(visualRenderer.material != obstacleLethalMaterial) {
    //            visualRenderer.material = obstacleLethalMaterial;
    //        }
    //    }
    //}

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
