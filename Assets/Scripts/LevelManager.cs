using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    
    public static LevelManager Main;

    public Transform startPoint;
    public Transform[] path;

    public int currency;
    public int lifes;
    

    private void Awake() {
        Main = this;
    }

    private void Start() {
        currency = 200;
        lifes = 100;
    }

    public void IncreaseCurrency(int amount) {
        currency += amount;
    }

    public bool SpendCurrency(int amount) {
        if (amount <= currency) {
            currency -= amount;
            return true;
        }
        else {
            Debug.Log("Pleite");
            return false;
        }
    }

    public void DecreaseLife(int amount) {
        if (lifes - amount <= 0) {
            // ends game
            int score = this.gameObject.GetComponent<EnemySpawner>().CurrentWave;
            AddScores(score);
            SceneManager.LoadScene(0);
        }
        else {
            lifes -= amount;
        }
    }

    public void AddScores(int toAdd) {
        int[] scores = new int[11];
        for (int i = 0; i < 10; i++) {
            try {
                scores[i] = PlayerPrefs.GetInt(i.ToString());
            }
            catch (Exception e) {
                scores[i] = 0;
                Console.WriteLine(e);
            }
        }
        scores[10] = toAdd;
        Array.Sort(scores);
        Array.Reverse(scores);
        foreach (var k in scores) {
            Debug.Log(k);
        }
        
        for (int j = 0; j < 10; j++) {
            PlayerPrefs.SetInt(j.ToString(),scores[j]);
        }
    }
    
}
