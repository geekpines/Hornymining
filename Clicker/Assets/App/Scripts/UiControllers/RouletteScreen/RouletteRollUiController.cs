using System;
using System.Linq;
using App.Scripts.Gameplay.CoreGameplay.Mining;
using App.Scripts.Gameplay.CoreGameplay.Player;
using App.Scripts.Gameplay.MetaGameplay.Roulette;
using App.Scripts.UiControllers.Commmon;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace App.Scripts.UiControllers.RouletteScreen
{
    /// <summary>
    /// Отвечает логику роллинга (визуальную часть) и
    /// получение награды игроком
    /// </summary>
    public class RouletteRollUiController : MonoBehaviour
    {
        [Inject] private PlayerProfile _playerProfile;
        [Inject] private MinerCreatorSystem _minerCreatorSystem;
        
        [Title("Визуальные настройки ролла")]
        [SerializeField, Range(3, 8)] private int _rollDuration = 5;
        private const int PreferCountRollPerSecond = 10;
        
        [Title("Ссылки на контроллеры")]
        [SerializeField] private RollMinerSystem _rollMinerSystem;
        [SerializeField] private SpecialScrollMinerView _scrollMinerView = default;
        
        [Title("Элементы Ui")]
        [SerializeField] private Button _rollButton;

        private MinerConfiguration _reward;
        public Action OnStartedRoll;
        public Action OnFinishedRoll;
        
        private void OnEnable()
        {
            _rollButton.onClick.AddListener(Roll);
        }

        private void OnDisable()
        {
            _rollButton.onClick.RemoveListener(Roll);
        }
        
        private void Roll()
        {
            OnStartedRoll?.Invoke();
            _reward = _rollMinerSystem.RollItem();
            Debug.Log($"Сгенерированная награда: {_reward.Name.GetLocalizedString()}");
            _scrollMinerView.ScrollTo(
                CalculateCountRoll(_rollDuration, _rollMinerSystem.Configuration.RouletteItems.Count), 
                _rollDuration, 
                OnScrollFinished,
                _reward);
        }

        private int CalculateCountRoll(int duration, int size)
        {
            var countRoll = duration * PreferCountRollPerSecond;
            return countRoll + (countRoll % size);
        }

        private void OnScrollFinished()
        {
            //todo: пока нет системы грейдов
            _playerProfile.Miners.Add(_minerCreatorSystem.CreateMiner(_reward, 3));
            OnFinishedRoll?.Invoke();
        }
    }
}