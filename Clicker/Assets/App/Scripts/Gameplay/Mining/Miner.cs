using System;
using UnityEngine;
using UnityEngine.Localization;

namespace App.Scripts.Foundation
{
    [Serializable]
    public class Miner
    {
        public LocalizedString Name => Configuration.Name;
        public LocalizedString Description => Configuration.Description;
        public Sprite Icon => Configuration.Icon;
        
        [field: SerializeField]
        public MinerConfiguration Configuration { get; private set; }

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
        }
    }
}