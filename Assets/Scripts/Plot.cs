using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Plot : MonoBehaviour {
    
    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;
    
    public GameObject towerObj;
    public StandardTurret turret;
    private Color startColor;

    private void Start() {
        startColor = sr.color;
    }

    private void OnMouseEnter() {
        sr.color = hoverColor;
    }

    private void OnMouseExit() {
        sr.color = startColor;
    }

    private void OnMouseDown() {
        if (towerObj != null) {
            if (UIManager.Main.IsHoveringUI()) {
                return;
            }
            //Open menu

            turret.OpenUpgradeUI();
            return;
            
        }

        Tower tmpTower = BuildManager.Main.GetSelectedTower();

        if (LevelManager.Main.SpendCurrency(tmpTower.cost)) {
            towerObj = Instantiate(tmpTower.turretPrefab, transform.position, Quaternion.identity);
            turret = towerObj.GetComponent<StandardTurret>();
        }
    }
}
