using App.Scripts.UiViews.Common.MinerLevelUnlock;
using DragonBones;
using UnityEngine;

namespace App.Scripts.UiViews.CoreGameplay.Mining
{
    public class MinerVisualContext : BaseUiElement<MinerVisualContext>
    {
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public UnityArmatureComponent ArmatureComponent { get; private set; }
        [field: SerializeField] public LevelUnlockComponents UnlockComponents { get; private set; }
    }
}