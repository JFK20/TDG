using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceTurret : StandardTurret
{
    protected override void Update() {
        if (target == null || target.gameObject.GetComponent<EnemyMovement>().isSlowed) {
            FindTarget();
            return;
        }
        
        RotateTowardsTarget();
        
        if (!CheckTargetIsInRange()) {
            target = null;
        }
        else {
            timeUntilFire += Time.deltaTime;

            if (timeUntilFire >= 1f / bps) {
                Shoot();
                timeUntilFire = 0;
            }
        }
    }

    protected override void Shoot() {
        GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
        StandardBullet standardBulletScript = bulletObj.GetComponent<StandardBullet>();
        standardBulletScript.SetTarget(target);
        target.gameObject.GetComponent<EnemyMovement>().isSlowed = true;
    }

    protected override void FindTarget() {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position,
            0f, enemyMask);

        if (hits.Length > 0) {
            int length = hits.Length;
            for (int i = 0; i < length; i++) {
                if (FoundTarget(hits,i)) {
                    return;
                }
            }
        }
    }

    private bool FoundTarget(RaycastHit2D[] _hits,int position) {
        Transform tmp = _hits[position].transform;
        if (!tmp.gameObject.GetComponent<EnemyMovement>().isSlowed) {
            target = tmp;
            return true;
        }
        return false;
    }
    
}
