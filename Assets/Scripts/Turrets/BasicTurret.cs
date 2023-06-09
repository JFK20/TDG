using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class BasicTurret : StandardTurret {

    protected override void Shoot() {
        GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
        StandardBullet standardBulletScript = bulletObj.GetComponent<StandardBullet>();
        SoundEffectPlayer.Main.ShootSound();
        standardBulletScript.SetTarget(target);
    }

    protected override void Start() {
        baseBps = bps;
        baseTargetingRange = targetingRange;

        maxLevelBps = 5;
        maxLevelRange = 3;
        
        upgradeButton1.onClick.AddListener(UpgradeBps);
        upgradeButton2.onClick.AddListener(UpgradeRange);
    }
    
    protected override void UpgradeBps(){
        if (CalculateCost(levelBps) > LevelManager.Main.currency) { return; }

        LevelManager.Main.SpendCurrency(CalculateCost(levelBps));
        upgradeButton1Points[levelBps - 1].GetComponent<SpriteRenderer>().color = Color.yellow;
        levelBps++;
        bps = CalculateBps(levelBps);
        SoundEffectPlayer.Main.BuildandUpgradeSound();
        if (levelBps >= maxLevelBps) {
            upgradeButton1.interactable = false;
        }
    }
    
    protected override void UpgradeRange(){
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
