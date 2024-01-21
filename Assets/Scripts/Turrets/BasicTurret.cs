using System;
using System.Collections;
using System.Collections.Generic;
using UI;
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
        base.UpgradeBps();
    }
    
    protected override void UpgradeRange(){
        base.UpgradeRange();
    }
}
