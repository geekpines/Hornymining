using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.UiControllers.RouletteScreen
{
    /// <summary>
    /// Класс отвечает за анимации во время роллинга и последующего
    /// возращения Ui в начальное состояние
    /// </summary>
    public class RouletteScreenAnimation : MonoBehaviour
    {
        [Title("Настройки анимации")] 
        [SerializeField, Tooltip("Насколько пикселей опустить кнопку ролла после нажатия на нее")] 
        private int _rollButtonHideDownPixels = 300;
        
        [SerializeField, Tooltip("Базовое время анимации между состояниями")] 
        private float _animationTime = 1f;
        
        [Title("Контроллеры")]
        [SerializeField] private RouletteRollUiController _rouletteRollUiController;
        [SerializeField] private SpecialScrollMinerView _specialScrollMiner;

        [Title("Ссылки на визуальные компоненты")] 
        [SerializeField] private GameObject _blockRaycast;
        [SerializeField] private GameObject _rollButton;
        [SerializeField] private GameObject _informationPanel;
        [SerializeField] private CanvasGroup _informationCanvas;
        [SerializeField] private RectTransform _rouletteScroll;
        [SerializeField] private RectTransform _minerRewardPosition;
        
        [Title("Кнопки")] 
        [SerializeField] private Button _getRewardButton;
        [SerializeField] private Button _backButton;

        private Vector2 _minerDefaultPosition;
        private float _cellIntervalDefault;
        
        private void OnEnable()
        {
            _rouletteRollUiController.OnStartedRoll += PlayStartRollAnimation;
            _rouletteRollUiController.OnFinishedRoll += PlayFinishRollAnimation;
            _getRewardButton.onClick.AddListener(PlayResetAnimation);
        }

        private void OnDisable()
        {
            _rouletteRollUiController.OnStartedRoll -= PlayStartRollAnimation;
            _rouletteRollUiController.OnFinishedRoll -= PlayFinishRollAnimation;
            _getRewardButton.onClick.RemoveListener(PlayResetAnimation);
        }

        private void PlayStartRollAnimation()
        {
            _blockRaycast.SetActive(true);

            _rollButton.transform.DOMove(new Vector3(
                _rollButton.transform.position.x,
                _rollButton.transform.position.y - _rollButtonHideDownPixels), _animationTime);
            _backButton.transform.DOMove(new Vector3(
                _backButton.transform.position.x,
                _backButton.transform.position.y + _rollButtonHideDownPixels), _animationTime);
            
            _rouletteScroll.DOScale(new Vector2(1.2f, 1.2f), _animationTime);
        }

        private void PlayFinishRollAnimation()
        {
            _minerDefaultPosition = _rouletteScroll.position;
            _cellIntervalDefault = _specialScrollMiner.CellInterval;
            _informationCanvas.alpha = 0;
            _informationPanel.SetActive(true);
            
            _rouletteScroll.DOScale(new Vector2(1.5f, 1.5f), _animationTime);
            _rouletteScroll.DOMove(_minerRewardPosition.position, _animationTime);
            
            //Изменить расстояние между ячейками
            DOTween.To(
                ()=> _specialScrollMiner.CellInterval, 
                x=> _specialScrollMiner.CellInterval = x, 
                0.5f, _animationTime);
            
            //Изменить прозрачность у панели информации
            DOTween.To(
                ()=> _informationCanvas.alpha, 
                x=> _informationCanvas.alpha = x, 
                1f, _animationTime);
        }

        private void PlayResetAnimation()
        {
            _rollButton.transform.DOMove(new Vector3(
                _rollButton.transform.position.x,
                _rollButton.transform.position.y + _rollButtonHideDownPixels), _animationTime);
            _backButton.transform.DOMove(new Vector3(
                _backButton.transform.position.x,
                _backButton.transform.position.y - _rollButtonHideDownPixels), _animationTime);
            
            _rouletteScroll.DOScale(new Vector2(1f, 1f), _animationTime);
            _rouletteScroll.DOMove(_minerDefaultPosition, _animationTime);
            
            //Восстановить расстояние между ячейками
            DOTween.To(
                ()=> _specialScrollMiner.CellInterval, 
                x=> _specialScrollMiner.CellInterval = x, 
                _cellIntervalDefault, _animationTime)
                .OnComplete(FinishedResetAnimation);
            
            //Спрятать панель информации
            DOTween.To(
                ()=> _informationCanvas.alpha, 
                x=> _informationCanvas.alpha = x, 
                0, _animationTime / 2);
        }

        private void FinishedResetAnimation()
        {
            _informationPanel.SetActive(false);
            _blockRaycast.SetActive(false);
        }
    }
}