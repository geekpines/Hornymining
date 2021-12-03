using App.Scripts.Gameplay.CoreGameplay.Coins;
using App.Scripts.Gameplay.CoreGameplay.Player;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace App.Scripts.Gameplay.Prototypes
{
    public class PrototypeAddCoins : MonoBehaviour
    {
        [SerializeField] private List<CoinInfo> AddCoins = new List<CoinInfo>();
        private PlayerProfile _playerProfile;

        [Serializable]
        private class CoinInfo
        {
            public CoinType Type;
            public float Value;
        }

        [Inject]
        private void Construct(PlayerProfile playerProfile)
        {
            _playerProfile = playerProfile;
        }

        private void Start()
        {
            foreach (var coin in AddCoins)
            {
                _playerProfile.AddScore(coin.Type, coin.Value);
            }
        }
    }
}