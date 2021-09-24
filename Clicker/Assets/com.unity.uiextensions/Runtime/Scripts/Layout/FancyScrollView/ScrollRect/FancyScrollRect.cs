﻿/// Credit setchi (https://github.com/setchi)
/// Sourced from - https://github.com/setchi/FancyScrollView

using System;
using System.Collections.Generic;
using UnityEngine.UI.Extensions.EasingCore;

namespace UnityEngine.UI.Extensions
{
    /// <summary>
    /// ScrollRect スタイルのスクロールビューを実装するための抽象基底クラス.
    /// 無限スクロールおよびスナップには対応していません.
    /// <see cref="FancyScrollView{TItemData, TContext}.Context"/> が不要な場合は
    /// 代わりに <see cref="FancyScrollRect{TItemData}"/> を使用します.
    /// </summary>
    /// <typeparam name="TItemData">アイテムのデータ型.</typeparam>
    /// <typeparam name="TContext"><see cref="FancyScrollView{TItemData, TContext}.Context"/> の型.</typeparam>
    [RequireComponent(typeof(ScrollerExtension))]
    public abstract class FancyScrollRect<TItemData, TContext> : FancyScrollView<TItemData, TContext>
        where TContext : class, IFancyScrollRectContext, new()
    {
        /// <summary>
        /// スクロール中にセルが再利用されるまでの余白のセル数.
        /// </summary>
        /// <remarks>
        /// <c>0</c> を指定するとセルが完全に隠れた直後に再利用されます.
        /// <c>1</c> 以上を指定すると, そのセル数だけ余分にスクロールしてから再利用されます.
        /// </remarks>
        [SerializeField] protected float reuseCellMarginCount = 0f;

        /// <summary>
        /// コンテンツ先頭の余白.
        /// </summary>
        [SerializeField] protected float paddingHead = 0f;

        /// <summary>
        /// コンテンツ末尾の余白.
        /// </summary>
        [SerializeField] protected float paddingTail = 0f;

        /// <summary>
        /// スクロール軸方向のセル同士の余白.
        /// </summary>
        [SerializeField] protected float spacing = 0f;

        /// <summary>
        /// セルのサイズ.
        /// </summary>
        protected abstract float CellSize { get; }

        /// <summary>
        /// スクロール可能かどうか.
        /// </summary>
        /// <remarks>
        /// アイテム数が十分少なくビューポート内に全てのセルが収まっている場合は <c>false</c>, それ以外は <c>true</c> になります.
        /// </remarks>
        protected virtual bool Scrollable => MaxScrollPosition > 0f;

        ScrollerExtension _cachedScrollerExtension;

        /// <summary>
        /// スクロール位置を制御する <see cref="FancyScrollView.Scroller"/> のインスタンス.
        /// </summary>
        /// <remarks>
        /// <see cref="ScrollerExtension"/> のスクロール位置を変更する際は必ず <see cref="ToScrollerPosition(float)"/> を使用して変換した位置を使用してください.
        /// </remarks>
        protected ScrollerExtension ScrollerExtension => _cachedScrollerExtension ?? (_cachedScrollerExtension = GetComponent<ScrollerExtension>());

        float ScrollLength => 1f / Mathf.Max(CellInterval, 1e-2f) - 1f;

        float ViewportLength => ScrollLength - reuseCellMarginCount * 2f;

        float PaddingHeadLength => (paddingHead - spacing * 0.5f) / (CellSize + spacing);

        float MaxScrollPosition => ItemsSource.Count
            - ScrollLength
            + reuseCellMarginCount * 2f
            + (paddingHead + paddingTail - spacing) / (CellSize + spacing);

        /// <inheritdoc/>
        protected override void Initialize()
        {
            base.Initialize();

            Context.ScrollDirection = ScrollerExtension.ScrollDirection;
            Context.CalculateScrollSize = () =>
            {
                var interval = CellSize + spacing;
                var reuseMargin = interval * reuseCellMarginCount;
                var scrollSize = ScrollerExtension.ViewportSize + interval + reuseMargin * 2f;
                return (scrollSize, reuseMargin);
            };

            AdjustCellIntervalAndScrollOffset();
            ScrollerExtension.OnValueChanged(OnScrollerValueChanged);
        }

        /// <summary>
        /// <see cref="ScrollerExtension"/> のスクロール位置が変更された際の処理.
        /// </summary>
        /// <param name="p"><see cref="ScrollerExtension"/> のスクロール位置.</param>
        void OnScrollerValueChanged(float p)
        {
            base.UpdatePosition(Scrollable ? ToFancyScrollViewPosition(p) : 0f);

            if (ScrollerExtension.Scrollbar)
            {
                if (p > ItemsSource.Count - 1)
                {
                    ShrinkScrollbar(p - (ItemsSource.Count - 1));
                }
                else if (p < 0f)
                {
                    ShrinkScrollbar(-p);
                }
            }
        }

        /// <summary>
        /// スクロール範囲を超えてスクロールされた量に基づいて, スクロールバーのサイズを縮小します.
        /// </summary>
        /// <param name="offset">スクロール範囲を超えてスクロールされた量.</param>
        void ShrinkScrollbar(float offset)
        {
            var scale = 1f - ToFancyScrollViewPosition(offset) / (ViewportLength - PaddingHeadLength);
            UpdateScrollbarSize((ViewportLength - PaddingHeadLength) * scale);
        }

        /// <inheritdoc/>
        protected override void Refresh()
        {
            AdjustCellIntervalAndScrollOffset();
            RefreshScroller();
            base.Refresh();
        }

        /// <inheritdoc/>
        protected override void Relayout()
        {
            AdjustCellIntervalAndScrollOffset();
            RefreshScroller();
            base.Relayout();
        }

        /// <summary>
        /// <see cref="ScrollerExtension"/> の各種状態を更新します.
        /// </summary>
        protected void RefreshScroller()
        {
            ScrollerExtension.Draggable = Scrollable;
            ScrollerExtension.ScrollSensitivity = ToScrollerPosition(ViewportLength - PaddingHeadLength);
            ScrollerExtension.Position = ToScrollerPosition(currentPosition);

            if (ScrollerExtension.Scrollbar)
            {
                ScrollerExtension.Scrollbar.gameObject.SetActive(Scrollable);
                UpdateScrollbarSize(ViewportLength);
            }
        }

        /// <inheritdoc/>
        protected override void UpdateContents(IList<TItemData> items)
        {
            Debug.Assert(Context.CalculateScrollSize != null);

            AdjustCellIntervalAndScrollOffset();
            base.UpdateContents(items);

            ScrollerExtension.SetTotalCount(items.Count);
            RefreshScroller();
        }

        /// <summary>
        /// スクロール位置を更新します.
        /// </summary>
        /// <param name="position">スクロール位置.</param>
        protected new void UpdatePosition(float position)
        {
            ScrollerExtension.Position = ToScrollerPosition(position, 0.5f);
        }

        /// <summary>
        /// 指定したアイテムの位置までジャンプします.
        /// </summary>
        /// <param name="itemIndex">アイテムのインデックス.</param>
        /// <param name="alignment">ビューポート内におけるセル位置の基準. 0f(先頭) ~ 1f(末尾).</param>
        protected virtual void JumpTo(int itemIndex, float alignment = 0.5f)
        {
            ScrollerExtension.Position = ToScrollerPosition(itemIndex, alignment);
        }

        /// <summary>
        /// 指定したアイテムの位置まで移動します.
        /// </summary>
        /// <param name="index">アイテムのインデックス.</param>
        /// <param name="duration">移動にかける秒数.</param>
        /// <param name="alignment">ビューポート内におけるセル位置の基準. 0f(先頭) ~ 1f(末尾).</param>
        /// <param name="onComplete">移動が完了した際に呼び出されるコールバック.</param>
        protected virtual void ScrollTo(int index, float duration, float alignment = 0.5f, Action onComplete = null)
        {
            ScrollerExtension.ScrollTo(ToScrollerPosition(index, alignment), duration, onComplete);
        }

        /// <summary>
        /// 指定したアイテムの位置まで移動します.
        /// </summary>
        /// <param name="index">アイテムのインデックス.</param>
        /// <param name="duration">移動にかける秒数.</param>
        /// <param name="easing">移動に使用するイージング.</param>
        /// <param name="alignment">ビューポート内におけるセル位置の基準. 0f(先頭) ~ 1f(末尾).</param>
        /// <param name="onComplete">移動が完了した際に呼び出されるコールバック.</param>
        protected virtual void ScrollTo(int index, float duration, Ease easing, float alignment = 0.5f, Action onComplete = null)
        {
            ScrollerExtension.ScrollTo(ToScrollerPosition(index, alignment), duration, easing, onComplete);
        }

        /// <summary>
        /// ビューポートとコンテンツの長さに基づいてスクロールバーのサイズを更新します.
        /// </summary>
        /// <param name="viewportLength">ビューポートのサイズ.</param>
        protected void UpdateScrollbarSize(float viewportLength)
        {
            var contentLength = Mathf.Max(ItemsSource.Count + (paddingHead + paddingTail - spacing) / (CellSize + spacing), 1);
            ScrollerExtension.Scrollbar.size = Scrollable ? Mathf.Clamp01(viewportLength / contentLength) : 1f;
        }

        /// <summary>
        /// <see cref="ScrollerExtension"/> が扱うスクロール位置を <see cref="FancyScrollRect{TItemData, TContext}"/> が扱うスクロール位置に変換します.
        /// </summary>
        /// <param name="position"><see cref="ScrollerExtension"/> が扱うスクロール位置.</param>
        /// <returns><see cref="FancyScrollRect{TItemData, TContext}"/> が扱うスクロール位置.</returns>
        protected float ToFancyScrollViewPosition(float position)
        {
            return position / Mathf.Max(ItemsSource.Count - 1, 1) * MaxScrollPosition - PaddingHeadLength;
        }

        /// <summary>
        /// <see cref="FancyScrollRect{TItemData, TContext}"/> が扱うスクロール位置を <see cref="ScrollerExtension"/> が扱うスクロール位置に変換します.
        /// </summary>
        /// <param name="position"><see cref="FancyScrollRect{TItemData, TContext}"/> が扱うスクロール位置.</param>
        /// <returns><see cref="ScrollerExtension"/> が扱うスクロール位置.</returns>
        protected float ToScrollerPosition(float position)
        {
            return (position + PaddingHeadLength) / MaxScrollPosition * Mathf.Max(ItemsSource.Count - 1, 1);
        }

        /// <summary>
        /// <see cref="FancyScrollRect{TItemData, TContext}"/> が扱うスクロール位置を <see cref="ScrollerExtension"/> が扱うスクロール位置に変換します.
        /// </summary>
        /// <param name="position"><see cref="FancyScrollRect{TItemData, TContext}"/> が扱うスクロール位置.</param>
        /// <param name="alignment">ビューポート内におけるセル位置の基準. 0f(先頭) ~ 1f(末尾).</param>
        /// <returns><see cref="ScrollerExtension"/> が扱うスクロール位置.</returns>
        protected float ToScrollerPosition(float position, float alignment = 0.5f)
        {
            var offset = alignment * (ScrollLength - (1f + reuseCellMarginCount * 2f))
                + (1f - alignment - 0.5f) * spacing / (CellSize + spacing);
            return ToScrollerPosition(Mathf.Clamp(position - offset, 0f, MaxScrollPosition));
        }

        /// <summary>
        /// 指定された設定を実現するための
        /// <see cref="FancyScrollView{TItemData,TContext}.CellInterval"/> と
        /// <see cref="FancyScrollView{TItemData,TContext}.scrollOffset"/> を計算して適用します.
        /// </summary>
        protected void AdjustCellIntervalAndScrollOffset()
        {
            var totalSize = ScrollerExtension.ViewportSize + (CellSize + spacing) * (1f + reuseCellMarginCount * 2f);
            CellInterval = (CellSize + spacing) / totalSize;
            scrollOffset = CellInterval * (1f + reuseCellMarginCount);
        }

        protected virtual void OnValidate()
        {
            AdjustCellIntervalAndScrollOffset();

            if (loop)
            {
                loop = false;
                Debug.LogError("Loop is currently not supported in FancyScrollRect.");
            }

            if (ScrollerExtension.SnapEnabled)
            {
                ScrollerExtension.SnapEnabled = false;
                Debug.LogError("Snap is currently not supported in FancyScrollRect.");
            }

            if (ScrollerExtension.MovementType == MovementType.Unrestricted)
            {
                ScrollerExtension.MovementType = MovementType.Elastic;
                Debug.LogError("MovementType.Unrestricted is currently not supported in FancyScrollRect.");
            }
        }
    }

    /// <summary>
    /// ScrollRect スタイルのスクロールビューを実装するための抽象基底クラス.
    /// 無限スクロールおよびスナップには対応していません.
    /// </summary>
    /// <typeparam name="TItemData">アイテムのデータ型.</typeparam>
    /// <seealso cref="FancyScrollRect{TItemData, TContext}"/>
    public abstract class FancyScrollRect<TItemData> : FancyScrollRect<TItemData, FancyScrollRectContext> { }
}