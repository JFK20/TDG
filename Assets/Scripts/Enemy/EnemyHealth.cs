using Sounds;
using UnityEngine;

namespace Enemy
{
    public class EnemyHealth : MonoBehaviour {
    
        [Header("Attributes")] 
        [SerializeField] private int hitPoints = 2;
        public int HitPoints => hitPoints;

        [SerializeField] private int currencyWorth = 50;

        private bool isDestroyed = false;

        public void TakeDamage(int damage) {
            hitPoints -= damage;

            if (hitPoints <= 0 && !isDestroyed) {
                EnemySpawner.onEnemyKilled.Invoke();
                LevelManager.Main.IncreaseCurrency(currencyWorth);
                isDestroyed = true;
                SoundEffectPlayer.Main.KillSound();
                Destroy(gameObject);
            }
        }
    }
}
