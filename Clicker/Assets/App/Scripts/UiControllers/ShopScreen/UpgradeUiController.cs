using App.Scripts.Gameplay.CoreGameplay.Coins;
using App.Scripts.Gameplay.CoreGameplay.Player;
using App.Scripts.UiControllers.GameScreen.MinersPanel;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UpgradeUiController : MonoBehaviour
{
    [SerializeField] private Button _upgradeButton;
    [SerializeField] private Button _surpriseButton;
    [SerializeField] private List<LevelShopUpgrades> _levelShop;
    [SerializeField] private MinerActiveSlotsEventsUiController _minerActiveSlotEventsUiController;

    private string _levelShopKey = "levelShop";

    private PlayerProfile _playerProfile;


    [Inject]
    private void Construct(PlayerProfile playerProfile)
    {
        _playerProfile = playerProfile;
    }

    private void Awake()
    {
        _upgradeButton.onClick.AddListener(CasualUpgrade);
        _surpriseButton.onClick.AddListener(SurpriseButtonPressed);

        int k = _levelShop.Count;
        foreach(var level in _levelShop)
        {            
            var s = level.LoadLevel(_levelShopKey + k);
            while (s != 0 && k == 1)
            {                
                s--;
                CasualUpgrade();
            }
            while(s != 0 && k == 2)
            {
                SurpriseButtonPressed();
                s--;
            }
            level.SaveLevel(_levelShopKey + k);
            k--;
        }
    }

    private void CasualUpgrade()
    {
        
        _playerProfile.percentUpgrade += _levelShop[0].CasualUpgrade(_playerProfile);
        _levelShop[0].UpdateLevelText();
        Debug.Log(_playerProfile.percentUpgrade);
    }

    private void SurpriseButtonPressed()
    {
       
        if(_playerProfile.TryRemoveScore(_playerProfile.Coins[_levelShop[1].CurrentLevel-1].ID, 100) && _levelShop[1].CurrentLevel < 5)
        {
            AdditionalCoins additionalCoins = new AdditionalCoins();
            _levelShop[1].Surprise(additionalCoins, _minerActiveSlotEventsUiController);
            
            _playerProfile.AddScore(_playerProfile.Coins[_levelShop[1].CurrentLevel-1].ID, -100);
            _levelShop[1].UpdateLevelText();
        }        
    }


}
