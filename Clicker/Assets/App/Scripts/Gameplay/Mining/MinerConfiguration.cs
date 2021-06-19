using System;
using System.Collections.Generic;
using UnityEngine;

namespace App.Scripts.Gameplay.Mining
{
    [CreateAssetMenu(fileName = "MinerConfiguration", menuName = "Game/Mining/MinerConfiguration", order = 0)]
    public class MinerConfiguration : ScriptableObject
    {
        public string Name;
        public string Description;
        public Sprite Icon;
        public List<SequenceCharacter> SequenceLevel;
        
        [Serializable]
        public class SequenceCharacter
        {
            public List<Sprite> Sequence = new List<Sprite>();
        }

    }
}