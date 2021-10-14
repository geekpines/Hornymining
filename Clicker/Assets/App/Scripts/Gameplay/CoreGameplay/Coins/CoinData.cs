using System;
using UnityEngine;

namespace App.Scripts.Gameplay.CoreGameplay.Coins
{
    /// <summary>
    /// Данные по ресурсам
    /// </summary>
    [Serializable]
    public class CoinData
    {
        /// <summary>
        /// Значение изменено
        /// </summary>
        public Action<CoinType, float> OnAddValue;
        public Action<CoinType, float> OnChangeCount;
        
        /// <summary>
        /// ID ресурса
        /// </summary>
        [field: SerializeField] public CoinType ID { get; private set; }
        
        /// <summary>
        /// Количество ресурса
        /// </summary>
        [field: SerializeField] public float Value { get; private set; }

        public CoinData(CoinType id)
        {
            ID = id;
            Value = 0;
        }

        /// <summary>
        /// Добавить значение к текущему количеству
        /// </summary>
        /// <param name="addValue"></param>
        public void Add(float addValue)
        {
            Set(Value + addValue);
            OnAddValue?.Invoke(ID, Value);
        }

        /// <summary>
        /// Вычесть из текущего количества
        /// </summary>
        /// <param name="subValue"></param>
        public void Decrease(float subValue)
        {
            Set(Value - subValue);
        }

        /// <summary>
        /// Установить текущее количество
        /// </summary>
        /// <param name="value"></param>
        public void Set(float value)
        {
            Value = Mathf.Max(value, 0);
            OnChangeCount?.Invoke(ID, Value);
        }
    }
}