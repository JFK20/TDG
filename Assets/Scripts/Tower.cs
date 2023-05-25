using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Tower {

    public string name;
    public int cost;
    public GameObject turretPrefab;

    public Tower(string name, int cost, GameObject turretPrefab) {
        this.name = name;
        this.cost = cost;
        this.turretPrefab = turretPrefab;
    }


}
