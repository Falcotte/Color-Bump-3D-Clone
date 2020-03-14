using UnityEngine;
using NaughtyAttributes;

public class Boundary : MonoBehaviour {
    public enum BoundaryHeight { Low, High };
    public BoundaryHeight height;

    [SerializeField] private int boundaryLength;

    [SerializeField] private Transform boundaryLeft;
    [SerializeField] private Transform boundaryRight;

    [SerializeField] private Renderer[] visualRenderers;
    public Renderer[] VisualRenderer => visualRenderers;

    [Button]
    public void SnapBoundaryPosition() {
        //TODO: Can be improved to snap to closer integer value
        transform.position = new Vector3(0, 0, Mathf.FloorToInt(transform.position.z));
    }

    public void SetBoundaryDimensions() {
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
        boundaryLeft.localPosition = new Vector3(-3, 0, transform.position.z + boundaryLength / 2f);
        boundaryRight.localPosition = new Vector3(3, 0, transform.position.z + boundaryLength / 2f);
    }

    public void SetBoundaryColor() {
        foreach(Renderer visualRenderer in visualRenderers) {
            visualRenderer.sharedMaterial.color = LevelSettings.Level.GetBoundaryColor(0);
        }
    }

    private void OnDrawGizmos() {
        //TODO: Can get rid of magic numbers
        Gizmos.color = Color.magenta;

        Gizmos.DrawWireCube(boundaryLeft.position + Vector3.left * 5 * 0.12f, new Vector3(0.12f * 11, 0.12f * 5, boundaryLength));
        Gizmos.DrawWireCube(boundaryRight.position + Vector3.right * 5 * 0.12f, new Vector3(0.12f * 11, 0.12f * 5, boundaryLength));

        if(height == BoundaryHeight.Low) {
            UnityEditor.Handles.Label(transform.position, "Boundary Low");
        }
        else {
            UnityEditor.Handles.Label(transform.position, "Boundary High");
        }
    }
}
