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


        private void OnEnable()
        {
            _openRouletteScreenButton.onClick.AddListener(ShowRouletteScreen);
        }

        private void OnDisable()
        {
            _openRouletteScreenButton.onClick.RemoveListener(ShowRouletteScreen);
        }

        private void ShowRouletteScreen()
        {
            _gameScreen.SetActive(false);
            _rouletteScreen.SetActive(true);
        }
    }
}