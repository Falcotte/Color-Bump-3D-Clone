﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Level0", menuName = "Level/New Level", order = 3)]
public class Level : ScriptableObject {
    [Header("Level Properties")]
    [SerializeField] private float length;
    public float PreviousLength { get; set; }

    [Header("Theme")]
    [SerializeField] private List<Color> groundColors = new List<Color>();
    public Color PreviousGroundColor { get; set; }
    [SerializeField] private List<Color> boundaryColors = new List<Color>();
    public Color PreviousBoundaryColor { get; set; }
    [SerializeField] private List<Color> backgroundColors = new List<Color>();
    public Color PreviousBackgroundColor { get; set; }

    [SerializeField] private List<Color> playerColors = new List<Color>();
    public Color PreviousPlayerColor { get; set; }
    [SerializeField] private List<Color> obstacleColors = new List<Color>();
    public Color PreviousObstacleColor { get; set; }

    public static UnityAction OnGroundColorChanged,
                              OnBoundaryColorChanged,
                              OnBackgroundColorChanged,
                              OnPlayerColorChanged,
                              OnObstacleColorChanged;

    public float GetLevelLength() {
        return length;
    }

    public Color GetGroundColor(int index) {
        if(groundColors.Count > 0) {
            return groundColors[index % groundColors.Count];
        }
        return Color.black;
    }

    public void GroundColorChanged() {
        OnGroundColorChanged?.Invoke();
    }

    public Color GetBoundaryColor(int index) {
        if(boundaryColors.Count > 0) {
            return boundaryColors[index % boundaryColors.Count];
        }
        return Color.black;
    }

    public void BoundaryColorChanged() {
        OnBoundaryColorChanged?.Invoke();
    }

    public Color GetBackgroundColor(int index) {
        if(backgroundColors.Count > 0) {
            return backgroundColors[index % boundaryColors.Count];
        }
        return Color.black;
    }

    public void BackgroundColorChanged() {
        OnBackgroundColorChanged?.Invoke();
    }

    public Color GetPlayerColor(int index) {
        if(playerColors.Count>0) {
            return playerColors[index % playerColors.Count];
        }
        return Color.black;
    }

    public void PlayerColorChanged() {
        OnPlayerColorChanged?.Invoke();
    }

    public Color GetObstacleColor(int index) {
        if(obstacleColors.Count>0) {
            return obstacleColors[index % obstacleColors.Count];
        }
        return Color.black;
    }

    public void ObstacleColorChanged() {
        OnBoundaryColorChanged?.Invoke();
    }
}
