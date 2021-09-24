using System;
using System.Collections;
using System.Collections.Generic;
using App.Scripts.Gameplay.CoreGameplay.Mining;
using App.Scripts.UiViews.RouletteScreen;
using UnityEngine;
using UnityEngine.UI.Extensions;
using UnityEngine.UI.Extensions.Examples.FancyScrollViewExample03;
using Ease = UnityEngine.UI.Extensions.EasingCore.Ease;

namespace App.Scripts.UiControllers.Commmon
{
    /// <summary>
    /// Доработанный контроллер скроллинга ячеек
    /// </summary>
    public class SpecialScrollMinerView : FancyScrollView<ItemData, Context>
    {
        [SerializeField] private ScrollerExtension _scrollerExtension = default;
        [SerializeField] private GameObject _cellPrefab = default;
        
        /// <summary>
        /// Список визуальных ячеек
        /// </summary>
        public List<RouletteSlotView> MinerViews { get; private set; } = new List<RouletteSlotView>();
        
        /// <summary>
        /// Количество визуальных ячеек
        /// </summary>
        public int Size => ItemsSource.Count;
        
        /// <summary>
        /// Текущая выбранная ячейка
        /// </summary>
        public RouletteSlotView CurrentSelected => MinerViews[CurrentSelectedIndex];
        
        /// <summary>
        /// Индекс текущей выбранной ячейки
        /// </summary>
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

        /// <summary>
        /// Обновить информацию о ячейках
        /// </summary>
        /// <param name="items"></param>
        public void UpdateData(IList<ItemData> items)
        {
            UpdateContents(items);
            _scrollerExtension.SetTotalCount(items.Count);
        }

        /// <summary>
        /// Выбрать ячейку по индексу
        /// </summary>
        /// <param name="index"></param>
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
        /// <param name="offSetCells">На сколько позиций сдвинуть (желательно более 15!)</param>
        /// <param name="duration">Время, за которое сделать сдвиг</param>
        /// <param name="onScrolled"></param>
        public void ScrollTo(int offSetCells, float duration, Action onScrolled,
            MinerConfiguration targetConfiguration)
        {
            Debug.Log($"Целевой конфиг в рулетке: {targetConfiguration.Name.GetLocalizedString()}");
            StartCoroutine(ScrollingToIndex(offSetCells, duration, onScrolled, targetConfiguration));
        }

        /// <summary>
        /// Корутина содержит всю логику скроллинга (включая остановку).
        /// </summary>
        /// <param name="position"></param>
        /// <param name="duration"></param>
        /// <param name="onScrolled"></param>
        /// <returns></returns>
        private IEnumerator ScrollingToIndex(int position, float duration, Action onScrolled,
            MinerConfiguration targetConfiguration)
        {
            _scrollerExtension.SetForceVelocity((position - (10)) / duration);
            yield return new WaitForSeconds(duration);
            _scrollerExtension.SetForceVelocity(3.5f);
            yield return new WaitForSeconds(2);
            _scrollerExtension.SetForceVelocity(1);
            yield return new WaitForSeconds(3);

            while (true)
            {
                if (targetConfiguration != null)
                {
                    if (targetConfiguration.GetInstanceID() == CurrentSelected.ConfigHash)
                    {
                        Debug.Log($"Докручено до: {targetConfiguration.Name.GetLocalizedString()}");
                        _scrollerExtension.SetForceVelocityOff();
                        StartCoroutine(WaitingEndScrolling(onScrolled));
                        break;
                    }
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