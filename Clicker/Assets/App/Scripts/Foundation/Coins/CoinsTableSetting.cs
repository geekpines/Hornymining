using System;
using System.Collections.Generic;
using System.Linq;
using App.Scripts.Foundation.Coins;
using App.Scripts.Utilities.MonoBehaviours;
using UnityEngine;

namespace App.Scripts.Foundation
{
    [CreateAssetMenu(fileName = "CoinsTable", menuName = "Game/Settings/CoinsTable", order = 0)]
    public class CoinsTableSetting : ElementsTable<Coin>
    {
        private static CoinsTableSetting _instance;

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
        /// Получить список всех коинов
        /// </summary>
        /// <returns></returns>
        public List<Coin> GetAllCoins()
        {
            return Elements;
        }

        /// <summary>
        /// Получить ссылку на экземпляр ScriptableObject
        /// </summary>
        public static CoinsTableSetting Instance
        {
            get
            {
                if (_instance == null)
                {
                    var path = $"Settings/{typeof(CoinsTableSetting).Name}";
                    _instance = Resources.Load(path) as CoinsTableSetting;
                }

                return _instance;
            }
        }
    }
}