using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    
    protected void UpgradeAps(){
        if (CalculateCost(levelAps) > LevelManager.Main.currency) { return; }

        LevelManager.Main.SpendCurrency(CalculateCost(levelAps));
        upgradeButton1Points[levelAps - 1].GetComponent<SpriteRenderer>().color = Color.yellow;
        levelAps++;
        aps = CalculateBps(levelAps);
        SoundEffectPlayer.Main.BuildandUpgradeSound();
        if (levelAps >= maxLevelAps) {
            upgradeButton1.interactable = false;
        }
    }
    
    protected override void UpgradeRange(){
        if (CalculateCost(levelRange) > LevelManager.Main.currency) { return; }

        LevelManager.Main.SpendCurrency(CalculateCost(levelRange));
        upgradeButton2Points[levelRange - 1].GetComponent<SpriteRenderer>().color = Color.yellow;
        levelRange++;
        targetingRange = CalculateTargetingRange(levelRange);
        SoundEffectPlayer.Main.BuildandUpgradeSound();
        if (levelRange >= maxLevelRange) {
            upgradeButton2.interactable = false;
        }
    }
    
}
