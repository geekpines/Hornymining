using App.Scripts.Gameplay.CoreGameplay.Coins;
using App.Scripts.Gameplay.CoreGameplay.Player;
using App.Scripts.UiControllers.GameScreen.MinersPanel;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UpgradeUiController : MonoBehaviour
{
    [Title ("Events")]
    [SerializeField] private UpgradeEvents _upgradeEvents;

    [Title ("Кнопки")]
    [SerializeField] private Button _upgradeButton;
    [SerializeField] private Button _openSlotButton;
    [SerializeField] private Button _surpriseButton;
    [SerializeField] private Button _openStockButton;

    [Title ("Список Улучшений")]
    [SerializeField] private List<LevelShopUpgrades> _levelShop;
    //[SerializeField] private MinerActiveSlotsEventsUiController _minerActiveSlotEventsUiController;

    private string _levelShopKey = "levelShop";

    private PlayerProfile _playerProfile;


    [Inject]
    private void Construct(PlayerProfile playerProfile)
    {
        _playerProfile = playerProfile;
    }

    private void OnEnable()
    {
        SetListeners();
    }

    private void OnDisable()
    {
        RemoveListeners();
    }

    private void OnDestroy()
    {
        RemoveListeners();
    }

    private void SetListeners()
    {
        _upgradeButton.onClick.AddListener(UpgradeClicked);
        _openSlotButton.onClick.AddListener(OpenSlotClicked);
        _surpriseButton.onClick.AddListener(SurpriseClicked);
        _openStockButton.onClick.AddListener(OpenStockClicked);
    }

    private void RemoveListeners()
    {
        _upgradeButton?.onClick.RemoveListener(UpgradeClicked);
        _openSlotButton.onClick.RemoveListener(OpenSlotClicked);
        _surpriseButton.onClick.RemoveListener(SurpriseClicked);
        _openStockButton.onClick.RemoveListener(OpenStockClicked);
        
    }

    private void UpgradeClicked()
    {
        LevelShopUpgrades casualUpgrade = _levelShop[0];
        
        _playerProfile.percentUpgrade += casualUpgrade.CasualUpgrade(_playerProfile);
    }

    private void OpenSlotClicked()
    {
        LevelShopUpgrades openMinerSlot = _levelShop[1];

        _upgradeEvents.MinerSlotOppened(openMinerSlot.OpenMinerSlot(_playerProfile));
    }

    private void SurpriseClicked()
    {
        LevelShopUpgrades surpriseUpgrade = _levelShop[2];
        AdditionalCoins additionalCoins = new AdditionalCoins();
        
        _upgradeEvents.SurpriseUpgrade(surpriseUpgrade.Surprise(additionalCoins));
    }

    private void OpenStockClicked()
    {
        LevelShopUpgrades openStock = _levelShop[3];
        if (true)
        {
            _upgradeEvents.StockOpened(openStock.OpenStock(_playerProfile) != 0, openStock.CurrentLevel - 1);
        }
    }
}
