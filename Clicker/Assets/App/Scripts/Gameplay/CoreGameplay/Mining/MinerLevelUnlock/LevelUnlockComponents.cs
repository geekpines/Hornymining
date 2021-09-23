using System;
using System.Collections.Generic;
using UnityEngine;

namespace App.Scripts.UiViews.Common.MinerLevelUnlock
{
    [Serializable]
    public class LevelUnlockComponents
    {
        public List<LevelUnlock> Levels = new List<LevelUnlock>();
    }
    
    [Serializable]
    public class LevelUnlock
    {
        public List<GameObject> Cloths = new List<GameObject>();
    }
}