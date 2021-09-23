using System;
using System.Collections;
using App.Scripts.UiControllers.Commmon;
using App.Scripts.UiViews.RouletteScreen;
using Sirenix.OdinInspector;
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
        [Title("Визуальные настройки ролла")]
        [SerializeField, Tooltip("Количество переключений ячеек до момента определения выдачи результата")]
        private int _rollLenght = 50;
        private float _rollTime = 5f;
        
        [SerializeField] private SpecialScrollMinerView _scrollMinerView = default;
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
            
        }


    }
}