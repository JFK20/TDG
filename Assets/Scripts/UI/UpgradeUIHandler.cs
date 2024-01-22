using TMPro;
using Turrets;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

namespace UI
{
    public class UpgradeUIHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
        public bool mouseOver = false;
        [SerializeField] private Button upgradeButton1;
        [SerializeField] private Button upgradeButton2;
        public TextMeshProUGUI upgradeCost1;
        public TextMeshProUGUI upgradeCost2;
        [SerializeField] private GameObject[] upgradeButton1Points;
        [SerializeField] private GameObject[] upgradeButton2Points;
    
        public void OnPointerEnter(PointerEventData eventData) {
            mouseOver = true;
            UIManager.Main.SetHoveringState(true);
            CircleManager.Instance.DrawCircle(50, gameObject.GetComponentInParent<StandardTurret>().GetRange(), transform.position);
        }
    
        public void OnPointerExit(PointerEventData eventData) {
            mouseOver = false;
            UIManager.Main.SetHoveringState(false);
            gameObject.SetActive(false);
            CircleManager.Instance.DestroyCircle();
        }
    
        public void UpgradedStatOne(int level, int cost) {
            upgradeButton1Points[level - 1].GetComponent<SpriteRenderer>().color = Color.yellow;
            upgradeCost1.text = "Cost: "  + cost.ToString();
        }
    
        public void UpgradedStatTwo(int level, int cost) {
            upgradeButton2Points[level - 1].GetComponent<SpriteRenderer>().color = Color.yellow;
            upgradeCost2.text = "Cost: " + cost.ToString();
        }
        
        public void DeactivateUpgradeButton(int number) {
            if (number == 1) {
                upgradeButton1.interactable = false;
            }
            else if (number == 2) {
                upgradeButton2.interactable = false;
            }
        }
    
    }
}
