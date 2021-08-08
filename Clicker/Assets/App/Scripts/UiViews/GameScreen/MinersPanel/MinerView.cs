using DragonBones;
using UnityEngine;

namespace App.Scripts.UiViews.GameScreen.MinersPanel
{
    public sealed class MinerView : BaseUiElement<MinerView>
    {
        [field: SerializeField] public UnityArmatureComponent Armature { get; private set; }
        [SerializeField] public GameObject LockMask;

        /// <summary>
        /// Установить компонент анимации для майнера
        /// </summary>
        /// <param name="armature"></param>
        public void SetArmature(UnityArmatureComponent armature)
        {
            Armature = armature;
        }

        /// <summary>
        /// Блокировать/Разблокировать майнера
        /// </summary>
        /// <param name="state"></param>
        public void SetLock(bool state)
        {
            LockMask.SetActive(state);
            Armature.enabled = state;
        }
    }
}