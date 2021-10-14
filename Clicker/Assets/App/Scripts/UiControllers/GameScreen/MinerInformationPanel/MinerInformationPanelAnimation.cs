using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace App.Scripts.UiControllers.GameScreen.MinerInformationPanel
{
    public class MinerInformationPanelAnimation : MonoBehaviour
    {
        [SerializeField] private MinerInformationPanelUiController _panelUiController;
        
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
            _rootPanel.SetActive(true);
        }

        private void PlayHideAnimation()
        {
            _rootPanel.SetActive(false);
        }
    }
}