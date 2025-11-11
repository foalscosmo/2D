using System.Collections;
using TMPro;
using UnityEngine;

namespace NPC
{
    public class TypewriterEffect : MonoBehaviour
    {
        [SerializeField] private float typingSpeed;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip typingSound;
        [SerializeField] private float soundPitchMin;
        [SerializeField] private float soundPitchMax;
        [SerializeField] private bool playSoundForSpaces = false;
        
        [SerializeField] private TextMeshProUGUI textComponent;
        private Coroutine typingCoroutine;
        
        public void StartTyping(string text)
        {
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
            }
            
            typingCoroutine = StartCoroutine(TypeText(text));
        }

        public float GetTypingDuration(string text)
        {
            return text.Length * typingSpeed;
        }

        public void StopTyping()
        {
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
                typingCoroutine = null;
            }
            
            if (audioSource != null)
            {
                audioSource.Stop();
            }
        }

        private IEnumerator TypeText(string text)
        {
            textComponent.text = "";
            
            foreach (char letter in text.ToCharArray())
            {
                textComponent.text += letter;
                
                if (ShouldPlaySound(letter) && audioSource != null && typingSound != null)
                {
                    if (audioSource != null)
                    {
                        audioSource.pitch = Random.Range(soundPitchMin, soundPitchMax);
                        audioSource.PlayOneShot(typingSound);
                    }
                }
                
                yield return new WaitForSeconds(typingSpeed);
            }
            
            typingCoroutine = null;
        }

        private bool ShouldPlaySound(char character)
        {
            if (playSoundForSpaces)
            {
                return !char.IsControl(character); 
            }
            else
            {
                return !char.IsWhiteSpace(character);
            }
        }
    }
}