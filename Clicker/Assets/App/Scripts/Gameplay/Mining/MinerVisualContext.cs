using App.Scripts.UiControllers.Common.MinerLevelUnlock;
using DragonBones;
using UnityEngine;

namespace App.Scripts.Foundation
{
    public class MinerVisualContext : MonoBehaviour
    {
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public UnityArmatureComponent ArmatureComponent { get; private set; }
        [field: SerializeField] public LevelUnlockComponents UnlockComponents { get; private set; }
    }
}