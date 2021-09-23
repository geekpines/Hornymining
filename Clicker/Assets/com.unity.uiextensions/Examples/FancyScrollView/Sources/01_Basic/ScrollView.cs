/*
 * FancyScrollView (https://github.com/setchi/FancyScrollView)
 * Copyright (c) 2020 setchi
 * Licensed under MIT (https://github.com/setchi/FancyScrollView/blob/master/LICENSE)
 */

using System.Collections.Generic;

namespace UnityEngine.UI.Extensions.Examples.FancyScrollViewExample01
{
    class ScrollView : FancyScrollView<ItemData>
    {
        [SerializeField] ScrollerExtension _scrollerExtension = default;
        [SerializeField] GameObject cellPrefab = default;

        protected override GameObject CellPrefab => cellPrefab;

        protected override void Initialize()
        {
            base.Initialize();
            _scrollerExtension.OnValueChanged(UpdatePosition);
        }

        public void UpdateData(IList<ItemData> items)
        {
            UpdateContents(items);
            _scrollerExtension.SetTotalCount(items.Count);
        }
    }
}
