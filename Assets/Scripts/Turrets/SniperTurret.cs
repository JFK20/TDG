using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperTurret : StandardTurret
{
    protected override void Shoot() {
        GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
        StandardBullet standardBulletScript = bulletObj.GetComponent<StandardBullet>();
        standardBulletScript.Damage = levelDmg;
        standardBulletScript.SetTarget(target);
    }
    
    protected override void Start() {
        baseBps = bps;
        baseTargetingRange = targetingRange;
        
        upgradeButton1.onClick.AddListener(UpgradeDmg);
        upgradeButton2.onClick.AddListener(UpgradeRange);
    }
    
    protected override void UpgradeDmg(){
        if (CalculateCost(levelDmg) > LevelManager.Main.currency) { return; }

        LevelManager.Main.SpendCurrency(CalculateCost(levelDmg));
        levelDmg++;
        if (levelDmg >= 5) {
            upgradeButton1.enabled = false;
        }
    }

    protected override void UpgradeRange() {
        if (CalculateCost(levelRange) > LevelManager.Main.currency) { return; }

        LevelManager.Main.SpendCurrency(CalculateCost(levelRange));
        levelRange++;
        targetingRange = CalculateTargetingRange(levelRange);
        if (levelRange >= 4) {
            upgradeButton2.enabled = false;
        }
    }
    
}
