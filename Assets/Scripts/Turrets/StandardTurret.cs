using Sounds;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Turrets
{
    public abstract class StandardTurret : MonoBehaviour {

        [Header("References")] 
        [SerializeField] protected Transform turretRotationPoint;
        [SerializeField] protected LayerMask enemyMask;
        [SerializeField] protected GameObject bulletPrefab;
        [SerializeField] protected Transform firingPoint;
        [SerializeField] protected GameObject upgradeUI;
        protected UpgradeUIHandler upgradeUIHandler;
    
        [Header("Attributes")] 
        [SerializeField] protected float targetingRange = 5f;
        [SerializeField] protected float rotationSpeed = 150f;
        [SerializeField] protected float bps = 1f; //bullets per second
        [SerializeField] protected int cost = 100;
    
        protected float baseTargetingRange = 5f;
        protected float baseBps = 1f;

        protected Transform target;
        protected float timeUntilFire;
    
        protected int levelBps = 1;
        protected int levelRange = 1;
        protected int levelDmg = 1;
    
        protected int maxLevelBps;
        protected int maxLevelRange;
        protected int maxLevelDmg;

        public delegate void UpgradeUIHandlerMethod(int level, int cost);
        
        protected virtual void Start() {
            /*if (this.GetType() == typeof(IceTurret)) { return;}
        baseBps = bps;
        baseTargetingRange = targetingRange;*/
            upgradeUIHandler = upgradeUI.GetComponent<UpgradeUIHandler>();
        }

        protected virtual void Update() {
            if (target == null) {
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

        protected virtual void Shoot() {
            /*GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
        Bullet bulletScript = bulletObj.GetComponent<Bullet>();
        bulletScript.SetTarget(target);*/
            throw new InheritanceException();
        }
    
        protected bool CheckTargetIsInRange() {
            return Vector2.Distance(target.position, transform.position) <= targetingRange;
        }

        protected void RotateTowardsTarget() {
            float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg + 90f;

            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
            turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, targetRotation,rotationSpeed * Time.deltaTime);
        }

        protected virtual void FindTarget() {
            RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position,
                0f, enemyMask);

            if (hits.Length > 0) {
                target = hits[0].transform;
            }
        }

        /*protected void OnDrawGizmosSelected() {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    }*/

        public void OpenUpgradeUI() {
            upgradeUI.SetActive(true);
        }
    
        public void CloseUpgradeUI() {
            upgradeUI.SetActive(false);
            UIManager.Main.SetHoveringState(false);
        }

        public void UpgradeAttribute(ref int level, int maxLevel, UpgradeUIHandlerMethod upgradeUIHandlerMethod, int buttonNumber) {
            int upgradeCost = CalculateCost(level);
            if (upgradeCost > LevelManager.Main.currency) { return; }

            LevelManager.Main.SpendCurrency(upgradeCost);
            level++;
            upgradeUIHandlerMethod(level - 1, CalculateCost(level));
            SoundEffectPlayer.Main.BuildandUpgradeSound();
            if (level >= maxLevel) {
                upgradeUIHandler.DeactivateUpgradeButton(buttonNumber);
            }
        }
        
        public virtual void UpgradeBps() {
            UpgradeAttribute(ref levelBps, maxLevelBps, upgradeUIHandler.UpgradedStatOne, 1);
            bps = CalculateBps(levelBps);
        }

        public virtual void UpgradeRange() {
            UpgradeAttribute(ref levelRange, maxLevelRange, upgradeUIHandler.UpgradedStatTwo, 2);
            targetingRange = CalculateTargetingRange(levelRange);
        }

        public virtual void UpgradeDmg() {
            UpgradeAttribute(ref levelDmg, maxLevelDmg, upgradeUIHandler.UpgradedStatOne, 1);
        }
    
        protected virtual int CalculateCost(int level) {
            return Mathf.RoundToInt(cost * Mathf.Pow(level, 0.8f));
        }

        protected virtual float CalculateBps(int level) {
            float result = baseBps * Mathf.Pow(level, 0.5f);
            return Mathf.Round(result * 100f) / 100f;
        }
    
        protected virtual float CalculateTargetingRange(int level) {
            float result = baseTargetingRange * Mathf.Pow(level, 0.3f);
            return Mathf.Round(result * 100f) / 100f;
        }
        
    }
}
