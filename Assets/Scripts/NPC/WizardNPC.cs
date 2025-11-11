using System;
using System.Collections.Generic;
using Coins;
using TMPro;
using UnityEngine;

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
        
        private TypewriterEffect typewriterEffect;

        private void Start()
        {
            typewriterEffect = textObj.GetComponent<TypewriterEffect>();
            if (typewriterEffect == null)
            {
                typewriterEffect = textObj.gameObject.AddComponent<TypewriterEffect>();
            }
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
            string message = "";
            
            if (coin.Amount >= missionCoinAmount && detectionAmount > 1)
            {
                message = "HaHaHa... Just Kidding there is no new chapter";
            }
            else if (coin.Amount < missionCoinAmount && detectionAmount > 1)
            {
                message = "It's not enough bring me more";
            }
            else if (coin.Amount < missionCoinAmount && detectionAmount == 1)
            {
                message = $"Hello there, I am your master, bring me {missionCoinAmount} gem to unlock new chapter";
            }

            if (!string.IsNullOrEmpty(message))
            {
                textBox.SetActive(true);
                typewriterEffect.StartTyping(message);
            }
        }

        private void DisableTextBox()
        {
            typewriterEffect.StopTyping();
            textBox.SetActive(false);
        }
    }
}