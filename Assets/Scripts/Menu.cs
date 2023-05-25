using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI currencyUI;
    [SerializeField] private Animator anim;

    private bool isMenuOpen = true;

    public void ToggleMenu() {
        isMenuOpen = !isMenuOpen;
        anim.SetBool("MenuOpen", isMenuOpen);
    }

    private void OnGUI() {
        currencyUI.text = LevelManager.Main.currency.ToString();
    }

    public void SetSelected() {
        
    }
    
}
