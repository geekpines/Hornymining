using App.Scripts.Gameplay.CoreGameplay.Mining;
using App.Scripts.Gameplay.CoreGameplay.Player;
using App.Scripts.UiViews.GameScreen.MinersPanel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;
using UnityEngine.UI.Extensions.Examples.FancyScrollViewExample03;
using Zenject;

namespace App.Scripts.UiControllers.GameScreen.MinersPanel
{
    /// <summary>
    /// Класс управляет ячейками майнеров на сцене (активные майнеры).
    /// Отвечает за отображение контента.
    /// </summary>
    public class MinerActiveSlotsUiController : MonoBehaviour
    {
        [SerializeField] private ScrollerExtension extension;
        [SerializeField] private Button OpenSlotButton;
        [SerializeField] private Button RefreshMinerUI;

        private LevelShopUpgrades Level = new LevelShopUpgrades();
        private string _slotKey = "slot";

        public event Action<MinerSlotView> OnMinerSelected;
        [SerializeField] private ExtendedScrollView _scrollMinerView;
        private PlayerProfile _playerProfile;

        public List<MinerSlotView> MinersSlotView { get; private set; } = new List<MinerSlotView>();
        private Dictionary<int, MinerSlotView> IdToView = new Dictionary<int, MinerSlotView>();

        [Inject]
        private void Construct(PlayerProfile playerProfile)
        {
            _playerProfile = playerProfile;
        }

        private void Awake()
        {
            //todo: добавить в систему инициализации
            InitializationActiveMiners();
            OpenSlotButton.onClick.AddListener(MinersViewController);
            RefreshMinerUI.onClick.AddListener(UpdateVisual);
            Level.LoadLevel(_slotKey);
            Level.SaveLevel(_slotKey);

        }


        private void InitializationActiveMiners()
        {

            InitializeScroll();
            InitializeViews();
            InitializeContent();

        }

        private void InitializeScroll()
        {
            //todo: заменить максимальное начение, на значение из конфига
            var items = Enumerable.Range(0, 5)
                .Select(i => new ItemData())
                .ToArray();
            _scrollMinerView.UpdateData(items);
            _scrollMinerView.SelectCell(0);
        }

        private void InitializeViews()
        {
            foreach (var element in _scrollMinerView.Elements)
            {
                if (element.TryGetComponent(out MinerSlotView view))
                {
                    MinersSlotView.Add(view);
                    view.OnMinerClicked += MinerClicked;
                }
            }
        }

        private void InitializeContent()
        {

            MinersSlotView[0].IsOpen = true;

            if (MinersSlotView.Count > 0)
            {
                var allActiveMiners = _playerProfile.GetActiveMiners();
                for (int i = 0; i < allActiveMiners.Count; i++)
                {
                    AddMinerToSlot(MinersSlotView[i], allActiveMiners[i]);
                }
            }
        }

        private void MinerClicked(MinerSlotView sender)
        {
            OnMinerSelected?.Invoke(sender);
        }

        public void AddMinerToSlot(MinerSlotView viewSlot, Miner miner)
        {


            if (viewSlot != null &&
                viewSlot.IsEmpty)
            {
                if (IdToView.ContainsKey(miner.ID))
                    return;

                var visual = Instantiate(
                    miner.Configuration.Visual,
                    viewSlot.RootPosition);

                viewSlot.SetVisual(
                    visual.gameObject,
                    visual.ArmatureComponent,
                    miner.ID);

                IdToView.Add(miner.ID, viewSlot);
                viewSlot.SetLock(false);

                viewSlot.SetLevelVisual(miner.Level);
                viewSlot.SetName(miner.Name);
            }

        }

        public void RemoveSlot(MinerSlotView viewSlot)
        {
            if (!viewSlot.IsEmpty)
            {
                viewSlot.DestroyVisual();
                viewSlot.SetLock(true);
            }
        }

        public void ClearSlot(MinerSlotView viewSlot)
        {
            if (!viewSlot.IsEmpty)
            {
                viewSlot.SetLock(true);
                Destroy(viewSlot.gameObject);
            }
        }

        public MinerSlotView GetView(int id)
        {
            if (IdToView.ContainsKey(id))
            {
                return IdToView[id];
            }
            return null;
        }

        public void SetLock(int id, bool state)
        {
            if (IdToView.ContainsKey(id))
            {
                IdToView[id].SetLock(state);
            }
        }

        private void OnDestroy()
        {
            foreach (var minerSlotView in MinersSlotView)
            {
                minerSlotView.OnMinerClicked -= MinerClicked;
            }
        }

        private void MinersViewController()
        {

            if (Level.CurrentLevel < 4)
            {
                MinersSlotView[Level.CurrentLevel + 1].IsOpen = Level.OpenMinerSlot(_playerProfile);
            }

        }

        public void UpdateVisual()
        {
            var allActiveMiners = _playerProfile.GetActiveMiners();
            for (int i = 0; i < allActiveMiners.Count; i++)
            {
                CheckForVisualUpdate(MinersSlotView[i], allActiveMiners[i]);
            }
        }

        private void CheckForVisualUpdate(MinerSlotView viewSlot, Miner miner)
        {
            if (viewSlot != null &&
               !viewSlot.IsEmpty)
            {
                viewSlot.SetLevelVisual(miner.Level);
            }
        }

        
    }
}