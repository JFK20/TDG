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
    public bool isSlowed = false;

    private void Start() {
        target = LevelManager.Main.path[pathIndex];
    }

    private void Update() {
        if (Vector2.Distance(target.position,this.transform.position) <= 0.1f) {
            pathIndex++;

            if (pathIndex == LevelManager.Main.path.Length) {
                EnemySpawner.onEnemyKilled.Invoke();
                Destroy(this.gameObject);
                return;
            }
            else {
                target = LevelManager.Main.path[pathIndex];
            }
        }
    }

    private void FixedUpdate() {
        Vector2 direction = (target.position - this.transform.position).normalized;

        rb.velocity = direction * moveSpeed;
    }

    private IEnumerator ChangeSpeedCoroutine(float amount,int duration) {
        float baseMoveSpeed = moveSpeed;
        moveSpeed += amount;
        yield return new WaitForSeconds(duration);
        moveSpeed = baseMoveSpeed;
        isSlowed = false;
    }

    public void ChangeSpeed(float amount,int duration) {
        if (moveSpeed + amount < 0) { return; }
        StartCoroutine(ChangeSpeedCoroutine(amount, duration));
    }
}
