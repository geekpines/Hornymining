using System;
using System.Collections.Generic;
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
        where T : MonoBehaviour
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
            where T : MonoBehaviour
        {
            public T Item;
            [Range(1, 1000)] public int Weight;
            //[ReadOnly]
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
                lootItemData.SetChance((lootItemData.Weight * 100) / sumWeight);
            }
        }
        
    }
}