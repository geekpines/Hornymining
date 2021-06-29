using System;
using UnityEngine;

namespace App.Scripts.Gameplay.Coins
{
    /// <summary>
    /// Данные по ресурсам
    /// </summary>
    [Serializable]
    public class CoinData
    {
        /// <summary>
        /// ID ресурса
        /// </summary>
        public int ID { get; private set; }
        
        /// <summary>
        /// Количество ресурса
        /// </summary>
        public float Count { get; private set; }

        public CoinData(int id)
        {
            ID = id;
            Count = 0;
        }

        /// <summary>
        /// Добавить значение к текущему количеству
        /// </summary>
        /// <param name="value"></param>
        public void Add(float value)
        {
            Count += value;
            Count = Mathf.Min(Count, 0);
        }

        /// <summary>
        /// Установить текущее количество
        /// </summary>
        /// <param name="value"></param>
        public void Set(float value)
        {
            Count = value;
        }
    }
}