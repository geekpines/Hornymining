using System;
using App.Scripts.Gameplay.Roulette;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Gameplay.Expiriments
{
    public class RouletteTest : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private RouletteObject _roulette;
        
        private void OnEnable()
        {
            _button.onClick.AddListener(Clicked);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(Clicked);
        }

        private void Clicked()
        {
            var result = _roulette.RollItem();
            if (result != null)
            {
                //Debug.Log($"Result: {result.Name}");
            }
        }
    }
}