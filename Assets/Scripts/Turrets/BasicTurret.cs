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
        
        upgradeButton.onClick.AddListener(Upgrade);
    }
    
    protected override void Upgrade(){
        if (CalculateCost() > LevelManager.Main.currency) { return; }

        LevelManager.Main.SpendCurrency(CalculateCost());
        level++;
        bps = CalculateBps();
        targetingRange = CalculateTargetingRange();
    }
    
    protected override int CalculateCost() {
        return Mathf.RoundToInt(cost * Mathf.Pow(level, 0.8f));
    }

    protected override float CalculateBps() {
        return baseBps * Mathf.Pow(level, 0.5f);
    }
    
    protected override float CalculateTargetingRange() {
        return baseTargetingRange * Mathf.Pow(level, 0.3f);
    }
}
