using System;
using System.Collections.Generic;
using UnityEngine;

namespace App.Scripts.Gameplay.CoreGameplay.Coins.Upgrades
{
    /// <summary>
    /// Класс содержит вероятность добычи той или иной валюты
    /// при клике на майнера
    /// </summary>
    [CreateAssetMenu(fileName = "CoinsChance", menuName = "Game/CoinChance/Level", order = 0)]
    public class CoinsLevelChance : ScriptableObject
    {
        public List<CoinChance> Coins = new List<CoinChance>();
        
        //todo: Добавить проверку общего шанса - сумма не должна превышать 100
        
        [Serializable]
        public class CoinChance
        {
            public Coin Coin;
            [Range(0, 100)] public float Chance;

            public CoinChance(Coin coin, float chance)
            {
                Coin = coin;
                Chance = chance;
            }
        }

        private void OnValidate()
        {
            if (Coins.Count != CoinsTableSetting.Instance.Elements.Count)
            {
                Coins.Clear();
                foreach (var element in CoinsTableSetting.Instance.GetAllCoins())
                {
                    Coins.Add(new CoinChance(element, 0));
                }
            }

            float sum = 0;
            foreach (var coin in Coins)
            {
                sum += coin.Chance;
            }

            if (Math.Abs(sum - 100) < 0.02f)
            {
                //Debug.Log($"Сумма вероятностей {name}:<color=#00FF00>{sum}</color> Корректна!");
            }
            else
            {
                Debug.Log($"Сумма вероятностей {name}:<color=#FF0000>{sum}</color> некорректна!");
            }
        }
    }
}