using UnityEditor;
using DG.Tweening;

[CustomEditor(typeof(Obstacle))]
public class ObstacleEditor : Editor {
    private Obstacle obstacle;

    private void OnEnable() {
        obstacle = (Obstacle)target;
    }

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        if(obstacle.IsMoving) {
            if(obstacle.GetComponent<DOTweenPath>() == null) {
                obstacle.gameObject.AddComponent<DOTweenPath>();
            }
        }
        else {
            if(obstacle.GetComponent<DOTweenPath>() != null) {
                DestroyImmediate(obstacle.GetComponent<DOTweenPath>());
            }
        }

        obstacle.SetMaterial();
        obstacle.SetColor();
    }
}
