using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectPlayer : MonoBehaviour {
    public static SoundEffectPlayer Main;
    
    [SerializeField] private AudioSource src1, src2, src3;
    [SerializeField] private AudioClip sfx1, sfx2, sfx3;

    private void Awake() {
        Main = this;
    }

    public void ShootSound() {
        src1.clip = sfx1;
        src1.Play();
    }
    
    public void KillSound() {
        src2.clip = sfx2;
        src2.Play();
    }
    
    public void BuildandUpgradeSound() {
        src3.clip = sfx3;
        src3.Play();
    }
}
