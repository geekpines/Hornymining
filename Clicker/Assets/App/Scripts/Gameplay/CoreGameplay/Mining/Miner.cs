using System;
using App.Scripts.Common;
using UnityEngine;
using UnityEngine.Localization;

namespace App.Scripts.Gameplay.CoreGameplay.Mining
{
    [Serializable]
    public class Miner
    {
        /// <summary>
        /// Локализованное имя
        /// </summary>
        public LocalizedString Name => Configuration.Name;
        
        /// <summary>
        /// Локализованное описание
        /// </summary>
        public LocalizedString Description => Configuration.Description;
        
        /// <summary>
        /// Иконка/Аватар
        /// </summary>
        public Sprite Icon => Configuration.Visual.Icon;
        
        /// <summary>
        /// Конфигурационный файл (общий для всех героев одного типа)
        /// </summary>
        [field: SerializeField]
        public MinerConfiguration Configuration { get; private set; }

        /// <summary>
        /// Уникальный ID героя
        /// </summary>
        public int ID { get; private set; }

        /// <summary>
        /// Уровень прокачки девочки
        /// </summary>
        [field: SerializeField]
        public int Level { get; private set; }

        /// <summary>
        /// Уровень редкости девочки
        /// </summary>
        [field: SerializeField]
        public int Grade { get; private set; }

        public Miner(MinerConfiguration configuration, int grade)
        {
            Configuration = configuration;
            Grade = grade;
            Level = 0;
            ID = UniqueID.Generate();
        }
    }
}