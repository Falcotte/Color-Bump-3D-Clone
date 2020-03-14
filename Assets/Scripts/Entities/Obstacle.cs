﻿using UnityEngine;

#pragma warning disable 0649
public class Obstacle : MonoBehaviour {
    [SerializeField] private Color color;
    [SerializeField] private bool isLethal;

    private void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player")) {
            if(isLethal) {
                Debug.Log("Game over");
            }
        }
    }
}