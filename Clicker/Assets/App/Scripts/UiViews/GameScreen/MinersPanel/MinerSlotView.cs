using UnityEngine;

namespace App.Scripts.UiViews.GameScreen.MinersPanel
{
    public class MinerSlotView : MinerView
    {
        [SerializeField] public GameObject LockMask;
        [field: SerializeField] public Transform RootPosition { get; private set; }
        public GameObject RootVisual { get; private set; }
        public int ConfigHash { get; private set; }

        [field: SerializeField]
        public bool IsLocked { get; private set; }

        /// <summary>
        /// Блокировать/Разблокировать майнера
        /// </summary>
        /// <param name="state"></param>
        public void SetLock(bool state)
        {
            LockMask.SetActive(state);
            IsLocked = state;
        }
        
        
    }
}