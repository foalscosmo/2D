using UnityEngine;

namespace Player
{
    public class PlayerSounds : MonoBehaviour
    {
        [SerializeField] private AudioClip movementSound;
        [SerializeField] private AudioClip deathSound;
        [SerializeField] private AudioClip checkPointSound;
        [SerializeField] private AudioClip coinSound;
        [SerializeField] private AudioClip jumpSound;
        [SerializeField] private AudioClip dashSound;
        [SerializeField] private AudioClip landSound;
        [SerializeField] private AudioSource audioSource;
        public void MovementSound()
        {
            audioSource.PlayOneShot(movementSound);
        }

        public void DeathSound()
        {
            audioSource.PlayOneShot(deathSound);
        }

        public void CheckPointSound()
        {
            audioSource.PlayOneShot(checkPointSound);
        }

        public void CoinSound()
        {
            audioSource.PlayOneShot(coinSound);
        }

        public void JumpSound()
        {
            audioSource.PlayOneShot(jumpSound);
        }

        public void DashSound()
        {
            audioSource.PlayOneShot(dashSound);
        }

        public void LandSound()
        {
            audioSource.PlayOneShot(landSound);
        }
        
    }
}