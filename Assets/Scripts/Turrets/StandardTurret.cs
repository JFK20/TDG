using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;


public abstract class StandardTurret : MonoBehaviour {

    [Header("References")] 
    [SerializeField] protected Transform turretRotationPoint;
    [SerializeField] protected LayerMask enemyMask;
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] protected Transform firingPoint;
    [SerializeField] protected GameObject upgradeUI;
    [SerializeField] protected Button upgradeButton1;
    [SerializeField] protected Button upgradeButton2;
    
    [Header("Attributes")] 
    [SerializeField] protected float targetingRange = 5f;
    [SerializeField] protected float rotationSpeed = 150f;
    [SerializeField] protected float bps = 1f; //bullets per second
    [SerializeField] protected int cost = 100;

    [SerializeField] protected GameObject[] upgradeButton1Points;
    [SerializeField] protected GameObject[] upgradeButton2Points;
    
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

    protected virtual void Start() {
        /*if (this.GetType() == typeof(IceTurret)) { return;}
        baseBps = bps;
        baseTargetingRange = targetingRange;*/
        throw new InheritanceException();
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

    protected virtual void UpgradeBps() {
        /*if (CalculateCost() > LevelManager.Main.currency) { return; }

        LevelManager.Main.SpendCurrency(CalculateCost());
        level++;
        bps = CalculateBps();
        targetingRange = CalculateTargetingRange();*/
        throw new InheritanceException();
    }

    protected virtual void UpgradeRange() {
        throw new InheritanceException();
    }

    protected virtual void UpgradeDmg() {
        throw new InheritanceException();
    }
    
    protected virtual int CalculateCost(int level) {
        return Mathf.RoundToInt(cost * Mathf.Pow(level, 0.8f));
        //throw new InheritanceException();
    }

    protected virtual float CalculateBps(int level) {
        return baseBps * Mathf.Pow(level, 0.5f);
        //throw new InheritanceException();
    }
    
    protected virtual float CalculateTargetingRange(int level) {
        return baseTargetingRange * Mathf.Pow(level, 0.3f);
        //throw new InheritanceException();
    }
}
