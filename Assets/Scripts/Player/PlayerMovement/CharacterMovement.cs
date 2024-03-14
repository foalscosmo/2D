using UnityEngine;

// Namespace for managing player movement
namespace Player.PlayerMovement
{
    // Class responsible for character movement
    public class CharacterMovement : MonoBehaviour
    {
        // Serialized fields for character components and input
        [SerializeField] private CharacterComponents characterComponents;
        [SerializeField] private CharacterInput characterInput;
        [SerializeField] private CharacterStats characterStats;
        [SerializeField] private CharacterDetection characterDetection;

        // Called every frame
        private void Update()
        {
            // Handling character input
            HandleInput();
        }

        // Method to handle character input
        private void HandleInput()
        {
            // If character is against a wall, adjust movement vector accordingly
            if (characterDetection.IsWall())
            {
                characterStats.MoveVector = new Vector2(
                    characterInput.MoveRight.action.ReadValue<float>() - characterInput.MoveLeft.action.ReadValue<float>(),
                    characterInput.MoveUp.action.ReadValue<float>() - characterInput.MoveDown.action.ReadValue<float>()
                ).normalized;
            }
            else
            {
                // If not against a wall, only horizontal movement is allowed
                characterStats.MoveVector = new Vector2(
                    characterInput.MoveRight.action.ReadValue<float>() - characterInput.MoveLeft.action.ReadValue<float>(),
                    0
                ).normalized;
            }
        }

        // Method to move character vertically
        public void MoveVertically(float verticalSpeed)
        {
            characterStats.VelocityY = Mathf.MoveTowards(characterComponents.Rb.velocity.y, verticalSpeed,
                characterStats.SmoothTime);
            characterComponents.Rb.velocity = new Vector2(characterComponents.Rb.velocity.x, characterStats.VelocityY);
        }
        
        // Method to move character horizontally
        public void MoveHorizontally(float horizontalSpeed)
        {
            switch (characterStats.IsDashing)
            {
                case false:
                    // Move character horizontally with smoothing
                    characterStats.VelocityX = Mathf.MoveTowards(characterComponents.Rb.velocity.x,
                        horizontalSpeed, characterStats.SmoothTime);
                    characterComponents.Rb.velocity =
                        new Vector2(characterStats.VelocityX, characterComponents.Rb.velocity.y);
                    break;
                case true:
                    // If dashing, move character with dash speed and direction
                    var dashSpeed = characterStats.DashSpeed;
                    var dashDirection = characterComponents.Sr.flipX ? -1f : 1f;

                    var velocity = characterComponents.Rb.velocity;
                    velocity = new Vector2(
                        dashDirection * dashSpeed,
                        velocity.y);
                    characterComponents.Rb.velocity = velocity;
                    
                    // Decrement dash distance and stop dashing if distance is zero or less
                    var dashDistance = 2f;
                    dashDistance -= Mathf.Abs(velocity.x) * Time.deltaTime;
                    if (!(dashDistance <= 0)) return;
                    characterComponents.Rb.velocity = Vector2.zero;
                    characterStats.IsDashing = false;
                    break;
            }
        }
    }
}
