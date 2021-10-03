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
        public List<CoinData> Coins = new List<CoinData>();
        public List<Miner> Miners = new List<Miner>();
        
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

    }
}