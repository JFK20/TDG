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
        if (amount > lifes) {
            SceneManager.LoadScene(0);
        }
        else {
            lifes -= amount;
        }
    }
}
