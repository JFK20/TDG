using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour {
    [Header("References")] 
    [SerializeField] private GameObject[] enemyPrefab;
    
    [Header("Attributes")] 
    [SerializeField] private int baseEnemy = 8;
    [SerializeField] private float enemiesPerSecond = 0.5f;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float difficultyScalingFactor = 0.8f;

    [Header("Events")] 
    public static UnityEvent onEnemyKilled = new UnityEvent();

    private int currentWave = 1;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private bool isSpawning = false;

    private void Awake() {
        onEnemyKilled.AddListener(EnemyKilled);
    }

    private void Start() {
        StartCoroutine(StartWave());
    }

    private void Update() {
        if (!isSpawning) { return; }
        
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn >= (1f / enemiesPerSecond) && enemiesLeftToSpawn > 0) {
            SpawnEnemy();
            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0f;
        }

        if (enemiesAlive == 0 && enemiesLeftToSpawn == 0) {
            EndWave();
        }
    }

    private void EndWave() {
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        currentWave++;
        StartCoroutine(StartWave());
    }

    private void EnemyKilled() {
        enemiesAlive--;
    }
    
    private IEnumerator  StartWave() {
        yield return new WaitForSeconds(timeBetweenWaves);
        enemiesLeftToSpawn = EnemiesPerWave();
        isSpawning = true;
    }

    private void SpawnEnemy() {
        GameObject prefabToSpawn = enemyPrefab[0];
        Instantiate(prefabToSpawn, LevelManager.Main.startPoint.position, Quaternion.identity);
    }

    private int EnemiesPerWave() {
        return Mathf.RoundToInt(baseEnemy * Mathf.Pow(currentWave, difficultyScalingFactor));
    }
}
