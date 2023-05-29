using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI currencyUI;
    [SerializeField] private TextMeshProUGUI lifeUI;
    [SerializeField] private Animator anim;

    private bool isMenuOpen = true;

    public void ToggleMenu() {
        isMenuOpen = !isMenuOpen;
        anim.SetBool("MenuOpen", isMenuOpen);
    }

    private void OnGUI() {
        LevelManager data = LevelManager.Main;
        String wave = data.gameObject.GetComponent<EnemySpawner>().CurrentWave.ToString();
        currencyUI.text = data.currency.ToString();
        lifeUI.text =  "lifes:  " + data.lifes.ToString() + "\n" + " wave: " + wave;
    }
}
