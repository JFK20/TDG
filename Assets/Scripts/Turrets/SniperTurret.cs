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
        
        maxLevelDmg = 5;
        maxLevelRange = 3;
        
        upgradeButton1.onClick.AddListener(UpgradeDmg);
        upgradeButton2.onClick.AddListener(UpgradeRange);
    }
    
    protected override void UpgradeDmg(){
        int upgradeCost = CalculateCost(levelDmg);
        if (upgradeCost > LevelManager.Main.currency) { return; }

        LevelManager.Main.SpendCurrency(upgradeCost);
        levelDmg++;
        upgradeUI.GetComponent<UpgradeUIHandler>().UpgradedStatOne(levelDmg-1, CalculateCost(levelDmg));
        SoundEffectPlayer.Main.BuildandUpgradeSound();
        if (levelDmg >= maxLevelDmg) {
            upgradeButton1.interactable = false;
        }
    }

    protected override void UpgradeRange() {
        int upgradeCost = CalculateCost(levelRange);
        if (upgradeCost > LevelManager.Main.currency) { return; }

        LevelManager.Main.SpendCurrency(upgradeCost);
        levelRange++;
        upgradeUI.GetComponent<UpgradeUIHandler>().UpgradedStatTwo(levelRange - 1,CalculateCost(levelRange));
        targetingRange = CalculateTargetingRange(levelRange);
        SoundEffectPlayer.Main.BuildandUpgradeSound();
        if (levelRange >= maxLevelRange) {
            upgradeButton2.interactable = false;
        }
    }
    
}
