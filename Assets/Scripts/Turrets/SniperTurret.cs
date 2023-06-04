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
        if (CalculateCost(levelDmg) > LevelManager.Main.currency) { return; }

        LevelManager.Main.SpendCurrency(CalculateCost(levelDmg));
        upgradeButton1Points[levelDmg - 1].GetComponent<SpriteRenderer>().color = Color.yellow;
        levelDmg++;
        SoundEffectPlayer.Main.BuildandUpgradeSound();
        if (levelDmg >= maxLevelDmg) {
            upgradeButton1.interactable = false;
        }
    }

    protected override void UpgradeRange() {
        if (CalculateCost(levelRange) > LevelManager.Main.currency) { return; }

        LevelManager.Main.SpendCurrency(CalculateCost(levelRange));
        upgradeButton2Points[levelRange - 1].GetComponent<SpriteRenderer>().color = Color.yellow;
        levelRange++;
        targetingRange = CalculateTargetingRange(levelRange);
        SoundEffectPlayer.Main.BuildandUpgradeSound();
        if (levelRange >= maxLevelRange) {
            upgradeButton2.interactable = false;
        }
    }
    
}
