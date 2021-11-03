using System;
using System.Collections.Generic;
using System.Linq;
using App.Scripts.Gameplay.CoreGameplay.Mining;
using App.Scripts.Gameplay.CoreGameplay.Player;
using App.Scripts.UiViews.GameScreen.MinersPanel;
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
        private LevelShopUpgrades Level = new LevelShopUpgrades();

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
            
        }
        
        private void InitializationActiveMiners()
        {
            extension.enabled = false;
            InitializeScroll();
            InitializeViews();
            InitializeContent();
            
            OpenSlotButton.onClick.AddListener(MinersViewController);
            OpenSlotButton.onClick.AddListener(InitializeContent);
            OpenSlotButton.onClick.AddListener(InitializeViews);
            OpenSlotButton.onClick.AddListener(InitializeScroll);
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
            extension.enabled = true;
            _scrollMinerView.CellInterval = Level.OpenMinerSlot(_playerProfile);
        }
    }
}