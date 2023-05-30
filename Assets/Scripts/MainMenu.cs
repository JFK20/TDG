using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
   [SerializeField] private GameObject mainUI;
   [SerializeField] private GameObject optionsUI;
   [SerializeField] private GameObject highScoreUI;

   public void StartGame() {
      SceneManager.LoadScene(1);
   }

   public void Quit() {
      Application.Quit();
   }

   public void BackToMainMenu() {
      mainUI.SetActive(true);
      optionsUI.SetActive(false);
      highScoreUI.SetActive(false);
   }

   public void LoadOptions() {
      mainUI.SetActive(false);
      optionsUI.SetActive(true);
   }
   
   public void LoadHighscore() {
      mainUI.SetActive(false);
      highScoreUI.SetActive(true);
   }
   
}
