using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.UiControllers
{
    //todo: Заменить на дузи?
    public class MainCanvasUiController : MonoBehaviour
    {
        [SerializeField] private GameObject _gameScreen;
        [SerializeField] private GameObject _rouletteScreen;

        [SerializeField] private Button _openRouletteScreenButton;
        [SerializeField] private Button _backRouletteScreenButton;

        private void OnEnable()
        {
            _openRouletteScreenButton.onClick.AddListener(ShowRouletteScreen);
            _backRouletteScreenButton.onClick.AddListener(ShowGameScreen);
        }

        private void OnDisable()
        {
            _openRouletteScreenButton.onClick.RemoveListener(ShowRouletteScreen);
            _backRouletteScreenButton.onClick.RemoveListener(ShowGameScreen);
        }

        private void ShowRouletteScreen()
        {
            _gameScreen.SetActive(false);
            _rouletteScreen.SetActive(true);
        }

        private void ShowGameScreen()
        {
            _gameScreen.SetActive(true);
            _rouletteScreen.SetActive(false);
        }
        
    }
}