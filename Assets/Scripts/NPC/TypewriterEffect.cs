using System.Collections;
using TMPro;
using UnityEngine;

namespace NPC
{
    public class TypewriterEffect : MonoBehaviour
    {
        [SerializeField] private float typingSpeed = 0.05f;
        private TextMeshProUGUI textComponent;
        private Coroutine typingCoroutine;

        private void Awake()
        {
            textComponent = GetComponent<TextMeshProUGUI>();
        }

        public void StartTyping(string text)
        {
            // Stop any ongoing typing
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
            }
            
            // Start new typing effect
            typingCoroutine = StartCoroutine(TypeText(text));
        }

        public void StopTyping()
        {
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
                typingCoroutine = null;
            }
        }

        private IEnumerator TypeText(string text)
        {
            textComponent.text = "";
            
            foreach (char letter in text.ToCharArray())
            {
                textComponent.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }
            
            typingCoroutine = null;
        }

        public bool IsTyping()
        {
            return typingCoroutine != null;
        }
    }
}