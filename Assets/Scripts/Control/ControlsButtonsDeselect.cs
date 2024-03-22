using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

// Namespace for control-related functionality
namespace Control
{
    // Class responsible for deselecting control buttons
    public class ControlsButtonsDeselect : MonoBehaviour
    {
        // List of buttons whose previous state needs to be tracked
        [SerializeField] private List<Button> previousButtons = new List<Button>();

        // List of buttons to listen for click events
        [SerializeField] private List<Button> buttons = new List<Button>();

        // Start is called before the first frame update
        private void Start()
        {
            // Attach a listener to each button to trigger deselecting
            foreach (var button in buttons)
            {
                button.onClick.AddListener(DeselectButtons);
            }
        }

        // Method to deselect buttons
        private void DeselectButtons()
        {
            // Loop through previous buttons
            foreach (var button in previousButtons.Where(button => button.targetGraphic.color == Color.white))
            {
                // Change the color of the button to grey to indicate deselection
                button.targetGraphic.color = Color.grey;
            }
        }
    }
}