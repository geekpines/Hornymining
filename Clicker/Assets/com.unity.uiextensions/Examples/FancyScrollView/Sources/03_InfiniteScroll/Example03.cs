/*
 * FancyScrollView (https://github.com/setchi/FancyScrollView)
 * Copyright (c) 2020 setchi
 * Licensed under MIT (https://github.com/setchi/FancyScrollView/blob/master/LICENSE)
 */

using System.Collections.Generic;
using System.Linq;

namespace UnityEngine.UI.Extensions.Examples.FancyScrollViewExample03
{
    class Example03 : MonoBehaviour
    {
        [SerializeField] ScrollView scrollView = default;

        void Start()
        {
            var items = Enumerable.Range(0, 10)
                .Select(i => new ItemData())
                .ToArray();
            
            scrollView.UpdateData(items);
            scrollView.SelectCell(0);
        }
    }
}
