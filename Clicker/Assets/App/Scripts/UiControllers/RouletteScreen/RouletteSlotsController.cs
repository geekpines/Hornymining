using System.Collections.Generic;
using System.Linq;
using App.Scripts.Gameplay.CoreGameplay.Mining;
using App.Scripts.Gameplay.MetaGameplay.Roulette;
using App.Scripts.UiControllers.Commmon;
using App.Scripts.UiViews.RouletteScreen;
using UnityEngine;
using UnityEngine.UI.Extensions.Examples.FancyScrollViewExample03;

namespace App.Scripts.Gameplay.UiControllers.RouletteScreen
{
    public class RouletteSlotsController : MonoBehaviour
    {
        [SerializeField] private SpecialScrollMinerView _scrollMinerView = default;
        [SerializeField] private RollMinerController _rollMinerController;
        
        private Dictionary<MinerConfiguration, RouletteSlotView> _configToView =
            new Dictionary<MinerConfiguration, RouletteSlotView>();
        
        private void Start()
        {
            InitializationRoulette();
        }

        public void InitializationRoulette()
        {
            InitializeScroll();
            InitializeDictionary();
            InitializeViews();
        }

        private void InitializeDictionary()
        {
            _configToView.Clear();
            for (int i = 0; i < _rollMinerController.Configuration.RouletteItems.Count; i++)
            {
                if (!_configToView.ContainsKey(_rollMinerController.Configuration.RouletteItems[i].Item))
                {
                    _configToView.Add(
                        _rollMinerController.Configuration.RouletteItems[i].Item,
                        _scrollMinerView.MinerViews[i]);
                }
            }
        }

        private void InitializeScroll()
        {
            var items = Enumerable.Range(0, _rollMinerController.Configuration.RouletteItems.Count)
                .Select(i => new ItemData())
                .ToArray();
            _scrollMinerView.UpdateData(items);
            _scrollMinerView.SelectCell(0);
        }

        private void InitializeViews()
        {
            foreach (var config in _configToView.Keys)
            {
                var visual = Instantiate(config.Visual, _configToView[config].RootPosition);
                _configToView[config].SetVisual(visual.ArmatureComponent);
            }
        }
    }
}