using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace App.Scripts.Foundation.Roulette.Core
{
    /// <summary>
    /// Класс генерации предметов на основе конфигурации.
    /// </summary>
    /// <typeparam name="T">Тип предмета</typeparam>
    public abstract class Roulette<T> : MonoBehaviour
        where T : MonoBehaviour
    {
        /// <summary>
        /// Флаг удаления предмета из списка после 
        /// успешной генерации
        /// </summary>
        public bool IsRemoveItemAfterRoll;
        
        /// <summary>
        /// Текущая концигурация для генерации предметов
        /// </summary>
        [field:SerializeField]
        public RouletteConfiguration<T> Configuration { get; private set; }
        private List<RouletteConfiguration<T>.RouletteItemInfo<T>> _chachedItemDatas 
            = new List<RouletteConfiguration<T>.RouletteItemInfo<T>>();
        private int _sumWeight;
        
        /// <summary>
        /// Сгенерировать предмет на основе конфигурации
        /// (в ней указан список предметов и их веса)
        /// </summary>
        /// <returns></returns>
        public T RollItem()
        {
            if (_chachedItemDatas.Count < 1)
            {
                Debug.Log("Невозможно произвести ролл! Количество предметов для ролла равно 0!");
                return null;
            }

            var randomWeight = Random.Range(1, _sumWeight);
            int index = FindItemConfigIndex(randomWeight);

            var result = _chachedItemDatas[index].Item;
            if (IsRemoveItemAfterRoll)
            {
                _sumWeight -= _chachedItemDatas[index].Weight;
                _chachedItemDatas.RemoveAt(index);
            }
            return result;
        }

        /// <summary>
        /// Установить новую конфигурацию для генерации
        /// </summary>
        /// <param name="config">Конфигурация</param>
        public void SetConfiguration(RouletteConfiguration<T> config)
        {
            Configuration = config;
            InitializationChache();
        }

        private void Start()
        {
            InitializationChache();
        }

        private void InitializationChache()
        {
            _chachedItemDatas.Clear();
            _sumWeight = 0;
            for (int i = 0; i < Configuration.RouletteItems.Count; i++)
            {
                _chachedItemDatas.Add(Configuration.RouletteItems[i]);
                _sumWeight += Configuration.RouletteItems[i].Weight;
            }
        }
        
        private int FindItemConfigIndex(int randomValue)
        {
            int range = 0;
            int index = 0;
            foreach (var lootData in _chachedItemDatas)
            {
                if (lootData.Weight > 0)
                {
                    if (randomValue > range &&
                        randomValue <= range + lootData.Weight)
                    {
                        return index;
                    }
                    else
                    {
                        range += lootData.Weight;
                    }
                }
                index++;
            }
            return -1;
        }
    }
}