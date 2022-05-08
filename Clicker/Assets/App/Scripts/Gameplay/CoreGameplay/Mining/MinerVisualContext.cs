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
        [field: SerializeField] public LevelUnlockComponents PartsUnlockComponents { get; private set; }
        private void OnValidate()
        {
            UnlockComponents.SetUnlockLevel(UnlockComponents.CurrentLevel);
            ArmatureComponent.animationName = "action";
        }

        private void OnDisable()
        {
            GameObject Miner = GameObject.Find(gameObject.name + "(Clone)");
            if (Miner != null)
            {
                MinerVisualContext secondGameObject = Miner.GetComponent<MinerVisualContext>();

                for (int i = 0; i < PartsUnlockComponents.Levels.Count; i++)
                {
                    for (int j = 0; j < PartsUnlockComponents.Levels[i].Cloths.Count; j++)
                    {
                        secondGameObject.PartsUnlockComponents.Levels[i].Cloths[j].SetActive(PartsUnlockComponents.Levels[i].Cloths[j].activeSelf);
                    }
                }
            }            
        }
    }
}