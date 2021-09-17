/*
 * FancyScrollView (https://github.com/setchi/FancyScrollView)
 * Copyright (c) 2020 setchi
 * Licensed under MIT (https://github.com/setchi/FancyScrollView/blob/master/LICENSE)
 */

using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace UnityEngine.UI.Extensions.Examples.FancyScrollViewExample03
{
    public class Example03 : MonoBehaviour
    {
        [SerializeField] ExtendedScrollView extendedScrollView = default;

        void Start()
        {
            var items = Enumerable.Range(0, 10)
                .Select(i => new ItemData())
                .ToArray();
            
            extendedScrollView.UpdateData(items);
            extendedScrollView.SelectCell(0);
        }
    }
}
