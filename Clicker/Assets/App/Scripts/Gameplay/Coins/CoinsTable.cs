using System;
using App.Scripts.Utilities.MonoBehaviours;
using UnityEngine;

namespace App.Scripts.Gameplay.Coins
{
    [CreateAssetMenu(fileName = "CoinsTable", menuName = "Game/Settings/CoinsTable", order = 0)]
    public class CoinsTable : ElementsTable<Coin>
    {
        private static CoinsTable _instance;

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