using System;
using App.Scripts.UiViews.GameScreen;
using UnityEngine;

namespace App.Scripts.UiControllers.GameScreen
{
    public class StoneAnimationScore : MonoBehaviour
    {
        public Action OnClicked;
        [SerializeField] private StoneCoinsView _stoneCoinsView;

        public void ShowScoreLine(Sprite icon, float score)
        {
            _stoneCoinsView.ShowScoreAnimation(icon, score);
        }

        private void OnEnable()
        {
            _stoneCoinsView.OnPressDown += StoneClicked;
        }

        private void OnDisable()
        {
            _stoneCoinsView.OnPressDown -= StoneClicked;
        }

        private void StoneClicked(StoneCoinsView obj)
        {
            OnClicked?.Invoke();
        }
    }
}