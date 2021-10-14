using System.Collections.Generic;
using App.Scripts.UiViews.GameScreen.TopPanel;
using App.Scripts.Utilities.MonoBehaviours;
using UnityEngine;

namespace App.Scripts.UiControllers.GameScreen.MinerInformationPanel
{
    public class CostViewPool : MonoBehaviour
    {
        [SerializeField] private CoinInfoView _viewPrefab;
        [SerializeField] private int _poolSize = 5;
        private PoolObject<CoinInfoView> _coinViewPool;

        public class CoinInformation
        {
            public readonly Sprite Icon;
            public readonly float Value;

            public CoinInformation(Sprite icon, float value)
            {
                Icon = icon;
                Value = value;
            }
        }
        
        private void Awake()
        {
            InitializationPool();
        }

        private void InitializationPool()
        {
            if (_coinViewPool == null)
            {
                _coinViewPool = new PoolObject<CoinInfoView>(
                    _viewPrefab,
                    _poolSize,
                    this.transform,
                    true);
            }
        }

        public void ShowCosts(List<CoinInformation> coinInfos)
        {
            if (_coinViewPool == null)
                InitializationPool();
            _coinViewPool.ReturnAll();
            foreach (var coinInformation in coinInfos)
            {
                var coinView = _coinViewPool.GetObject();
                coinView.SetIcon(coinInformation.Icon);
                coinView.SetValue(coinInformation.Value);
            }
        }
    }
}