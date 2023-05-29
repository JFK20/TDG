using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class BasicTurret : StandardTurret {
    
    protected override void Shoot() {
        GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
        StandardBullet standardBulletScript = bulletObj.GetComponent<StandardBullet>();
        standardBulletScript.SetTarget(target);
    }

    protected override void Start() {
        baseBps = bps;
        baseTargetingRange = targetingRange;
        
        upgradeButton1.onClick.AddListener(UpgradeBps);
        upgradeButton2.onClick.AddListener(UpgradeRange);
    }
    
    protected override void UpgradeBps(){
        if (CalculateCost(levelBps) > LevelManager.Main.currency) { return; }

        LevelManager.Main.SpendCurrency(CalculateCost(levelBps));
        levelBps++;
        bps = CalculateBps(levelBps);
        if (levelBps >= 5) {
            upgradeButton1.interactable = false;
        }
    }
    
    protected override void UpgradeRange(){
        if (CalculateCost(levelRange) > LevelManager.Main.currency) { return; }

        LevelManager.Main.SpendCurrency(CalculateCost(levelRange));
        levelRange++;
        targetingRange = CalculateTargetingRange(levelRange);
        if (levelRange >= 3) {
            upgradeButton2.interactable = false;
        }
    }
}
