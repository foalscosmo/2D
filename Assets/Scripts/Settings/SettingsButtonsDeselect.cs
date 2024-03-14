using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Settings
{
    // Deselects previous buttons when any button in the list is clicked
    public class SettingsButtonsDeselect : MonoBehaviour
    {
        [SerializeField] private List<Button> previousButtons = new List<Button>(); // List of buttons to be deselected
        [SerializeField] private List<Button> buttons = new List<Button>(); // List of buttons that trigger deselection

        // Called when the object is initialized
        private void Start()
        {
            // Add listener to each button in the list
            foreach (var changer in buttons)
            {
                changer.onClick.AddListener(DeselectButtons);
            }
        }

        // Deselects previous buttons when any button in the list is clicked
        private void DeselectButtons()
        {
            // Iterate through each button in the previous buttons list
            foreach (var button in previousButtons.Where(button => button.targetGraphic.color == Color.white))
            {
                // Change the button's color to a custom gray
                button.targetGraphic.color = new Color(0.56f, 0.56f, 0.56f);
            }
        }
    }
}