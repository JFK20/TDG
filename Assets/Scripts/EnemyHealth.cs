using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {
    
    [Header("Attributes")] 
    [SerializeField] private int hitPoints = 2;
    [SerializeField] private int currencyWorth = 50;

    private bool isDestroyed = false;

    public void TakeDamage(int damage) {
        hitPoints -= damage;

        if (hitPoints <= 0 && !isDestroyed) {
            EnemySpawner.onEnemyKilled.Invoke();
            LevelManager.Main.IncreaseCurrency(currencyWorth);
            isDestroyed = true;
            Destroy(gameObject);
        }
    }
}
