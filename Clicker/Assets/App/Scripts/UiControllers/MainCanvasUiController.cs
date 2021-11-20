using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.UiControllers
{
    //todo: Заменить на дузи?
    public class MainCanvasUiController : MonoBehaviour
    {
        [SerializeField] private GameObject _gameScreen;
        [SerializeField] private GameObject _rouletteScreen;
        [SerializeField] private GameObject _shopScreen;
        [SerializeField] private GameObject _stockPanel;
        [SerializeField] private GameObject _upgradePanel;

        [SerializeField] private Button _openRouletteScreenButton;
        [SerializeField] private Button _backRouletteScreenButton;

        [SerializeField] private Button _openStockButton;
        [SerializeField] private Button _openStockButton1;
        [SerializeField] private Button _backStockButton;

        [SerializeField] private Button _openUpgradeButton;
        [SerializeField] private Button _openUpgradeButton1;
        [SerializeField] private Button _backUpgradeButton;

        private void OnEnable()
        {
            _openRouletteScreenButton.onClick.AddListener(ShowRouletteScreen);
            _backRouletteScreenButton.onClick.AddListener(ShowGameScreen);

            //открытие биржи и апгрейда из Gamescreen
            _openStockButton.onClick.AddListener(ShowStockScreen);
            _openUpgradeButton.onClick.AddListener(ShowUpgradeScreen);

            //открытие биржи и апгрейда из Shopscreen
            _openStockButton1.onClick.AddListener(ShowStockScreen);
            _openUpgradeButton1.onClick.AddListener(ShowUpgradeScreen);

            //закрытие биржи и апгрейда
            _backStockButton.onClick.AddListener(ShowGameScreen);
            _backUpgradeButton.onClick.AddListener(ShowGameScreen);
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
            _shopScreen.SetActive(false);
            _openStockButton.gameObject.SetActive(true);
            _openUpgradeButton.gameObject.SetActive(true);
        }

        private void ShowStockScreen()
        {
            _shopScreen.SetActive(true);
            _stockPanel.SetActive(true);
            _upgradePanel.SetActive(false);

            _openStockButton.gameObject.SetActive(false);
            _openUpgradeButton.gameObject.SetActive(false);
            
        }

        private void ShowUpgradeScreen()
        {
            _shopScreen.SetActive(true);
            _upgradePanel.SetActive(true);
            _stockPanel.SetActive(false);

            _openStockButton.gameObject.SetActive(false);
            _openUpgradeButton.gameObject.SetActive(false);
        }

    }
}