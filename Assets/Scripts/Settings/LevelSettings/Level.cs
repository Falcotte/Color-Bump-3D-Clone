using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "Level0", menuName = "Level/New Level", order = 3)]
public class Level : ScriptableObject {
    [Header("Level Properties")]
    [SerializeField] private float length;

    [Header("Theme")]
    [SerializeField] private List<Color> groundColors;
    [SerializeField] private List<Color> boundaryColors;
    [SerializeField] private List<Color> backgroundColors;

    [SerializeField] private List<Color> playerColors;
    [SerializeField] private List<Color> obstacleColors;

    public float GetLevelLength() {
        return length;
    }

    public Color GetGroundColor(int index) {
        return groundColors[index % groundColors.Count];
    }

    public Color GetBoundaryColor(int index) {
        return boundaryColors[index % boundaryColors.Count];
    }

    public Color GetBackgroundColor(int index) {
        return backgroundColors[index % boundaryColors.Count];
    }

    public Color GetPlayerColor(int index) {
        return playerColors[index % playerColors.Count];
    }

    public Color GetObstacleColor(int index) {
        return obstacleColors[index % obstacleColors.Count];
    }
}
