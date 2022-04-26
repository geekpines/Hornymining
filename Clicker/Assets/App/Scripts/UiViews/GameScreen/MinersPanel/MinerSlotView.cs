using App.Scripts.UiViews.GameScreen.Stone;
using DragonBones;
using System;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.UI;
using Transform = UnityEngine.Transform;

namespace App.Scripts.UiViews.GameScreen.MinersPanel
{
    public class MinerSlotView : MinerView
    {
        public event Action<MinerSlotView> OnMinerClicked;

        [SerializeField] private GameObject _plusIcon;
        [SerializeField] private GameObject _minerContent;
        [SerializeField] private GameObject _lockMask;
        [SerializeField] private MiningCoinsView _miningCoinsView;
        
        public Image videocard;
        public DialogUiController dialogUiController;

        [field: SerializeField] public Transform RootPosition { get; private set; }
        public GameObject RootVisual { get; private set; }
        public int Id { get; private set; }

        [field: SerializeField] public bool IsLocked { get; private set; } = true;
        public bool IsOpen = false;

        [Header("Кнопки")]
        [SerializeField] private Button _minerButton;
        [SerializeField] private Button _lockButton;

        [SerializeField] public LocalizeStringEvent minerName { get; private set; }

        /// <summary>
        /// Блокировать/Разблокировать майнера
        /// </summary>
        /// <param name="state"></param>
        public void SetLock(bool state)
        {
            _minerContent.SetActive(!state);
            _lockMask.SetActive(state);
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

        public void ShowScoreLine(Sprite icon, float score)
        {
            _miningCoinsView.ShowScoreAnimation(icon, score);
        }

        private void OnEnable()
        {
            _minerButton.onClick.AddListener(MinerClicked);
            _lockButton.onClick.AddListener(MinerClicked);
            if (!IsOpen)
            {
                _plusIcon.SetActive(false);
            }
            else
            {
                _plusIcon.SetActive(true);
            }
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

        public void SetName(LocalizedString minerName)
        {
            Debug.Log(this.minerName.name + "Miner Name: " + minerName);
            this.minerName.StringReference = minerName;
        }

        public bool CheckName(LocalizedString minerName)
        {
            if(this.minerName.StringReference == minerName)
            {
                return true;
            }
            return false;
        }

        public void SetVisible()
        {
            if (IsOpen)
            {
                _plusIcon.SetActive(true);
            }
        }
    }
}