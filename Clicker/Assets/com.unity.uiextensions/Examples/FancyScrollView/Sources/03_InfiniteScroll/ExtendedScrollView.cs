/*
 * FancyScrollView (https://github.com/setchi/FancyScrollView)
 * Copyright (c) 2020 setchi
 * Licensed under MIT (https://github.com/setchi/FancyScrollView/blob/master/LICENSE)
 */

using System;
using System.Collections.Generic;
using UnityEngine.UI.Extensions.EasingCore;

namespace UnityEngine.UI.Extensions.Examples.FancyScrollViewExample03
{
    public class ExtendedScrollView : FancyScrollView<ItemData, Context>
    {
        [SerializeField] ScrollerExtension _scrollerExtension = default;
        [SerializeField] GameObject cellPrefab = default;
        public List<GameObject> Elements { get; private set; } = new List<GameObject>();

        protected override GameObject CellPrefab => cellPrefab;

        protected override void Initialize()
        {
            base.Initialize();

            Context.OnCellClicked = SelectCell;

            _scrollerExtension.OnValueChanged(UpdatePosition);
            _scrollerExtension.OnSelectionChanged(UpdateSelection);
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

        private void OnEnable()
        {
            OnSpawned += AddElement;
        }

        private void OnDisable()
        {
            OnSpawned -= AddElement;
        }

        private void AddElement(GameObject element)
        {
            if (!Elements.Contains(element))
            {
                Elements.Add(element);
            }
        }

        public void UpdateData(IList<ItemData> items)
        {
            UpdateContents(items);
            _scrollerExtension.SetTotalCount(items.Count);
        }

        public void SelectCell(int index)
        {
            if (index < 0 || index >= ItemsSource.Count || index == Context.SelectedIndex)
            {
                return;
            }

            UpdateSelection(index);
            _scrollerExtension.ScrollTo(index, 0.35f, Ease.OutCubic);
        }
    }
}
