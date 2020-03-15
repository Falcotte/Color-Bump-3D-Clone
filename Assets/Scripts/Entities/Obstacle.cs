﻿using UnityEngine;
using DG.Tweening;

#pragma warning disable 0649
public class Obstacle : MonoBehaviour {
    [SerializeField] private Renderer visualRenderer;
    public Renderer VisualRenderer => visualRenderer;

    [SerializeField] private Rigidbody rb;

    [SerializeField] private bool isLethal;
    public bool IsLethal => isLethal;

    [SerializeField] private bool isMoving;
    public bool IsMoving => isMoving;

    [SerializeField] private Material obstacleMaterial;
    [SerializeField] private Material obstacleLethalMaterial;

    [SerializeField] private int colorIndex;

    private void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player")) {
            rb.useGravity = true;

            if(isLethal) {
                Debug.Log("Game over");
                collision.gameObject.GetComponent<PlayerHandler>().Die();
            }
        }

        else if(collision.gameObject.layer == LayerMask.NameToLayer("Obstacle")) {
            if(isMoving) {
                DOTween.Kill(transform);
            }
            rb.useGravity = true;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("ColorChanger")) {
            colorIndex++;
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
    public void SetColor() {
        if(IsLethal) {
            visualRenderer.sharedMaterial.color = LevelSettings.Level.GetObstacleColor(colorIndex);
        }
        else {
            visualRenderer.sharedMaterial.color = LevelSettings.Level.GetPlayerColor(colorIndex);
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
