using System;
using System.Collections;
using App.Scripts.UiControllers.Commmon;
using App.Scripts.UiViews.RouletteScreen;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.UiControllers.RouletteScreen
{
    /// <summary>
    /// Отвечает за визуализацию процесса роллинга и
    /// показ соответствующей анимации
    /// </summary>
    public class RouletteRollUiController : MonoBehaviour
    {
        [SerializeField] private SpecialScrollMinerView _scrollMinerView = default;
        [SerializeField] private Button _rollButton;
        [SerializeField, Tooltip("Количество переключений ячеек до момента выдачи результата")] 
        private int _rollDuration = 50;

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
            
        }


    }
}