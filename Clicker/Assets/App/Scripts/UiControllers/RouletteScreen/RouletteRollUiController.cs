using System.Linq;
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
        [Inject]
        private PlayerProfile _playerProfile;
        
        [Title("Визуальные настройки ролла")]
        [SerializeField, Tooltip("Количество полных оборотов перед тем, как выдать результат")]
        private int _countRollLoop = 10;
        [SerializeField] private float _rollTime = 5f;

        [Title("Ссылки на контроллеры")]
        [SerializeField] private RollMinerController _rollMinerController;
        [SerializeField] private RouletteSlotsUiController _rouletteSlotsUiController;
        [SerializeField] private SpecialScrollMinerView _scrollMinerView = default;
        
        [Title("Элементы Ui")]
        [SerializeField] private Button _rollButton;
        
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
            var reward = _rollMinerController.RollItem();
            Debug.Log($"Целевой конфиг: {reward.Name} / Хэш: {reward.GetHashCode()}");
            _scrollMinerView.ScrollTo(
                _countRollLoop * _rollMinerController.Configuration.RouletteItems.Count, 
                _rollTime, 
                OnScrollFinished,
                reward);
        }

        private void OnScrollFinished()
        {
            
        }
    }
}