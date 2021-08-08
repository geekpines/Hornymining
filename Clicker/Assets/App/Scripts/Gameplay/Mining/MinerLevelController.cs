using System;
using System.Collections.Generic;
using UnityEngine;

namespace App.Scripts.Foundation
{
    public class MinerLevelController : MonoBehaviour
    {
        public List<LevelContent> Levels = new List<LevelContent>();
        public int CurrentLevel { get; private set; }

        [Serializable]
        public class LevelContent
        {
            public List<GameObject> ClothElements = new List<GameObject>();
            
        }
        
    }
}