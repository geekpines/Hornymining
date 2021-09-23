using System.Collections.Generic;
using App.Scripts.UiViews.RouletteScreen;
using UnityEngine;
using UnityEngine.UI.Extensions;
using UnityEngine.UI.Extensions.EasingCore;
using UnityEngine.UI.Extensions.Examples.FancyScrollViewExample03;

namespace App.Scripts.UiControllers.Commmon
{
    public class SpecialScrollMinerView : FancyScrollView<ItemData, Context>
    {
        [SerializeField] private Scroller _scroller = default;
        [SerializeField] private GameObject _cellPrefab = default;
        public List<RouletteSlotView> MinerViews { get; private set; } = new List<RouletteSlotView>();

        protected override GameObject CellPrefab => _cellPrefab;

        protected override void Initialize()
        {
            base.Initialize();

            Context.OnCellClicked = SelectCell;

            _scroller.OnValueChanged(UpdatePosition);
            _scroller.OnSelectionChanged(UpdateSelection);
        }

        private void OnEnable()
        {
            OnSpawned += TryAddMinerView;
        }

        private void OnDisable()
        {
            OnSpawned -= TryAddMinerView;
        }

        private void TryAddMinerView(GameObject cell)
        {
            if (cell.TryGetComponent<RouletteSlotView>(out var minerView))
            {
                if (!MinerViews.Contains(minerView))
                {
                    MinerViews.Add(minerView);
                }
            }
        }

        void UpdateSelection(int index)
        {
            if (Context.SelectedIndex == index)
            {
                return;
            }

            Context.SelectedIndex = index;
            Refresh();
        }

        public void UpdateData(IList<ItemData> items)
        {
            UpdateContents(items);
            _scroller.SetTotalCount(items.Count);
        }

        public void SelectCell(int index)
        {
            if (index < 0 || index >= ItemsSource.Count || index == Context.SelectedIndex)
            {
                return;
            }

            UpdateSelection(index);
            _scroller.ScrollTo(index, 0.35f, Ease.OutCubic);
        }
    }
}