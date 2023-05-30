using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BuildManager : MonoBehaviour {
    public static BuildManager Main;

    [Header("References")]
    [SerializeField] private Tower[] towers;

    private int selectedTower = 0;
    private void Awake() {
        Main = this;
    }

    public Tower GetSelectedTower() {
        return towers[selectedTower];
    }

    public void SetSelectedTower(int _selectedTower) {
        this.selectedTower = _selectedTower;
    }
}
