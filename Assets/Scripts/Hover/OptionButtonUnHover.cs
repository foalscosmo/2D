using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Hover
{
    public class OptionButtonUnHover : MonoBehaviour
    {
        // Serialized fields can be set in the Unity Inspector
        [SerializeField] private List<Button> buttons = new List<Button>(); // List of buttons to track
        [SerializeField] private EventSystem eventSystem; // Reference to the Event System
        public event Action OnResetHover; // Event invoked when hover is reset

        // Called when the script instance is being loaded
        private void Awake()
        {
            // Loop through all buttons
            for (var i = 0; i < buttons.Count; i++)
            {
                var index = i;
                // Get or add EventTrigger component to the button's GameObject
                var eventTrigger = buttons[i].gameObject.GetComponent<EventTrigger>() 
                                   ?? buttons[i].gameObject.AddComponent<EventTrigger>();

                // Create a new EventTrigger entry for deselect event
                var entry = new EventTrigger.Entry()
                {
                    eventID = EventTriggerType.Deselect
                };
                // Add a listener to the deselect event that calls ResetHover method with the button's index
                entry.callback.AddListener(_ => ResetHover(index));
                eventTrigger.triggers.Add(entry);
            }
        }

        // Resets hover for a button at the specified index
        private void ResetHover(int index)
        {
            // Check if there is no gamepad connected and certain keys are pressed, or if the selected GameObject is not the button
            if ((Gamepad.current == null && (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.Escape) ||
                 Input.GetKeyDown(KeyCode.A))) ||
                (Gamepad.current != null && (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.Escape) ||
                 Input.GetKeyDown(KeyCode.A) || Gamepad.current.buttonEast.isPressed ||
                 Gamepad.current.dpad.left.isPressed || IsLeftJoystickLeft())) &&
                eventSystem.currentSelectedGameObject != buttons[index].gameObject)
            {
                // Change the button's target graphic color to grey
                buttons[index].targetGraphic.color = Color.grey;
                // Invoke the OnResetHover event
                OnResetHover?.Invoke();
            }
        }

        // Checks if the left joystick is tilted left
        private static bool IsLeftJoystickLeft()
        {
            // Get the left stick value of the gamepad, if connected
            var leftStickValue = Gamepad.current != null ? Gamepad.current.leftStick.ReadValue() : Vector2.zero;
            // Return true if left stick's x value is less than -0.5
            return leftStickValue.x < -0.5f; 
        }
    }
}