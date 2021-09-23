using System.Collections.Generic;
using App.Scripts.Gameplay.CoreGameplay.Coins;
using App.Scripts.Gameplay.CoreGameplay.Coins.Static;
using App.Scripts.Gameplay.CoreGameplay.Player;
using App.Scripts.UiViews.GameScreen.TopPanel;
using Assets.App.Scripts.Common;
using UnityEngine;
using Zenject;

namespace App.Scripts.UiControllers.GameScreen
{
    /// <summary>
    /// Отображает инофрмацию о текущем количестве валюты у игрока
    /// </summary>
    public class CoinsUiController : AbstractService<CoinsUiController>
    {
        [SerializeField] private TopPanelController _topPanelController;
        private PlayerProfile _player;
        private Dictionary<CoinType, CoinInfoView> _iDtoCoinView = new Dictionary<CoinType, CoinInfoView>();
        
        [Inject]
        private void Construct(PlayerProfile player)
        {
            _player = player;
        }

        private void Awake()
        {
            ShowPlayerResources();
        }

        private void ShowPlayerResources()
        {
            _iDtoCoinView.Clear();
            if (_player.Coins.Count <= _topPanelController.CoinInfoViews.Count)
            {
                for (int i = 0; i < _topPanelController.CoinInfoViews.Count; i++)
                {
                    if (_player.Coins.Count >= i)
                    {
                        var coinInfo = CoinsInformation.GetCoinInformation(_player.Coins[i].ID);
                        _topPanelController.CoinInfoViews[i].SetCoinInformation(
                            coinInfo.Icon, _player.Coins[i].Value, coinInfo.Description);
                        _iDtoCoinView.Add(_player.Coins[i].ID, _topPanelController.CoinInfoViews[i]);
                    }
                    else
                    {
                        _topPanelController.CoinInfoViews[i].gameObject.SetActive(false);
                    }
                }
            }
            else
            {
                Debug.LogError("Элементов валюты на сцене меньше, чем существует в игре!");
            }
        }
        
        protected override void OnEnable()
        {
            foreach (var coin in _player.Coins)
            {
                coin.OnChanged += ChangeCoinValue;
            }
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            foreach (var coin in _player.Coins)
            {
                coin.OnChanged -= ChangeCoinValue;
            }
        }

        private void ChangeCoinValue(CoinType id, float newValue)
        {
            _iDtoCoinView[id].SetValue(newValue);
        }
    }
}