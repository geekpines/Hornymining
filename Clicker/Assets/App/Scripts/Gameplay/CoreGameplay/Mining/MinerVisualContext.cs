using App.Scripts.Gameplay.CoreGameplay.Mining.MinerLevelUnlock;
using DragonBones;
using UnityEngine;

namespace App.Scripts.Gameplay.CoreGameplay.Mining
{
    public class MinerVisualContext : MonoBehaviour
    {
        [field: SerializeField] public Sprite Icon { get; private set; }        
        [field: SerializeField] public Sprite CoinIcon { get; private set; }
        [field: SerializeField] public UnityArmatureComponent ArmatureComponent { get; private set; }
        [field: SerializeField] public LevelUnlockComponents UnlockComponents { get; private set; }

        private void OnValidate()
        {
            UnlockComponents.SetUnlockLevel(UnlockComponents.CurrentLevel);
            ArmatureComponent.animationName = "action";
        }
    }
}