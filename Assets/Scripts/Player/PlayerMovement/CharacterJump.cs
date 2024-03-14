using UnityEngine;
using UnityEngine.InputSystem; // Assuming you're using the new Input System

namespace Player.PlayerMovement
{
    public class CharacterJump : MonoBehaviour
        {
            [SerializeField] private CharacterStats characterStats; // Reference to character's stats
            [SerializeField] private CharacterComponents characterComponents; // Reference to character's components
            [SerializeField] private CharacterDetection characterDetection; // Reference to character's detection
            [SerializeField] private CharacterInput input; // Reference to character's input

            private void OnEnable()
            {
                // Subscribe to jump action when enabled
                input.JumpAction.action.performed += JumpActionPress;
            }

            private void OnDisable()
            {
                // Unsubscribe from jump action when disabled
                input.JumpAction.action.performed -= JumpActionPress;
            }

            // Method triggered when jump action is performed
            private void JumpActionPress(InputAction.CallbackContext context)
            {
                // Check if the character can jump
                if (characterStats.NumberOfJumps < characterStats.MaxJump)
                {
                    // Increment jump count, set jump flag to true, and reset ended jump early flag
                    characterStats.NumberOfJumps++;
                    characterStats.IsJump = true;
                    characterStats.EndedJumpEarly = false;
                }
            }

            // Method to execute the jump
            public void Jump()
            {
                // Check if the character can jump
                if (!characterStats.IsJump) return;

                // Apply vertical velocity for jump
                characterComponents.Rb.velocity = new Vector2(characterComponents.Rb.velocity.x, characterStats.JumpForce);

                // Reset jump flag
                characterStats.IsJump = false;
            }

            // Method to apply gravity
            public void ApplyGravity()
            {
                // Retrieve current vertical velocity
                var currentVelocityY = characterComponents.Rb.velocity.y;
                var gravity = characterStats.Gravity; // Gravity force
                var maxFallSpeed = characterStats.MaxFallSpeed; // Maximum fall speed

                // If the jump ended early, adjust gravity
                if (characterStats.EndedJumpEarly && currentVelocityY > 0)
                {
                    gravity *= characterStats.JumpEndEarlyGravityModifier;
                }

                // If character is grounded or moving upwards, exit
                if (characterDetection.IsGrounded() || !(currentVelocityY <= 0f)) return;

                // Apply gravity
                var newVelocityY = Mathf.MoveTowards(currentVelocityY, -maxFallSpeed, gravity * Time.deltaTime);
                characterComponents.Rb.velocity = new Vector2(characterComponents.Rb.velocity.x, newVelocityY);

                // Increase gravity scale if character has jumped at least once
                if (characterStats.NumberOfJumps >= 1) characterComponents.Rb.gravityScale = 4;
            }

            // Method to handle early jump end
            public void HandleJumpEnd()
            {
                // If character is not grounded, not jumping, and moving upwards, set jump ended early flag
                if (!characterDetection.IsGrounded() && !characterStats.IsJump && characterComponents.Rb.velocity.y > 0)
                {
                    characterStats.EndedJumpEarly = true;
                }
            }
        }
    }
