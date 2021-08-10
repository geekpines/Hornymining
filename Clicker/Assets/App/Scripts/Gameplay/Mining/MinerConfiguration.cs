using System;
using System.Collections.Generic;
using App.Scripts.UiControllers.Common.MinerLevelUnlock;
using DragonBones;
using UnityEngine;
using UnityEngine.Localization;

namespace App.Scripts.Foundation
{
    [CreateAssetMenu(fileName = "MinerConfiguration", menuName = "Game/MinerConfiguration", order = 0)]
    public class MinerConfiguration : ScriptableObject
    {
        public LocalizedString Name;
        public LocalizedString Description;
        public Sprite Icon;
        public UnityArmatureComponent Armature;
        public LevelUnlockComponents Level;
        public List<MiningResource> MiningResources = new List<MiningResource>();
        
        [Serializable]
        public class MiningResource
        {
            public CoinType Type;
            public float Value;
        }
    }
}