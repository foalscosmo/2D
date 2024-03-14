using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Panel
{
    // Disables option panels when corresponding buttons are clicked
    public class OptionPanelsDisabler : MonoBehaviour
    {
        [SerializeField] private List<GameObject> optionPanels = new List<GameObject>(); // List of option panels
        [SerializeField] private List<Button> optionButtons = new List<Button>(); // List of buttons corresponding to option panels

        // Called when the object becomes enabled and active
        private void Awake()
        {
            // Add listeners to all option buttons
            foreach (var button in optionButtons)
            {
                button.onClick.AddListener(DisablePanels);
            }
        }

        // Disables all active option panels
        private void DisablePanels()
        {
            // Iterate through option panels and deactivate if active
            foreach (var panel in optionPanels.Where(panel => panel.activeSelf))
            {
                panel.SetActive(false);
            }
        }
    }
}