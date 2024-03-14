using System.Collections;
using UnityEngine;

namespace Player.PlayerMovement // Namespace for managing player movement
{
    // Class representing the character's dash behavior, inheriting from MonoBehaviour
    public class CharacterDash : MonoBehaviour
    {
        [SerializeField] private CharacterStats characterStats; // Reference to character's statistics
        [SerializeField] private DetectionStats detectionStats; // Reference to detection statistics
        private bool canDash = true; // Flag to indicate if character can dash

        // Method to trigger a dash action
        public void Dash()
        {
            // Initiates dash only if character can dash
            if (canDash) StartCoroutine(DashTimer());
        }

        // Coroutine to handle dash duration
        private IEnumerator DashTimer()
        {
            // Records start time and dash duration
            var startTime = Time.time;
            var dashTime = characterStats.DashDuration;
            characterStats.IsDashing = true; // Flags character as dashing
            canDash = false; // Disables dash during the dash action

            // Loop until dash time is reached
            while (Time.time - startTime < dashTime)
            {
                characterStats.DashTimer = dashTime - (Time.time - startTime); // Updates dash timer
                yield return new WaitForFixedUpdate(); // Waits for fixed update
            }

            // Resets character's move speed after dash based on ledge detection
            characterStats.MoveSpeed = detectionStats.IsLedgeDetected ? 0 : 5;
            characterStats.IsDashing = false; // Flags character as not dashing
            yield return new WaitForSeconds(0.1f); // Adds a short delay after dash
            canDash = true; // Enables dash again
        }
    }
}
