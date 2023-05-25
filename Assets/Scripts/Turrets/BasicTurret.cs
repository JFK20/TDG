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
}
