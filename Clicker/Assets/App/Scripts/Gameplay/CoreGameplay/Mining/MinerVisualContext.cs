using System;
using App.Scripts.UiViews.Common.MinerLevelUnlock;
using DragonBones;
using UnityEngine;

namespace App.Scripts.Gameplay.CoreGameplay.Mining
{
    public class MinerVisualContext : MonoBehaviour
    {
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public UnityArmatureComponent ArmatureComponent { get; private set; }
        [field: SerializeField] public LevelUnlockComponents UnlockComponents { get; private set; }

        private void OnValidate()
        {
            UnlockComponents.SetUnlockLevel(UnlockComponents.CurrentLevel);
        }
    }
}