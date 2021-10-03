using System;
using System.Collections.Generic;
using App.Scripts.UiViews.GameScreen.TopPanel;
using UnityEngine;

namespace App.Scripts.UiControllers.GameScreen.ScorePanel
{
    /// <summary>
    /// Контроллер панели добытых ресурсов
    /// </summary>
    public class ScorePanelUiController : MonoBehaviour
    {
        public Action<CoinInfoView> OnStartHolderCoin;
        public Action<CoinInfoView> OnEndHolderCoin;

        [field: SerializeField]
        public List<CoinInfoView> CoinInfoViews { get; private set; } = new List<CoinInfoView>();

        private void OnEnable()
        {
            foreach (var coinInfoView in CoinInfoViews)
            {
                coinInfoView.OnStartHolder += StartHolder;
                coinInfoView.OnEndHolder += EndHolder;
            }
        }

        private void OnDisable()
        {
            foreach (var coinInfoView in CoinInfoViews)
            {
                coinInfoView.OnStartHolder -= StartHolder;
                coinInfoView.OnEndHolder -= EndHolder;
            }
        }

        private void StartHolder(CoinInfoView coinInfoView)
        {
            EnableDescription(coinInfoView, true);
            OnStartHolderCoin?.Invoke(coinInfoView);
        }

        private void EndHolder(CoinInfoView coinInfoView)
        {
            EnableDescription(coinInfoView, false);
            OnEndHolderCoin?.Invoke(coinInfoView);
        }

        private void EnableDescription(CoinInfoView view, bool show)
        {
            view.EnableDescription(show);
        }
    }
}