using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighScore : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI textField;
    public void Start()
    {
        int[] scores = new int[10];
        for (int i = 0; i < 10; i++) {
            try {
                scores[i] = PlayerPrefs.GetInt(i.ToString());
            }
            catch (Exception e) {
                scores[i] = 0;
                Console.WriteLine(e);
            }
        }

        for (int i = 0; i < scores.Length; i++) {
            textField.text += (i+1).ToString() + ": " + scores[i].ToString()  + "\n";
        }
    }
}
