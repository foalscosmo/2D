using System;
using UnityEngine;

namespace Coins
{
    [CreateAssetMenu(menuName = "Create PlayerCoin", fileName = "PlayerCoin", order = 0)]
    public class PlayerCoin : ScriptableObject
    {
        [SerializeField] private int amount;
        public int Amount
        {
            get => amount;
            set => amount = value;
        }

        public void AddAmount()
        {
            amount ++;
        }
    }
}