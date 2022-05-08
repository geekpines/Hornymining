using App.Scripts.Gameplay.CoreGameplay.Coins;
using App.Scripts.Gameplay.CoreGameplay.Coins.Static;
using App.Scripts.Gameplay.CoreGameplay.Mining;
using Assets.App.Scripts.Common;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Localization.Settings;

namespace App.Scripts.Gameplay.CoreGameplay.Player
{
    [Serializable, InfoBox("Информация о игроке. Для одиночной игры должен использоваться как " +
                           "синглтон. Именно через этот класс происходит запоминание всего прогресса.")]
    public class PlayerProfile : AbstractService<PlayerProfile>
    {
        //todo: вынести в конфиг
        private const int MaxActiveMinersCount = 5;
        public event Action<Miner> OnAllMinersCountChanged;
        public event Action<Miner> OnActiveMinersCountChanged;
        public event Action<bool> OnMoneyNotEnough;

        [field: SerializeField]
        public List<CoinData> Coins { get; private set; } = new List<CoinData>();
        public float percentUpgrade = 1;

        [SerializeField] private List<Miner> _allMiners = new List<Miner>();
        private List<Miner> _activeMiners = new List<Miner>();

        private Dictionary<CoinType, CoinData> CoinTypeToData = new Dictionary<CoinType, CoinData>();

        private void Awake()
        {
            
            //todo: добавить сохранение
            Coins.Clear();
            var temp = new List<CoinData>();
            foreach (var coin in CoinsInformation.GetElements())
            {
                temp.Add(new CoinData(coin.ID, coin.coinTradeValue));
            }
            Coins = temp.OrderBy(data => data.ID).ToList();

            foreach (var coinData in Coins)
            {
                if (!CoinTypeToData.ContainsKey(coinData.ID))
                {
                    CoinTypeToData.Add(coinData.ID, coinData);
                }
            }
        }


        /// <summary>
        /// Добавить майнера игроку
        /// </summary>
        /// <param name="miner"></param>
        public void AddMiner(Miner miner)
        {
            _allMiners.Add(miner);
            OnAllMinersCountChanged?.Invoke(miner);
        }

        /// <summary>
        /// Добавить активного майнера игроку
        /// </summary>
        /// <param name="miner"></param>
        public void AddActiveMiner(Miner miner)
        {
            if (_activeMiners.Count >= MaxActiveMinersCount)
                return;

            if (_allMiners.Contains(miner) &&
                !_activeMiners.Contains(miner))
            {
                _activeMiners.Add(miner);
                OnActiveMinersCountChanged?.Invoke(miner);
            }
        }

        /// <summary>
        /// Убрать активного майнера у игрока
        /// </summary>
        /// <param name="miner"></param>
        public void RemoveActiveMiner(Miner miner)
        {
            if (_allMiners.Contains(miner) &&
                _activeMiners.Contains(miner))
            {
                _activeMiners.Remove(miner);
                OnActiveMinersCountChanged?.Invoke(miner);
            }
        }

        /// <summary>
        /// Удалить майнера у игрока
        /// </summary>
        /// <param name="miner"></param>
        public void RemoveMiner(Miner miner)
        {
            if (_allMiners.Contains(miner))
            {
                _allMiners.Remove(miner);
                OnAllMinersCountChanged?.Invoke(miner);
            }

            if (_activeMiners.Contains(miner))
            {
                _activeMiners.Remove(miner);
                OnAllMinersCountChanged?.Invoke(miner);
            }
        }

        /// <summary>
        /// Получить список всех майнеров игрока
        /// </summary>
        /// <returns></returns>
        public List<Miner> GetAllMiners()
        {
            //todo: Заменить на IEnumerator
            return _allMiners;
        }

        /// <summary>
        /// Получить список всех активных майнеров игрока
        /// </summary>
        /// <returns></returns>
        public List<Miner> GetActiveMiners()
        {
            return _activeMiners;
        }

        /// <summary>
        /// Проверить, есть ли майнер у игрока
        /// </summary>
        /// <param name="miner"></param>
        /// <returns></returns>
        public bool ContainsMiner(Miner miner)
        {
            return _allMiners.Contains(miner);
        }

        /// <summary>
        /// Проверить, есть ли активный майнер у игрока
        /// </summary>
        /// <param name="miner"></param>
        /// <returns></returns>
        public bool ContainsActiveMiner(Miner miner)
        {
            return _activeMiners.Contains(miner);
        }

        /// <summary>
        /// Добавить валюту опредленного типа игроку
        /// </summary>
        /// <param name="resourceId">Тип валюты</param>
        /// <param name="addScore">Количество добавленной валюты</param>
        public void AddScore(CoinType resourceId, float addScore)
        {
            if (CoinTypeToData.ContainsKey(resourceId))
            {                
                    CoinTypeToData[resourceId].Add(addScore);
                //Debug.Log(percentUpgrade * addScore + " :percentUpgrade;" + " CoinType: " + resourceId);
            }
        }

        /// <summary>
        /// Попытаться вычесть определенное количество валюты у игрока
        /// </summary>
        /// <param name="resourceId">Тип валюты</param>
        /// <param name="value">Количество</param>
        /// <returns></returns>
        public bool TryRemoveScore(CoinType resourceId, float value)
        {
            OnMoneyNotEnough?.Invoke(!(CoinTypeToData[resourceId].Value - value > 0));
            return CoinTypeToData[resourceId].Value - value > 0;
        }

        /// <summary>
        /// Вычесть валюту у игрока
        /// </summary>
        /// <param name="resourceId">Тип валюты</param>
        /// <param name="value">Количество</param>
        public void RemoveScore(CoinType resourceId, float value)
        {
            CoinTypeToData[resourceId].Decrease(value);
        }

        public void RemoveActiveMiner(int ID)
        {
            foreach (var miner in GetActiveMiners())
            {
                if(miner.ID == ID)
                {
                    _activeMiners.Remove(miner);
                }
            }
        }

        public void ResetPlayer(PlayerProfile playerProfile)
        {
            _allMiners.Clear();
            _activeMiners.Clear();
            //percentUpgrade = 1;
            int i = 0;
            foreach (var coin in Coins)
            {
                playerProfile.AddScore(coin.ID, -playerProfile.Coins[i].Value);
                i++;
            }
        }
        public void ResetPlayer()
        {
            //percentUpgrade = 1;
            _activeMiners.Clear();
            _allMiners.Clear();
            for (int i = 0; i < Coins.Count; i++)
            {
                AddScore(Coins[i].ID, -Coins[i].Value);
            }
        }

        public Miner FindMiner(int id)
        {

            foreach(Miner miner in GetAllMiners())
            {
                if (miner.ID == id)
                {
                    return miner;
                }
            }
            return null;
        }
      
    }
}