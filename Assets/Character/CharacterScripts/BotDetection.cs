using System;
using Character.CharacterScriptable;
using UnityEngine;

namespace Character.CharacterScripts
{
    public class BotDetection : MonoBehaviour
    {
        [SerializeField]
        private Transform groundTransform;

        [SerializeField] private BotDetectionStats botDetectionStats;
        [SerializeField] private BotComponents botComponents; // Reference to character components

        private void FixedUpdate()
        {
            IsGrounded();
        }

        private void IsGrounded()
        {
            botDetectionStats.IsGrounded = botComponents.Rb.velocity.y <= 0.01f && Physics.CheckSphere(
                groundTransform.position, 0.2f, botDetectionStats.Grounded);
        }

    }
}
