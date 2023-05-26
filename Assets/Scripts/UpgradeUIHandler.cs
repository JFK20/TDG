using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeUIHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public bool mouse_over = false;

    private void Update() {
        if (mouse_over) {
            Debug.Log("Mouse Over:" + this.name);
        }
    }

    public void OnPointerEnter(PointerEventData eventData) {
        mouse_over = true;
        UIManager.Main.SetHoveringState(true);
    }
    
    public void OnPointerExit(PointerEventData eventData) {
        mouse_over = false;
        UIManager.Main.SetHoveringState(false);
        gameObject.SetActive(false);
    }
    
}
