using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace App.Scripts.UiControllers.GameScreen.MinerInformationPanel
{
    public class MinerInformationPanelAnimation : MonoBehaviour
    {
        [SerializeField] private MinerInformationPanelUiController _panelUiController;

        [Title("Настройки анимации")]
        [SerializeField] private int _distanceSideHideElementPixels = 800;
        [SerializeField] private float _animationTime = 1;

        [Title("Элементы панели")]
        [SerializeField] private GameObject _rootPanel;
        [SerializeField] private CanvasGroup _backgroundBlockRaycast;
        [SerializeField] private GameObject _informationPanel;
        [SerializeField] private Transform _minerRootPosition;

        private void OnEnable()
        {
            _panelUiController.OnShowPanel += PlayShowAnimation;
            _panelUiController.OnHidePanel += PlayHideAnimation;
        }

        private void OnDisable()
        {
            _panelUiController.OnShowPanel -= PlayShowAnimation;
            _panelUiController.OnHidePanel -= PlayHideAnimation;
        }

        private void PlayShowAnimation()
        {
            _backgroundBlockRaycast.alpha = 0;

            DOTween.To(
                () => _backgroundBlockRaycast.alpha,
                x => _backgroundBlockRaycast.alpha = x,
                1f, _animationTime);

            _informationPanel.transform.position = new Vector3(
                _informationPanel.transform.position.x + _distanceSideHideElementPixels,
                _informationPanel.transform.position.y,
                _informationPanel.transform.position.z);
            _informationPanel.transform.DOMove(new Vector3(
                _informationPanel.transform.position.x - _distanceSideHideElementPixels,
                _informationPanel.transform.position.y), _animationTime);

            _minerRootPosition.transform.position = new Vector3(
                _minerRootPosition.transform.position.x - _distanceSideHideElementPixels,
                _minerRootPosition.transform.position.y,
                _minerRootPosition.transform.position.z);
            _minerRootPosition.transform.DOMove(new Vector3(
                _minerRootPosition.transform.position.x + _distanceSideHideElementPixels,
                _minerRootPosition.transform.position.y), _animationTime);

            _rootPanel.SetActive(true);
        }

        private void PlayHideAnimation()
        {
            _rootPanel.SetActive(false);
        }
    }
}