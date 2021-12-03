using App.Scripts.Gameplay.CoreGameplay.Mining;
using App.Scripts.Gameplay.MetaGameplay.Roulette;
using App.Scripts.UiViews.RouletteScreen;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI.Extensions.Examples.FancyScrollViewExample03;

namespace App.Scripts.UiControllers.RouletteScreen
{
    /// <summary>
    /// Контроллер слотов рулетки. Отвечает за отображение в
    /// слотах правильной информации (о майнерах)
    /// </summary>
    public class RouletteSlotsUiController : MonoBehaviour
    {
        [SerializeField] private SpecialScrollMinerView _scrollMinerView = default;
        [SerializeField] private RollMinerSystem _rollMinerSystem;

        [ShowInInspector, ReadOnly]
        public readonly List<MinerSlotContext> Miners = new List<MinerSlotContext>();

        [Serializable]
        public class MinerSlotContext
        {
            [ShowInInspector, ReadOnly]
            public MinerConfiguration Configuration { get; }
            [ShowInInspector, ReadOnly]
            public RouletteSlotView View { get; }

            public MinerSlotContext(MinerConfiguration configuration, RouletteSlotView view)
            {
                Configuration = configuration;
                View = view;
            }
        }

        private void Start()
        {
            InitializationRoulette();
        }

        /// <summary>
        /// Проинициализировать рулетку
        /// </summary>
        public void InitializationRoulette()
        {
            ClearAll();
            InitializeScroll();
            InitializeViews();
        }

        private void ClearAll()
        {
            foreach (var miner in Miners)
            {
                miner.View.DestroyVisual();
            }
            Miners.Clear();
        }

        private void InitializeScroll()
        {
            var items = Enumerable.Range(0, _rollMinerSystem.Configuration.RouletteItems.Count)
                .Select(i => new ItemData())
                .ToArray();
            _scrollMinerView.UpdateData(items);
            _scrollMinerView.SelectCell(0);
        }

        private void InitializeViews()
        {
            var j = 0;
            for (int i = 0; i < _scrollMinerView.MinerViews.Count; i++)
            {
                if (j >= _rollMinerSystem.Configuration.RouletteItems.Count)
                    j = 0;

                var visual = Instantiate(
                    _rollMinerSystem.Configuration.RouletteItems[j].Item.Visual,
                    _scrollMinerView.MinerViews[i].RootPosition);
                _scrollMinerView.MinerViews[i].SetVisual(
                    visual.gameObject,
                    visual.ArmatureComponent,
                    _rollMinerSystem.Configuration.RouletteItems[j].Item.GetInstanceID());
                _scrollMinerView.MinerViews[i].SetInformation(
                    _rollMinerSystem.Configuration.RouletteItems[j].Item.Name);

                Miners.Add(new MinerSlotContext(
                    _rollMinerSystem.Configuration.RouletteItems[j].Item,
                    _scrollMinerView.MinerViews[i]));
                j++;
            }
        }
    }
}