using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceTurret : StandardTurret
{
    [Header("Attributes")] 
    [SerializeField] protected float aps= 1f; //attacks per second
    [SerializeField] protected float slowingAmount = 2f;
    [SerializeField] protected float slowingTime = 2f;
    
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
    
}
