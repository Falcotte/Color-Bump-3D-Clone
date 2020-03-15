﻿using UnityEngine;

public class Ground : MonoBehaviour {
    [SerializeField] private Renderer visualRenderer;
    public Renderer VisualRenderer => visualRenderer;

    private int previousColorIndex;
    private int currentColorIndex;
    private bool changingColor;
    private float colorChangeDuration = 2f;
    private float timer;

    private void Start() {
        currentColorIndex = GameManager.Instance.currentGroundColorIndex;
    }

    private void Update() {
        if(currentColorIndex != GameManager.Instance.currentGroundColorIndex) {
            previousColorIndex = currentColorIndex;
            currentColorIndex = GameManager.Instance.currentGroundColorIndex;
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

    private void ChangeColor(int previousColorIndex, float timer) {
            visualRenderer.sharedMaterial.color = Color.Lerp(LevelSettings.Level.GetGroundColor(previousColorIndex),
                                                             LevelSettings.Level.GetGroundColor(GameManager.Instance.currentGroundColorIndex),
                                                             timer / colorChangeDuration);
    }

    public void SetColor() {
        visualRenderer.sharedMaterial.color = LevelSettings.Level.GetGroundColor(0);
    }
}