using UnityEngine;

namespace Player.PlayerMovement // Namespace for managing player movement
{
    // Class responsible for character detection, inheriting from MonoBehaviour
    public class CharacterDetection : MonoBehaviour
    {
        [Header("Transform")] // Header for organization in Unity Inspector
        [SerializeField] private Transform groundTransform; // Reference to ground detection transform
        [SerializeField] private Transform headTransformForLedge; // Reference to head transform for ledge detection
        [SerializeField] private Transform bodyTransformForWall; // Reference to body transform for wall detection

        [field: Header("Ray")] // Header for organization in Unity Inspector
        public RaycastHit2D HeadRayHit { get; private set; } // RaycastHit2D for head ray hit detection
        private RaycastHit2D checkRayHit; // Temporary RaycastHit2D for ray hit detection

        // Properties for move on ledge and input for ledge climb flags
        public bool HasMoveOnLedge { get; set; }
        public bool IsInputForLedgeClimb { get; set; }

        [Header("Class")] // Header for organization in Unity Inspector
        [SerializeField] private DetectionStats detectionStats; // Reference to detection statistics
        [SerializeField] private CharacterComponents characterComponents; // Reference to character components
        
        // Method called on every frame update
        private void Update()
        {
            IsGrounded(); // Checks if character is grounded
            IsWall(); // Checks if character is against a wall
            CheckForLedgeWithRay(); // Checks for ledge presence with ray
        }

        // Method to check if the character is grounded
        public bool IsGrounded()
        {
            // Checks if character's vertical velocity is close to zero and if there's an overlap circle at ground position
            return characterComponents.Rb.velocity.y <= 0.01f && Physics2D.OverlapCircle(
                groundTransform.position, 0.2f, detectionStats.Grounded);
        }
        
        // Method to check if the character is against a wall
        public bool IsWall()
        {
            // Checks for wall presence with raycast based on character flip status
            return CheckForWallWithRay() && characterComponents.Sr.flipX == false || 
                   CheckForWallWithRay() && characterComponents.Sr.flipX;
        }

        // Method to perform wall detection with raycast
        private bool CheckForWallWithRay()
        {
            // Defines ray direction based on character flip status and casts ray to detect wall
            Vector2 rayDirection = bodyTransformForWall.right * (characterComponents.Sr.flipX ? -1 : 1);
            return Physics2D.Raycast(bodyTransformForWall.position, rayDirection, 
                detectionStats.WallCollisionRadius, detectionStats.WallMask);
        }

        // Method to perform ledge detection with raycast
        private void CheckForLedgeWithRay()
        {
            // Defines ray direction based on character flip status and casts ray to detect ledge
            Vector2 rayDirection = headTransformForLedge.right * (characterComponents.Sr.flipX ? -1 : 1);
            HeadRayHit = Physics2D.Raycast(headTransformForLedge.position, rayDirection, 
                detectionStats.LedgeLength, detectionStats.Grounded);
        }
    }
}
