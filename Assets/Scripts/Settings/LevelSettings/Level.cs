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
    [SerializeField] [ReorderableList] private List<Color> backgroundColors;

    [SerializeField] [ReorderableList] private List<Color> playerColor;
    [SerializeField] [ReorderableList] private List<Color> obstacleColor;
}
