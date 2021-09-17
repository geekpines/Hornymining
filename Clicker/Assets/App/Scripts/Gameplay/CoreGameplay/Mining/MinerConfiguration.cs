using System;
using System.Collections.Generic;
using App.Scripts.Gameplay.CoreGameplay.Coins;
using App.Scripts.UiViews.CoreGameplay.Mining;
using UnityEngine;
using UnityEngine.Localization;

namespace App.Scripts.Gameplay.CoreGameplay.Mining
{
    [CreateAssetMenu(fileName = "MinerConfiguration", menuName = "Game/MinerConfiguration", order = 0)]
    public class MinerConfiguration : ScriptableObject
    {
        public LocalizedString Name;
        public LocalizedString Description;
        public MinerVisualContext Visual;
        public List<MiningResource> MiningResources = new List<MiningResource>();
        
        [Serializable]
        public class MiningResource
        {
            public CoinType Type;
            public float Value;
        }
    }
}