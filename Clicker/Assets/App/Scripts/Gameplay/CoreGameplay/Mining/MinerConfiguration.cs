using App.Scripts.Gameplay.CoreGameplay.Coins;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

namespace App.Scripts.Gameplay.CoreGameplay.Mining
{
    [CreateAssetMenu(fileName = "MinerConfiguration", menuName = "Game/MinerConfiguration", order = 0)]
    public class MinerConfiguration : ScriptableObject
    {
        public LocalizedString Name;
        public LocalizedString Description;

        [Tooltip("Ссылка на визуал девочки")]
        public MinerVisualContext Visual;

        [Tooltip("Прогрессия девочки с уровнями. Тут можно настроить то, как будет " +
                 "улучшаться добыча девочки по мере ее улучшения")]
        public List<LevelsSettings> Levels = new List<LevelsSettings>();

        [Serializable]
        public class LevelsSettings
        {
            [Tooltip("Период автоматической добычи валюты (с)"), BoxGroup("Настройки уровня")]
            public float PeriodAutoMining;

            [Tooltip("Список валюты добываемой за клик"), BoxGroup("Настройки уровня")]
            public List<MiningResource> MiningResources = new List<MiningResource>();

            [Tooltip("Стоимость улучшения"), BoxGroup("Настройки уровня")]
            public List<MiningResource> UpgradeCost = new List<MiningResource>();
        }

        [Serializable]
        public class MiningResource
        {
            public CoinType Type;
            public float Value;
        }
    }
}