﻿using System;
using System.Collections.Generic;
using System.Linq;
using Assets.App.Scripts.Common;
using UnityEngine;

namespace App.Scripts.Foundation
{
    [Serializable]
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