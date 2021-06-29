﻿using System;
using System.Linq;
using App.Scripts.Utilities.MonoBehaviours;
using UnityEngine;

namespace App.Scripts.Gameplay.Coins
{
    [CreateAssetMenu(fileName = "CoinsTable", menuName = "Game/Settings/CoinsTable", order = 0)]
    public class CoinsTable : ElementsTable<Coin>
    {
        private static CoinsTable _instance;

        /// <summary>
        /// Получить конфиг коина по его ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Coin FindCoinToID(int id)
        {
            return Elements.FirstOrDefault(coin => coin.ID == id);
        }

        /// <summary>
        /// Получить ссылку на экземпляр ScriptableObject
        /// </summary>
        [Obsolete ("Для получения доступа к данным используйте классы обертки")]
        public static CoinsTable Instance
        {
            get
            {
                if (_instance == null)
                {
                    var path = $"Settings/{typeof(CoinsTable).Name}";
                    _instance = Resources.Load(path) as CoinsTable;
                }

                return _instance;
            }
        }
    }
}