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
    [SerializeField] List<LevelShopUpgrades> levelShop;
    [SerializeField] MinerActiveSlotsEventsUiController _minerActiveSlotEventsUiController;
    [SerializeField] private TextMeshProUGUI _leveltext;

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

        int k = levelShop.Count;
        foreach(var level in levelShop)
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
        
        _playerProfile.percentUpgrade += levelShop[0].CasualUpgrade(_playerProfile);
        Debug.Log(_playerProfile.percentUpgrade);
        _leveltext.text = "Level: " + levelShop[0].CurrentLevel;
    }

    private void SurpriseButtonPressed()
    {
        AdditionalCoins additionalCoins = new AdditionalCoins();
        if(!_playerProfile.TryRemoveScore(_playerProfile.Coins[levelShop[1].CurrentLevel].ID, 100))
        {
            levelShop[1].Surprise(additionalCoins, _minerActiveSlotEventsUiController);
            _playerProfile.AddScore(_playerProfile.Coins[levelShop[1].CurrentLevel].ID, -100);
            _leveltext.text = "Level: " + levelShop[1].CurrentLevel;
        }        
    }


}
