using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace App.Scripts.Foundation.Roulette.Core
{
    /// <summary>
    /// Конфигурация генерации предметов. В данном скрипте находятся
    /// настройки для генерации, такие как: Экземпляр предмета и его 
    /// вес, который в свою очередь влияет на шанс выпадения
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// example:
    ///[CreateAssetMenu(fileName = "RouletteConfiguration", menuName = "Settings/Roulette/Configurations", order = 0)]
    public abstract class RouletteConfiguration<T> : ScriptableObject
        where T : ScriptableObject
    {
        /// <summary>
        /// Список возможных предметов для генерации
        /// </summary>
        //[InfoBox("Список возможных предметов")]
        public List<RouletteItemInfo<T>> RouletteItems = new List<RouletteItemInfo<T>>();
  
        /// <summary>
        /// Класс обертка для генерации. Можно задать  
        /// настройки для генерации предмета
        /// </summary>
        [Serializable]
        public class RouletteItemInfo<T>
            where T : ScriptableObject
        {
            public T Item;
            [Range(0, 1000)] public int Weight;
            [ReadOnly]
            public float ChancePercent;
            
            public void SetChance(float chance)
            {
                ChancePercent = chance;
            }
        }
        
        private void OnValidate()
        {
            //Отображаем шанс
            int sumWeight = 0;
            foreach (var lootItemData in RouletteItems)
            {
                sumWeight += lootItemData.Weight;
            }
            foreach (var lootItemData in RouletteItems)
            {
                if (sumWeight == 0)
                {
                    Debug.LogError( "Суммарный шанс не может быть 0!");
                    return;
                }
                lootItemData.SetChance((lootItemData.Weight * 100) / sumWeight);
            }
        }
        
    }
}