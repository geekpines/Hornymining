using System;
using System.Collections.Generic;
using System.Linq;
using App.Scripts.Gameplay.CoreGameplay.Mining;
using App.Scripts.Gameplay.CoreGameplay.Player;
using App.Scripts.UiViews.GameScreen.MinersPanel;
using UnityEngine;
using UnityEngine.UI.Extensions.Examples.FancyScrollViewExample03;
using Zenject;

namespace App.Scripts.UiControllers.GameScreen.MinersPanel
{
    /// <summary>
    /// Класс управляет ячейками майнеров на сцене. Отвечает за отображение
    /// контента.
    /// </summary>
    public class MinerActiveSlotsUiController : MonoBehaviour
    {
        public event Action<int> OnMinerSelected;
        [SerializeField] private ExtendedScrollView _scrollMinerView;
        private PlayerProfile _playerProfile;

        private List<MinerSlotView> _minerSlotViews = new List<MinerSlotView>();
        private Dictionary<int, MinerSlotView> IdToView = new Dictionary<int, MinerSlotView>();
        
        [Inject]
        private void Construct(PlayerProfile playerProfile)
        {
            _playerProfile = playerProfile;
        }

        private void Start()
        {
            //todo: добавить в систему инициализации
            InitializationActiveMiners();
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
                    _minerSlotViews.Add(view);
                    view.OnMinerClicked += MinerClicked;
                }
            }
        }
        
        private void InitializeContent()
        {
            if (_minerSlotViews.Count > 0)
            {
                var allActiveMiners = _playerProfile.GetActiveMiners();
                for (int i = 0; i < allActiveMiners.Count; i++)
                {
                    AddMinerToSlot(_minerSlotViews[i], allActiveMiners[i]);
                }
            }
        }
        
        private void MinerClicked(MinerSlotView sender)
        {
            OnMinerSelected?.Invoke(sender.ConfigHash);
        }

        public void AddMinerToSlot(MinerSlotView viewSlot, Miner miner)
        {
            if (viewSlot.IsEmpty)
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

        public bool GetAvailableSlot(int id)
        {
            if (IdToView.ContainsKey(id))
            {
                return IdToView[id].IsLocked;
            }
            return true;
        }

        private void OnDestroy()
        {
            foreach (var minerSlotView in _minerSlotViews)
            {
                minerSlotView.OnMinerClicked -= MinerClicked;
            }
        }
    }
}