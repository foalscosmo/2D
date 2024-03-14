using UnityEngine;

// Namespace for managing player movement
namespace Player.PlayerMovement
{
    // ScriptableObject class for defining detection statistics
    [CreateAssetMenu(menuName = "Create CharacterDetection", fileName = "CharacterDetection")]
    public class DetectionStats : ScriptableObject
    {
        // Serialized fields for layer masks and detection parameters
        [Header("Layer Mask")] 
        [SerializeField] private LayerMask grounded;           // Layer mask for detecting ground
        [SerializeField] private LayerMask wallMask;           // Layer mask for detecting walls

        [Header("Float")] 
        [SerializeField] private float wallCollisionRadius;    // Radius for detecting wall collisions
        [SerializeField] private float ledgeLength;            // Length of ledge

        [Header("Bool")] 
        [SerializeField] private bool isLedgeDetected;         // Flag indicating if a ledge is detected

        // Properties to access serialized fields
        public LayerMask Grounded => grounded;                 // Property for accessing ground layer mask
        public LayerMask WallMask => wallMask;                 // Property for accessing wall layer mask
        public float WallCollisionRadius => wallCollisionRadius; // Property for accessing wall collision radius
        public bool IsLedgeDetected                           // Property for accessing and setting ledge detection flag
        {
            get => isLedgeDetected;
            set => isLedgeDetected = value;
        }
        public float LedgeLength => ledgeLength;               // Property for accessing ledge length
    }
}
