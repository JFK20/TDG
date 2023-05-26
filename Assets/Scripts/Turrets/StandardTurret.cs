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
    [SerializeField] protected Button upgradeButton;
    
    [Header("Attributes")] 
    [SerializeField] protected float targetingRange = 5f;
    [SerializeField] protected float rotationSpeed = 5f;
    [SerializeField] protected float bps = 1f; //bullets per second

    protected Transform target;
    protected float timeUntilFire;

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
        Debug.Log("GetFromInheritance");
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

    protected void OnDrawGizmosSelected() {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    }

    public void OpenUpgradeUI() {
        upgradeUI.SetActive(true);
    }
    
    public void CloseUpgradeUI() {
        upgradeUI.SetActive(false);
    }
}
