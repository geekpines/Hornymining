using System;
using DragonBones;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using Transform = UnityEngine.Transform;

namespace App.Scripts.UiViews.GameScreen.MinersPanel
{
    public class MinerSlotView : MinerView
    {
        public event Action<MinerSlotView> OnMinerClicked;

        [SerializeField] public GameObject MinerContent;
        [SerializeField] public GameObject LockMask;
        [field: SerializeField] public Transform RootPosition { get; private set; }
        public GameObject RootVisual { get; private set; }
        public int Id { get; private set; }

        [field: SerializeField] public bool IsLocked { get; private set; } = true;

        [Header("Кнопки")] 
        [SerializeField] private Button _minerButton;
        [SerializeField] private Button _lockButton;

        /// <summary>
        /// Блокировать/Разблокировать майнера
        /// </summary>
        /// <param name="state"></param>
        public void SetLock(bool state)
        {
            MinerContent.SetActive(!state);
            LockMask.SetActive(state);
            IsLocked = state;
        }
        
        public void SetVisual(GameObject rootObject, UnityArmatureComponent armatureComponent, int configHash)
        {
            RootVisual = rootObject;
            Id = configHash;
            SetVisual(armatureComponent);
        }
        
        public void DestroyVisual()
        {
            if (RootVisual != null)
            {
                Destroy(RootVisual);
            }
        }

        public void GenerateIdWithoutVisual()
        {
            Id = Random.Range(Int32.MinValue, Int32.MaxValue);
        }

        private void OnEnable()
        {
            _minerButton.onClick.AddListener(MinerClicked);
            _lockButton.onClick.AddListener(MinerClicked);
        }

        private void OnDisable()
        {
            _minerButton.onClick.RemoveListener(MinerClicked);
            _lockButton.onClick.RemoveListener(MinerClicked);
        }

        private void MinerClicked()
        {
            OnMinerClicked?.Invoke(this);
        }
    }
}