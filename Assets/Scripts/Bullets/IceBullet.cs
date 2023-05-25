using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBullet : StandardBullet {
    [Header("Special Attributes")] 
    [SerializeField] private float slowAmount;
    [SerializeField] private int slowDuration;
    
    protected override void OnCollisionEnter2D(Collision2D other) {
        base.OnCollisionEnter2D(other);
        other.gameObject.GetComponent<EnemyMovement>().ChangeSpeed(slowAmount,slowDuration);
    }
}
