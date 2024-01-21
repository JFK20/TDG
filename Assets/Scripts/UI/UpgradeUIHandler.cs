using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace UI
{
    public class UpgradeUIHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
        public bool mouseOver = false;
        public TextMeshProUGUI upgradeCost1;
        public TextMeshProUGUI upgradeCost2;
        [SerializeField] private GameObject[] upgradeButton1Points;
        [SerializeField] private GameObject[] upgradeButton2Points;
    
        public void OnPointerEnter(PointerEventData eventData) {
            mouseOver = true;
            UIManager.Main.SetHoveringState(true);
        }
    
        public void OnPointerExit(PointerEventData eventData) {
            mouseOver = false;
            UIManager.Main.SetHoveringState(false);
            gameObject.SetActive(false);
        }
    
        public void UpgradedStatOne(int level, int cost) {
            upgradeButton1Points[level - 1].GetComponent<SpriteRenderer>().color = Color.yellow;
            upgradeCost1.text = "Cost: "  + cost.ToString();
        }
    
        public void UpgradedStatTwo(int level, int cost) {
            upgradeButton2Points[level - 1].GetComponent<SpriteRenderer>().color = Color.yellow;
            upgradeCost2.text = "Cost: " + cost.ToString();
        }
    
    }
}
