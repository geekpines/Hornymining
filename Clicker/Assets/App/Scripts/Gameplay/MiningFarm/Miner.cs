using System;
using System.Collections.Generic;
using App.Scripts.UiControllers.GameScreen;
using UnityEngine;

namespace App.Scripts.Gameplay.Mining
{
    public class Miner : MonoBehaviour
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