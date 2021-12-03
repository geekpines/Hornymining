using App.Scripts.Gameplay.CoreGameplay.Mining;
using App.Scripts.Gameplay.CoreGameplay.Player;
using App.Scripts.Gameplay.MetaGameplay.Roulette;
using Sirenix.OdinInspector;
using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace App.Scripts.UiControllers.RouletteScreen
{
    /// <summary>
    /// Отвечает за логику роллинга (визуальную часть) и
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
        public event Action OnStartedRoll;
        public event Action OnFinishedRoll;

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
            _playerProfile.AddMiner(_minerCreatorSystem.CreateMiner(_reward));
            OnFinishedRoll?.Invoke();
        }
    }
}