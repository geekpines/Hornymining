using App.Scripts.Foundation;
using App.Scripts.UiControllers.GameScreen;
using Assets.App.Scripts.Common;
using UnityEngine;
using Zenject;

namespace Assets.App.Scripts.Gameplay
{
    public class StoneInstaller : AbstractService<StoneInstaller>
    {
        [SerializeField] private StoneController _stoneController;
        private PlayerProfile _player;

        [Inject]
        private void Construct(PlayerProfile player)
        {
            _player = player;
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
            //todo: загрушка рандома выбора валюты
            var randomValue = Random.Range(0, 100f);
            if (randomValue < 10)
            {
                _player.Coins[1].Add(0.1f);
            }
            else
            {
                _player.Coins[0].Add(1);
            }
        }
        
        private void ChangeCoinValue(int id, float changeCount)
        {
            var coinIcon = CoinsInformation.GetCoinIcon(id);
            _stoneController.ShowScoreLine(coinIcon, changeCount);
        }
    }
}