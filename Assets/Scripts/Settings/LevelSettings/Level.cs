using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "Level0", menuName = "Level/New Level", order = 3)]
public class Level : ScriptableObject {
    [Header("Level Properties")]
    [SerializeField] private float length;

    [Header("Theme")]
    [SerializeField] [ReorderableList] private List<Color> groundColors;
    [SerializeField] [ReorderableList] private List<Color> boundaryColors;
    [SerializeField] [ReorderableList] private List<Color> backgroundColors;

    [SerializeField] [ReorderableList] private List<Color> playerColors;
    [SerializeField] [ReorderableList] private List<Color> obstacleColors;

    public float GetLevelLength() {
        return length;
    }

    public Color GetGroundColor(int index) {
        return groundColors[index];
    }

    public Color GetBoundaryColor(int index) {
        return boundaryColors[index];
    }

    public Color GetBackgroundColor(int index) {
        return backgroundColors[index];
    }

    public Color GetPlayerColor(int index) {
        return playerColors[index];
    }

    public Color GetObstacleColor(int index) {
        return obstacleColors[index];
    }
}
