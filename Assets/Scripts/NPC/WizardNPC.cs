using System;
using System.Collections;
using System.Collections.Generic;
using Coins;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace NPC
{
    public class WizardNPC : MonoBehaviour
    {
        [SerializeField] private LayerMask playerLayerMask;
        [SerializeField] private BoxCollider2D detectionArea;
        [SerializeField] private PlayerCoin coin;
        [SerializeField] private GameObject textBox;
        [SerializeField] private TextMeshProUGUI textObj;
        [SerializeField] private int missionCoinAmount;
        [SerializeField] private int detectionAmount;
        [SerializeField] private float messageDelay = 2f;
        
        private TypewriterEffect typewriterEffect;
        private bool hasGivenFirstWarning = false;
        private Coroutine messageSequenceCoroutine;
        [SerializeField] private PlayerInput characterInput;

        private void Start()
        {
            typewriterEffect = textObj.GetComponent<TypewriterEffect>();
            if (typewriterEffect == null)
            {
                typewriterEffect = textObj.gameObject.AddComponent<TypewriterEffect>();
            }
            
            characterInput.enabled = false;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if ((playerLayerMask & (1 << other.gameObject.layer)) == 0) return;
            {
                detectionAmount++;
                CheckPlayerCoin();
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if ((playerLayerMask & (1 << other.gameObject.layer)) == 0) return;
            {
                DisableTextBox();
            }
        }

        private void CheckPlayerCoin()
        {
            if (coin.Amount >= missionCoinAmount && detectionAmount > 1)
            {
                ShowMessage("HaHaHa... Just Kidding there is no new chapter, have fun");
            }
            else if (coin.Amount < missionCoinAmount && detectionAmount > 1)
            {
                ShowMessage("It's not enough bring me all gems");
            }
            else if (coin.Amount < missionCoinAmount && detectionAmount == 1)
            {
                if (!hasGivenFirstWarning)
                {
                    // Show both messages in sequence
                    messageSequenceCoroutine = StartCoroutine(ShowFirstMeetingMessages());
                    hasGivenFirstWarning = true;
                }
                else
                {
                    ShowMessage("But I warn you, you have a difficult road ahead of you, so be careful.");
                }
            }
        }

        private IEnumerator ShowFirstMeetingMessages()
        {
            textBox.SetActive(true);
            
            var firstMessage = $"Hello there, I am your master, bring me {missionCoinAmount} gem to unlock new chapter";
            typewriterEffect.StartTyping(firstMessage);
            
            yield return new WaitForSeconds(typewriterEffect.GetTypingDuration(firstMessage) + messageDelay);
            
            var secondMessage = "But I warn you, you have a difficult road ahead of you, so be careful.";
            typewriterEffect.StartTyping(secondMessage);
            
            yield return new WaitForSeconds(typewriterEffect.GetTypingDuration(secondMessage) + messageDelay);
            
            var thirdMessage = "It's time to go and do it..";
            typewriterEffect.StartTyping(thirdMessage);
            
            yield return new WaitForSeconds(typewriterEffect.GetTypingDuration(thirdMessage) + messageDelay);
            characterInput.enabled = true;
        }

        private void ShowMessage(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                textBox.SetActive(true);
                typewriterEffect.StartTyping(message);
            }
        }

        private void DisableTextBox()
        {
            if (messageSequenceCoroutine != null)
            {
                StopCoroutine(messageSequenceCoroutine);
                messageSequenceCoroutine = null;
            }
            typewriterEffect.StopTyping();
            if (textBox != null) textBox.SetActive(false);
        }
    }
}