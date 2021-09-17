using App.Scripts.Gameplay.CoreGameplay.Coins;
using App.Scripts.Gameplay.CoreGameplay.Coins.Static;
using App.Scripts.Gameplay.CoreGameplay.Coins.Upgrades;
using App.Scripts.Gameplay.CoreGameplay.Player;
using App.Scripts.UiControllers.GameScreen;
using Assets.App.Scripts.Common;
using UnityEngine;
using Zenject;

namespace App.Scripts.Gameplay.MetaGameplay
{
    /// <summary>
    /// Добавляет игроку количество валюты за нажатие
    /// по майнеру
    /// </summary>
    public class StoneInstaller : AbstractService<StoneInstaller>
    {
        //todo: перенести эту логику в UiControllers
        [SerializeField] private StoneController _stoneController;
        private PlayerProfile _player;
        private CoinsChanceLevel _coinsChanceLevel;

        [Inject]
        private void Construct(PlayerProfile player, CoinsChanceLevel coinsChanceLevel)
        {
            _player = player;
            _coinsChanceLevel = coinsChanceLevel;
        }

        protected override void OnEnable()
        {
            _stoneController.OnClicked += StoneClicked;
            foreach (var coin in _player.Coins)
            {
                coin.OnChangeCount += ChangeCoinValue;
            }
        }
        
        protected override void OnDisable()
        {
            base.OnDisable();
            _stoneController.OnClicked -= StoneClicked;
            foreach (var coin in _player.Coins)
            {
                coin.OnChangeCount -= ChangeCoinValue;
            }
        }
        
        private void StoneClicked()
        {
            CalculateScore();
        }

        private void CalculateScore()
        {
            var randomValue = Random.Range(0, 100f);
            int index = FindCoinIndex(randomValue);
            if (index != -1)
            {
                _player.Coins[index].Add(1f);
            }
            else
            {
                Debug.LogError($"В таблице шансов выпадения валют уровня {_player.CoinLevelChance} " +
                               $"не правильная сумма вероятностей! Получено недопустимое значение!");
            }
        }

        private int FindCoinIndex(float randomValue)
        {
            float range = 0;
            int index = 0;
            foreach (var coinInfo in _coinsChanceLevel.Levels[_player.CoinLevelChance].Coins)
            {
                if (coinInfo.Chance > 0)
                {
                    if (randomValue > range &&
                        randomValue < range + coinInfo.Chance)
                    {
                        return index;
                    }
                    else
                    {
                        range += coinInfo.Chance;
                    }
                }
                index++;
            }
            return -1;
        }
        
        private void ChangeCoinValue(CoinType id, float changeCount)
        {
            var coinIcon = CoinsInformation.GetCoinIcon(id);
            _stoneController.ShowScoreLine(coinIcon, changeCount);
        }
    }
}