using UnityEngine;
using NaughtyAttributes;

[ExecuteInEditMode]
public class Boundary : MonoBehaviour {
    public enum BoundaryHeight { Low, High };
    public BoundaryHeight height;

    [SerializeField] private int boundaryLength;

    [SerializeField] private Transform boundaryLeft;
    [SerializeField] private Transform boundaryRight;

    [SerializeField] private Renderer[] visualRenderers;
    public Renderer[] VisualRenderer => visualRenderers;

    private int previousColorIndex;
    private int currentColorIndex;
    private bool changingColor;
    private float colorChangeDuration = 2f;
    private float timer;

    private void OnEnable() {
        Level.OnBoundaryColorChanged += SetColor;
    }

    private void OnDisable() {
        Level.OnBoundaryColorChanged -= SetColor;
    }

    private void Start() {
        currentColorIndex = GameManager.Instance.currentBoundaryColorIndex;
    }

    private void Update() {
        if(Application.isPlaying) {
            if(currentColorIndex != GameManager.Instance.currentBoundaryColorIndex) {
                previousColorIndex = currentColorIndex;
                currentColorIndex = GameManager.Instance.currentBoundaryColorIndex;
                changingColor = true;
            }

            if(changingColor) {
                if(timer <= colorChangeDuration) {
                    ChangeColor(previousColorIndex, timer);
                    timer += Time.deltaTime;
                }
                else {
                    ChangeColor(previousColorIndex, colorChangeDuration);
                    timer = 0f;
                    changingColor = false;
                }
            }
        }
    }

    private void ChangeColor(int previousColorIndex, float timer) {
        foreach(Renderer visualRenderer in visualRenderers) {
            visualRenderer.sharedMaterial.color = Color.Lerp(LevelSettings.Level.GetBoundaryColor(previousColorIndex),
                                                             LevelSettings.Level.GetBoundaryColor(GameManager.Instance.currentBoundaryColorIndex),
                                                             timer / colorChangeDuration);
        }
    }

    #region Editor Helper Methods
    [Button]
    public void SnapBoundaryPosition() {
        //TODO: Can be improved to snap to closer integer value
        transform.position = new Vector3(0, 0, Mathf.FloorToInt(transform.position.z));
    }

    public void SetDimensions() {
        if(height == BoundaryHeight.Low) {
            boundaryLeft.localScale = new Vector3(0.12f, 0.12f, boundaryLength);
            boundaryRight.localScale = new Vector3(0.12f, 0.12f, boundaryLength);
            boundaryLeft.gameObject.layer = LayerMask.NameToLayer("BoundaryLow");
            boundaryRight.gameObject.layer = LayerMask.NameToLayer("BoundaryLow");
        }
        else {
            boundaryLeft.localScale = new Vector3(0.12f, 2.4f, boundaryLength);
            boundaryRight.localScale = new Vector3(0.12f, 2.4f, boundaryLength);
            boundaryLeft.gameObject.layer = LayerMask.NameToLayer("BoundaryHigh");
            boundaryRight.gameObject.layer = LayerMask.NameToLayer("BoundaryHigh");
        }
        boundaryLeft.localPosition = new Vector3(-3, 0, boundaryLength / 2f);
        boundaryRight.localPosition = new Vector3(3, 0, boundaryLength / 2f);
    }

    public void SetColor() {
        foreach(Renderer visualRenderer in visualRenderers) {
            visualRenderer.sharedMaterial.color = LevelSettings.Level.GetBoundaryColor(GameManager.Instance.currentBoundaryColorIndex);
        }
    }

    private void OnDrawGizmos() {
        //TODO: Can get rid of magic numbers
        Gizmos.color = Color.magenta;

        Gizmos.DrawWireCube(boundaryLeft.position + Vector3.left * 5 * 0.12f, new Vector3(0.12f * 11, 0.12f * 5, boundaryLength));
        Gizmos.DrawWireCube(boundaryRight.position + Vector3.right * 5 * 0.12f, new Vector3(0.12f * 11, 0.12f * 5, boundaryLength));

#if UNITY_EDITOR
        if(height == BoundaryHeight.Low) {
            UnityEditor.Handles.Label(transform.position, "Boundary Low");
        }
        else {
            UnityEditor.Handles.Label(transform.position, "Boundary High");
        }
#endif
    }
#endregion
}
