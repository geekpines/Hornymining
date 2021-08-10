using System;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.UiControllers.GameScreen
{
    //todo: Заменить на дузи?
    public class MainCanvasUiController : MonoBehaviour
    {
        public GameObject GameScreen;
        public GameObject RouletteScreen;

        public Button OpenRouletteScreenButton;


        private void OnEnable()
        {
            OpenRouletteScreenButton.onClick.AddListener(ShowRouletteScreen);
        }

        private void OnDisable()
        {
            OpenRouletteScreenButton.onClick.RemoveListener(ShowRouletteScreen);
        }

        private void ShowRouletteScreen()
        {
            GameScreen.SetActive(false);
            RouletteScreen.SetActive(true);
        }
    }
}