using UnityEngine;

namespace Character.CharacterScriptable
{
    [CreateAssetMenu(menuName = "Create BotDetection", fileName = "BotDetectionStats", order = 0)]
    public class BotDetectionStats : ScriptableObject
    {
        [SerializeField] private LayerMask grounded;
        [SerializeField] private bool isGrounded;

        public bool IsGrounded
        {
            get => isGrounded;
            set => isGrounded = value;
        }
        public LayerMask Grounded
        {
            get => grounded;
            set => grounded = value;
        }
    }
}
