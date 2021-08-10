using UnityEngine;

namespace App.Scripts.UiViews.GameScreen.MinersPanel
{
    public class MinerSlotView : MinerView
    {
        [SerializeField] public GameObject LockMask;
        
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