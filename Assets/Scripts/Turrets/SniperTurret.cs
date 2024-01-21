using UnityEngine;

namespace Turrets
{
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
            base.UpgradeDmg();
        }

        protected override void UpgradeRange() {
            base.UpgradeRange();
        }
    
    }
}
