using System;
using System.Collections.Generic;
using UnityEngine;

namespace App.Scripts.Gameplay.CoreGameplay.Mining.MinerLevelUnlock
{
    [Serializable]
    public class LevelUnlock
    {
        public List<GameObject> Cloths = new List<GameObject>();
    }

    [Serializable]
    public class LevelUnlockComponents
    {
        public List<LevelUnlock> Levels = new List<LevelUnlock>();
        [field: SerializeField, Range(0, 4)] public int CurrentLevel { get; private set; } = 0;

        public void SetUnlockLevel(int levelUnlockIndex)
        {
            if (Levels.Count <= levelUnlockIndex)
            {
                Debug.LogError("Указан неверный уровень для разблокировки контента!");
                return;
            }

            CurrentLevel = levelUnlockIndex;
            ResetAllLevels();
            for (int i = 0; i < levelUnlockIndex + 1; i++)
            {
                SetVisibleLevel(Levels[i], false);
            }
        }

        private void SetVisibleLevel(LevelUnlock unlockLevel, bool visible)
        {
            foreach (var unlockLevelCloth in unlockLevel.Cloths)
            {
                unlockLevelCloth.SetActive(visible);
            }
        }

        private void ResetAllLevels()
        {
            foreach (var levelUnlockLevel in Levels)
            {
                SetVisibleLevel(levelUnlockLevel, true);
            }
        }
    }
}