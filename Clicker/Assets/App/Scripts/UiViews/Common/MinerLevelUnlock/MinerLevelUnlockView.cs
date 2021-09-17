using App.Scripts.UiViews.Common.MinerLevelUnlock;
using UnityEngine;

namespace App.Scripts.UiControllers.Common
{
    public class MinerLevelUnlockView : MonoBehaviour
    {
        public LevelUnlockComponents LevelUnlock { get; private set; }

        public void SetUnlockComponent(LevelUnlockComponents unlockComponents)
        {
            LevelUnlock = unlockComponents;
        }

        public void SetUnlockLevel(int levelUnlockIndex)
        {
            if (LevelUnlock.Levels.Count < levelUnlockIndex)
            {
                Debug.LogError("Указан неверный уровень для разблокировки контента!");
                return;
            }

            LockAllLevel();
            for (int i = LevelUnlock.Levels.Count; i >= levelUnlockIndex; i--)
            {
                SetUnlockLevel(LevelUnlock.Levels[i], true);
            }
        }

        private void SetUnlockLevel(LevelUnlock unlockLevel, bool unlock)
        {
            foreach (var unlockLevelCloth in unlockLevel.Cloths)
            {
                unlockLevelCloth.SetActive(unlock);
            }
        }

        private void LockAllLevel()
        {
            foreach (var levelUnlockLevel in LevelUnlock.Levels)
            {
                SetUnlockLevel(levelUnlockLevel, false);
            }
        }

    }
}