using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
    [Header("References")] 
    [SerializeField] private Rigidbody2D rb;
    
    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 2f;

    private Transform target;
    private int pathIndex = 0;
    private float baseSpeed;

    //public bool isSlowed = false;

    private void Start() {
        target = LevelManager.Main.path[pathIndex];
        baseSpeed = moveSpeed;
    }

    private void Update() {
        if (GameTime.isPaused) { return; }
        if (Vector2.Distance(target.position,this.transform.position) <= 0.1f) {
            pathIndex++;

            if (pathIndex == LevelManager.Main.path.Length) {
                EnemySpawner.onEnemyKilled.Invoke();
                LevelManager.Main.DecreaseLife((this.gameObject.GetComponent<EnemyHealth>().HitPoints) * 5); // /2
                Destroy(this.gameObject);
                return;
            }
            else {
                target = LevelManager.Main.path[pathIndex];
            }
        }
    }

    private void FixedUpdate() {
        if (GameTime.isPaused) { return; }
        Vector2 direction = (target.position - this.transform.position).normalized;

        rb.velocity = direction * moveSpeed;
    }
    
    public void UpdateSpeed(float newSpeed) {
        if (moveSpeed - newSpeed >= 0) {
            moveSpeed -= newSpeed;
        }
    }

    public void ResetSpeed() {
        moveSpeed = baseSpeed;
        if (this != null) {
            this.gameObject.GetComponent<SpriteRenderer>().color -= Color.blue;
        }
    }
}
