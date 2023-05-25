using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour {
    [Header("References")] 
    [SerializeField] private GameObject[] enemyPrefab;
    
    [Header("Attributes")] 
    [SerializeField] private int baseEnemy = 8;
    [SerializeField] private float enemiesPerSecond = 0.5f;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float difficultyScalingFactor = 0.7f;
    [SerializeField] private float maxEps = 15f;

    [Header("Events")] 
    public static UnityEvent onEnemyKilled = new UnityEvent();

    private int currentWave = 1;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private float eps; //Enemies Per Second
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
        if (timeSinceLastSpawn >= (1f / eps) && enemiesLeftToSpawn > 0) {
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
        eps = EnemiesPerSecond();
        isSpawning = true;
    }

    private void SpawnEnemy() {
        int rand = Random.Range(0, 10);
        GameObject prefabToSpawn;
        if (rand <= 7.5) {
            prefabToSpawn = enemyPrefab[0];
        }
        else {
            prefabToSpawn = enemyPrefab[1];
        }
        Instantiate(prefabToSpawn, LevelManager.Main.startPoint.position, Quaternion.identity);
    }

    private int EnemiesPerWave() {
        return Mathf.RoundToInt(baseEnemy * Mathf.Pow(currentWave, difficultyScalingFactor));
    }
    
    private float EnemiesPerSecond() {
        return Mathf.Clamp(enemiesPerSecond * Mathf.Pow(currentWave, difficultyScalingFactor), 0, maxEps);
    }
}
