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
            var s = PlayerPrefs.GetInt("HMShopsLevel" + level.gameObject.name);
            Debug.Log(s);
            while (s >1 && k == 1)
            {                
                s--;
                LoadCasualUpgrade();
            }
            while(s > 1 && k == 2)
            {
                LoadSurprise();
                s--;
            }
            //level.SaveLevel(_levelShopKey + k);
            k--;
        }
    }

    private void CasualUpgrade()
    {
        _playerProfile.percentUpgrade += _levelShop[0].CasualUpgrade(_playerProfile);
        _levelShop[0].UpdateLevelText();
    }

    private void LoadCasualUpgrade()
    {
        _playerProfile.percentUpgrade += _levelShop[0].LoadCasualUpgrade(_playerProfile);
        _levelShop[0].UpdateLevelText();
    }

    private void SurpriseButtonPressed()
    {
        CoinType type = _playerProfile.Coins[_levelShop[1].CurrentLevel - 1].ID;

        if (_playerProfile.TryRemoveScore(type, 100) && _levelShop[1].CurrentLevel < 5)
        {
            AdditionalCoins additionalCoins = new AdditionalCoins();
            _levelShop[1].Surprise(additionalCoins, _minerActiveSlotEventsUiController);
            
            _playerProfile.AddScore(type, -100);
            _levelShop[1].UpdateLevelText();
        }        
    }

    private void LoadSurprise()
    {
        AdditionalCoins additionalCoins = new AdditionalCoins();
        _levelShop[1].Surprise(additionalCoins, _minerActiveSlotEventsUiController);
        _levelShop[1].UpdateLevelText();
    }


}
