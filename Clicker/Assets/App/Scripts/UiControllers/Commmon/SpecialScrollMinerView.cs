using System;
using System.Collections;
using System.Collections.Generic;
using App.Scripts.UiViews.RouletteScreen;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI.Extensions;
using UnityEngine.UI.Extensions.Examples.FancyScrollViewExample03;
using Ease = UnityEngine.UI.Extensions.EasingCore.Ease;

namespace App.Scripts.UiControllers.Commmon
{
    public class SpecialScrollMinerView : FancyScrollView<ItemData, Context>
    {
        [SerializeField] private ScrollerExtension _scrollerExtension = default;
        [SerializeField] private GameObject _cellPrefab = default;
        public List<RouletteSlotView> MinerViews { get; private set; } = new List<RouletteSlotView>();
        public int Size => ItemsSource.Count;
        public RouletteSlotView CurrentSelected => MinerViews[CurrentSelectedIndex];
        public int CurrentSelectedIndex
        {
            get
            {
                if (currentPosition >= 0 &&
                    currentPosition < MinerViews.Count)
                {
                    return (int)currentPosition;
                }

                if (currentPosition >= 0)
                {
                    return (int)((currentPosition % MinerViews.Count));
                }
                else if (currentPosition < 0)
                {
                    var temp = (int) (MinerViews.Count + (currentPosition % MinerViews.Count));
                    if (temp == MinerViews.Count)
                        temp = 0;
                    return temp;
                }
                return (int)currentPosition;
            }
        }

        protected override GameObject CellPrefab => _cellPrefab;

        protected override void Initialize()
        {
            base.Initialize();

            Context.OnCellClicked = SelectCell;

            _scrollerExtension.OnValueChanged(UpdatePosition);
            _scrollerExtension.OnSelectionChanged(UpdateSelection);
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

        private void UpdateSelection(int index)
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

        /// <summary>
        /// Прокрутить рулетку до определенной ячейки
        /// </summary>
        /// <param name="position">На сколько позиций сдвинуть (минимум 30!)</param>
        /// <param name="duration">Время, за которое сделать сдвиг</param>
        /// <param name="onScrolled"></param>
        public void ScrollTo(int position, float duration, Action onScrolled)
        {
            StartCoroutine(ScrollingToIndex(position, duration, onScrolled));
        }

        /// <summary>
        /// Корутина содержит всю логику скроллинга (включая остановку).
        /// </summary>
        /// <param name="position"></param>
        /// <param name="duration"></param>
        /// <param name="onScrolled"></param>
        /// <returns></returns>
        private IEnumerator ScrollingToIndex(int position, float duration, Action onScrolled)
        {
            var targetPosition = currentPosition + position;
            Debug.Log($"CurrentPos: {currentPosition} / targetPos: {targetPosition}");
            
            _scrollerExtension.SetForceVelocity((position - (10)) / duration);
            yield return new WaitForSeconds(duration);
            _scrollerExtension.SetForceVelocity(3.5f);
            yield return new WaitForSeconds(2);
            _scrollerExtension.SetForceVelocity(1);
            yield return new WaitForSeconds(3);

            while (true)
            {
                if (Math.Abs(currentPosition - targetPosition) < 0.1f)
                {
                    _scrollerExtension.SetForceVelocityOff();
                    StartCoroutine(WaitingEndScrolling(onScrolled));
                    break;
                }
                Debug.Log("Докрутка");
                _scrollerExtension.SetForceVelocity(1f);
                yield return new WaitForSeconds(0.05f);
            }
        }

        private IEnumerator WaitingEndScrolling(Action onScrolled)
        {
            yield return new WaitForSeconds(0.4f);
            onScrolled?.Invoke();
        }
    }
}