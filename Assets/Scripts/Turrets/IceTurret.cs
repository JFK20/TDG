using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceTurret : StandardTurret
{
    [Header("Attributes")] 
    [SerializeField] protected float aps= 1f; //attacks per second
    [SerializeField] protected float slowingAmount = 2f;
    [SerializeField] protected float slowingTime = 2f;

    private float baseAps;
    private int levelAps = 1;
    
    protected override void Start() {
        baseAps = aps;
        baseTargetingRange = targetingRange;
        
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
    
    protected void UpgradeAps(){
        if (CalculateCost(levelAps) > LevelManager.Main.currency) { return; }

        LevelManager.Main.SpendCurrency(CalculateCost(levelAps));
        levelAps++;
        aps = CalculateBps(levelAps);
        SoundEffectPlayer.Main.BuildandUpgradeSound();
        if (levelAps >= 5) {
            upgradeButton1.interactable = false;
        }
    }
    
    protected override void UpgradeRange(){
        if (CalculateCost(levelRange) > LevelManager.Main.currency) { return; }

        LevelManager.Main.SpendCurrency(CalculateCost(levelRange));
        levelRange++;
        targetingRange = CalculateTargetingRange(levelRange);
        SoundEffectPlayer.Main.BuildandUpgradeSound();
        if (levelRange >= 3) {
            upgradeButton2.interactable = false;
        }
    }
    
}
