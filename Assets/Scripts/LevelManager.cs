using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
    
    public static LevelManager Main;

    public Transform startPoint;
    public Transform[] path;

    public int currency;

    private void Awake() {
        Main = this;
    }

    private void Start() {
        currency = 200;
    }

    public void IncreaseCurrency(int amount) {
        currency += amount;
    }

    public bool SpendCurrency(int amount) {
        if (amount <= currency) {
            //Buy Tower
            currency -= amount;
            return true;
        }
        else {
            Debug.Log("Pleite");
            return false;
        }
    }
}
