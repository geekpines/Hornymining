using System;
using System.Collections.Generic;
using System.Linq;
using App.Scripts.Gameplay.CoreGameplay.Coins;
using App.Scripts.Gameplay.CoreGameplay.Coins.Static;
using App.Scripts.Gameplay.CoreGameplay.Mining;
using Assets.App.Scripts.Common;
using Sirenix.OdinInspector;
using UnityEngine;

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
        [field:SerializeField] 
        public List<CoinData> Coins { get; private set; } = new List<CoinData>();
        
        [SerializeField] private List<Miner> _allMiners = new List<Miner>();
        private List<Miner> _activeMiners = new List<Miner>();

        [field: SerializeField]
        public int CoinLevelChance { get; private set; }

        private void Awake()
        {
            //todo: добавить сохранение
            Coins.Clear();
            var temp = new List<CoinData>();
            foreach (var coin in CoinsInformation.GetElements())
            {
                temp.Add(new CoinData(coin.ID));
            }
            Coins = temp.OrderBy(data => data.ID).ToList();
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

        public List<Miner> GetAllMiners()
        {
            return _allMiners;
        }

        public List<Miner> GetActiveMiners()
        {
            return _activeMiners;
        }

        public bool ContainsMiner(Miner miner)
        {
            return _allMiners.Contains(miner);
        }

        public bool ContainsActiveMiner(Miner miner)
        {
            return _activeMiners.Contains(miner);
        }

    }
}