using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {
    public static UIManager Main;

    private bool isHoveringUI;
    
    private void Awake() {
        Main = this;
    }

    public void SetHoveringState(bool state) {
        isHoveringUI = state;
    }

    public bool IsHoveringUI() {
        return isHoveringUI;
    }
    
}
