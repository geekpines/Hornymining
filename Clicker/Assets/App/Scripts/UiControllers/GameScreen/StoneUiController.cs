using App.Scripts.Gameplay.CoreGameplay.Coins;
using App.Scripts.Gameplay.CoreGameplay.Coins.Static;
using App.Scripts.Gameplay.CoreGameplay.Coins.Upgrades;
using App.Scripts.Gameplay.CoreGameplay.Player;
using Assets.App.Scripts.Common;
using UnityEngine;
using Zenject;

namespace App.Scripts.UiControllers.GameScreen
{
    /// <summary>
    /// Добавляет игроку количество валюты за нажатие
    /// по майнеру
    /// </summary>
    public class StoneUiController : AbstractService<StoneUiController>
    {
        [SerializeField] private StoneAnimationScore _stoneAnimationScore;
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
            _stoneAnimationScore.OnClicked += StoneClicked;
            foreach (var coin in _player.Coins)
            {
                coin.OnChangeCount += ChangeCoinValue;
            }
        }
        
        protected override void OnDisable()
        {
            base.OnDisable();
            _stoneAnimationScore.OnClicked -= StoneClicked;
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
            _stoneAnimationScore.ShowScoreLine(coinIcon, changeCount);
        }
    }
}