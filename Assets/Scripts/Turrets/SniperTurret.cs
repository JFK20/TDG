using Sounds;
using UnityEngine;

namespace Turrets
{
    public class SniperTurret : StandardTurret
    {
        protected override void Shoot() {
            GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
            StandardBullet standardBulletScript = bulletObj.GetComponent<StandardBullet>();
            standardBulletScript.Damage = levelDmg;
            SoundEffectPlayer.Main.ShootSound();
            standardBulletScript.SetTarget(target);
        }
    
        protected override void Start() {
            base.Start();
            baseBps = bps;
            baseTargetingRange = targetingRange;
        
            maxLevelDmg = 5;
            maxLevelRange = 3;
        }
    
        public override void UpgradeDmg(){
            base.UpgradeDmg();
        }

        public override void UpgradeRange() {
            base.UpgradeRange();
        }
    
    }
}
