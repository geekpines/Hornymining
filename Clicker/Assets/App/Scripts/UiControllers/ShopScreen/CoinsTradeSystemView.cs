using App.Scripts.Gameplay.CoreGameplay.Coins;
using App.Scripts.Gameplay.CoreGameplay.Player;
using App.Scripts.UiControllers.GameScreen.ScorePanel;
using App.Scripts.UiViews.GameScreen.TopPanel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class CoinsTradeSystemView : MonoBehaviour
{
    private PlayerProfile _player;
    [SerializeField] private CoinInfoView _coinInfoView;
    [SerializeField] private Button _buyButton;
    [SerializeField] private Button _sellButton;

    [SerializeField] private CoinsPanelInformation panelInformation;
    


    [Inject]
    private void Construct(PlayerProfile player)
    {
        _player = player;
    }

    private void Awake()
    {
        _buyButton.onClick.AddListener(Buy);
        _sellButton.onClick.AddListener(Sell);
    }

    void Buy()
    {
        for (int i = 0; i < panelInformation.CoinInfoViews.Count; i++)
        {
            if(panelInformation.CoinInfoViews[i] == _coinInfoView)
            {
                if (_player.Coins[5].Value - 1 >= 0)
                {
                    _player.AddScore(_player.Coins[i].ID, _player.Coins[i].TradeValue);
                    _player.AddScore(CoinType.HornyBucks, -1);
                }
                
            }
        }        
       
    }
    void Sell()
    {

        for (int i = 0; i < panelInformation.CoinInfoViews.Count; i++)
        {
            if (panelInformation.CoinInfoViews[i] == _coinInfoView)
            {
                if (_player.Coins[i].Value - _player.Coins[i].TradeValue >= 0)
                {
                    _player.AddScore(_player.Coins[i].ID, -_player.Coins[i].TradeValue);
                    _player.AddScore(CoinType.HornyBucks, 1);
                }
               
            }
        }

    }
}
