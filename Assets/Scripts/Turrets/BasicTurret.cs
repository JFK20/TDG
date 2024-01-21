using Sounds;
using UnityEngine;

namespace Turrets
{
    public class BasicTurret : StandardTurret {

        protected override void Shoot() {
            GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
            StandardBullet standardBulletScript = bulletObj.GetComponent<StandardBullet>();
            SoundEffectPlayer.Main.ShootSound();
            standardBulletScript.SetTarget(target);
        }

        protected override void Start() {
            base.Start();
            baseBps = bps;
            baseTargetingRange = targetingRange;

            maxLevelBps = 5;
            maxLevelRange = 3;
        }
    
        public override void UpgradeBps(){
            base.UpgradeBps();
        }
    
        public override void UpgradeRange(){
            base.UpgradeRange();
        }
    }
}
