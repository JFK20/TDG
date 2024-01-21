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
            baseAps = aps;
            baseTargetingRange = targetingRange;

            maxLevelAps = 5;
            maxLevelRange = 3;
        
            upgradeButton1.onClick.AddListener(UpgradeAps);
            upgradeButton2.onClick.AddListener(UpgradeRange);
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

        private void UpgradeAps(){
            int upgradeCost = CalculateCost(levelAps);
            if (upgradeCost > LevelManager.Main.currency) { return; }

            LevelManager.Main.SpendCurrency(upgradeCost);
            levelAps++;
            upgradeUI.GetComponent<UpgradeUIHandler>().UpgradedStatOne(levelAps - 1,CalculateCost(levelAps));
            aps = CalculateBps(levelAps);
            SoundEffectPlayer.Main.BuildandUpgradeSound();
            if (levelAps >= maxLevelAps) {
                upgradeButton1.interactable = false;
            }
        }
    
        protected override void UpgradeRange(){
            base.UpgradeRange();
        }
    
    }
}
