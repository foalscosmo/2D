using System;
using Player;
using Player.PlayerMovement;
using TMPro;
using UnityEngine;

namespace Coins
{
    public class PlayerCoinManager : MonoBehaviour
    {
        [SerializeField] private PlayerCoin playerCoin;
        [SerializeField] private CharacterDetection characterDetection;
        [SerializeField] private TextMeshProUGUI coinText;
        [SerializeField] private PlayerSounds playerSounds;

        private void Awake()
        {
            playerCoin.Amount = 0;
        }

        private void OnEnable()
        {
            characterDetection.OnCoinCollected += UpdateCoin;
        }

        private void OnDisable()
        {
            characterDetection.OnCoinCollected -= UpdateCoin;
        }

        private void UpdateCoin()
        {
            playerCoin.AddAmount();
            coinText.text = playerCoin.Amount.ToString();
            playerSounds.CoinSound();
        }
    }
}