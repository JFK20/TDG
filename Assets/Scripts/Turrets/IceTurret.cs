using System.Collections;
using Enemy;
using Sounds;
using UI;
using UnityEngine;

namespace Turrets
{
    public class IceTurret : StandardTurret {
        private int maxLevelAps;
        private float baseAps;
        private int levelAps = 1;

        [Header("Attributes")] 
        [SerializeField] protected float aps= 1f; //attacks per second
        [SerializeField] protected float slowingAmount = 2f;
        [SerializeField] protected float slowingTime = 2f;

        protected override void Start() {
            base.Start();
            baseAps = aps;
            baseTargetingRange = targetingRange;

            maxLevelAps = 5;
            maxLevelRange = 3;
        }
    
        protected override void Update() {
            timeUntilFire += Time.deltaTime;
        
            if (timeUntilFire >= 1f / aps) {
                FreezeEnemies();
                timeUntilFire = 0;
            }
        }

        private void FreezeEnemies() {
            RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position,
                0f, enemyMask);

            if (hits.Length > 0) {
                for (int i = 0; i < hits.Length; i++) {
                    RaycastHit2D hit = hits[i];

                    EnemyMovement em = hit.transform.GetComponent<EnemyMovement>();
                    em.UpdateSpeed(slowingAmount);
                    em.gameObject.GetComponent<SpriteRenderer>().color += Color.blue;
                    StartCoroutine(ResetEnemySpeed(em));
                }
            }
        }

        private IEnumerator ResetEnemySpeed(EnemyMovement em) {
            yield return new WaitForSeconds(slowingTime);
        
            em.ResetSpeed();
        }

        public void UpgradeAps()
        {
            UpgradeAttribute(ref levelAps, maxLevelAps, upgradeUIHandler.UpgradedStatOne, 1);
            aps = CalculateAps(levelAps);
        }
    
        public override void UpgradeRange(){
            base.UpgradeRange();
        }

        private float CalculateAps(int level)
        {
            return baseAps+ (level-1) * 0.1875f;
        }
        
    }
}
