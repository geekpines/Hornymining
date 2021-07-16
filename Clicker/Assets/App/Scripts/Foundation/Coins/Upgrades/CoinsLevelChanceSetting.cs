using System.Collections.Generic;
using UnityEngine;

namespace App.Scripts.Foundation.Upgrades
{
    [CreateAssetMenu(fileName = "CoinsLevelChanceSetting", menuName = "Game/Settings/CoinsLevelChanceSetting", order = 0)]
    public class CoinsLevelChanceSetting : ScriptableObject
    {
        private static CoinsLevelChanceSetting _instance;

        [SerializeField] private List<CoinsLevelChance> _levels = new List<CoinsLevelChance>();
        
        /// <summary>
        /// Получить ссылку на экземпляр ScriptableObject
        /// </summary>
        public static CoinsLevelChanceSetting Instance
        {
            get
            {
                if (_instance == null)
                {
                    var path = $"Settings/{typeof(CoinsLevelChanceSetting).Name}";
                    _instance = Resources.Load(path) as CoinsLevelChanceSetting;
                }

                return _instance;
            }
        }

        /// <summary>
        /// Получить таблицу вероятности валюты в соответствии 
        /// с уровнем
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public static CoinsLevelChance GetChanceTable(int level)
        {
            return _instance._levels.Count <= level ? _instance._levels[level] : null;
        }

        /// <summary>
        /// Получить количество уровней
        /// </summary>
        /// <returns></returns>
        public static int CountLevels()
        {
            return _instance._levels.Count;
        }
    }
}