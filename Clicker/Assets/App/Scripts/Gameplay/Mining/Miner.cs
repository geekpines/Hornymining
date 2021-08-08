using System;
using System.Collections.Generic;
using App.Scripts.Foundation.Roulette.Core;
using UnityEngine;

namespace App.Scripts.Foundation
{
    public class Miner : RouletteItem<Miner>
    {
        public string Name;
        public string Description;
        public Sprite Icon;
        public List<SequenceCharacter> LevelSequences;
        public List<Sprite> ClickSequence = new List<Sprite>();
        
        [Serializable]
        public class SequenceCharacter
        {
            public List<Sprite> Sequence = new List<Sprite>();
        }
    }
}