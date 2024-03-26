using System.Collections;
using UnityEditor.Hardware;
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
            [SerializeField] private DetectionStats detectionStats;
           

            private void OnEnable()
            {
                // Subscribe to jump action when enabled
                input.JumpAction.action.started += JumpActionPress;
            }

            private void OnDisable()
            {
                // Unsubscribe from jump action when disabled
                input.JumpAction.action.started -= JumpActionPress;
            }

            // Method triggered when jump action is performed
            private void JumpActionPress(InputAction.CallbackContext context)
            {
                // Check if the character can jump
                if (characterStats.NumberOfJumps < characterStats.MaxJump && !characterStats.IsDashing)
                {
                    // Increment jump count, set jump flag to true, and reset ended jump early flag
                    characterStats.NumberOfJumps++;
                    characterStats.IsJump = true;
                    characterStats.EndedJumpEarly = false;
                   Debug.Log(context.duration);
                }
            }

            // Method to execute the jump
            public void Jump()
            {
                // Check if the character can jump
                if (!characterStats.IsJump) return;

                // Apply vertical velocity for jump
                if (detectionStats.IsClimbing && characterStats.MoveVector.x == 0)
                {
                    float forceY = characterComponents.Rb.velocity.y < 5 ? 13 : 8;
                    float forceX = characterComponents.Sr.flipX ? 5 : -5;
                    characterComponents.Rb.AddForce(new Vector2(forceX, forceY), ForceMode2D.Impulse);
                    characterComponents.Sr.flipX = !characterComponents.Sr.flipX;
                    detectionStats.IsClimbing = false;
                }
                else
                {
                    characterComponents.Rb.velocity = new Vector2(characterStats.MoveVector.x, characterStats.JumpForce);
                }
                // Reset jump flag
                characterStats.IsJump = false;
            }
            
            // Method to apply gravity
            public void ApplyGravity()
            {
                // If the jump ended early, adjust gravity
                    if (characterStats.EndedJumpEarly && characterComponents.Rb.velocity.y > 0)
                    {
                        characterComponents.Rb.gravityScale *= characterStats.JumpEndEarlyGravityModifier;
                        //Debug.Log(gravity);
                    }
                
                    // If character is grounded or moving upwards, exit
                    if (detectionStats.IsGrounded || !(characterComponents.Rb.gravityScale <= 0f)) return;
                
                     var newVelocityY = Mathf.MoveTowards(characterComponents.Rb.velocity.y, 5,
                         characterComponents.Rb.gravityScale);
                    characterComponents.Rb.velocity = new Vector2(characterComponents.Rb.velocity.x, newVelocityY);
            }


            // Method to handle early jump end
            public void HandleJumpEnd()
            {
                // If character is not grounded, not jumping, and moving upwards, set jump ended early flag
                if (!detectionStats.IsGrounded && !characterStats.IsJump && characterComponents.Rb.velocity.y > 0)
                {
                    characterStats.EndedJumpEarly = true;
                }
            }
        }
    }
