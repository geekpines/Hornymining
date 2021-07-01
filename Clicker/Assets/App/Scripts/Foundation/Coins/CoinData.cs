using System;
using UnityEngine;

namespace App.Scripts.Foundation
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
        public Action<int, float> OnChanged;
        public Action<int, float> OnChangeCount;
        
        /// <summary>
        /// ID ресурса
        /// </summary>
        public int ID { get; private set; }
        
        /// <summary>
        /// Количество ресурса
        /// </summary>
        public float Value { get; private set; }

        public CoinData(int id)
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
            OnChangeCount?.Invoke(ID, addValue);
            Value += addValue;
            Value = Mathf.Max(Value, 0);
            OnChanged?.Invoke(ID, Value);
        }

        /// <summary>
        /// Установить текущее количество
        /// </summary>
        /// <param name="value"></param>
        public void Set(float value)
        {
            Value = value;
        }
    }
}