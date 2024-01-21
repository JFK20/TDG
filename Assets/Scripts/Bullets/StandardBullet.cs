using System;
using System.Collections;
using System.Collections.Generic;
using Enemy;
using UnityEngine;

public abstract class StandardBullet : MonoBehaviour {

    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    
    [Header("Attributes")] 
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private int damage = 1;

    public int Damage {
        get => damage;
        set => damage = value;
    }

    private Transform target;

    public void SetTarget(Transform _target) {
        target = _target;
    }

    private void FixedUpdate() {
        if (!target) { return; }
        Vector2 direction = (target.position - transform.position).normalized;

        rb.velocity = direction * bulletSpeed;
    }

    protected virtual void OnCollisionEnter2D(Collision2D other) {
        other.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
        Destroy(gameObject);
    }

    private void OnBecameInvisible() {
        Destroy(this.gameObject);
    }
}
