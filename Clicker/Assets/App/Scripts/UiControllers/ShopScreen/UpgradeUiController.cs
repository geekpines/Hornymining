using App.Scripts.Gameplay.CoreGameplay.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UpgradeUiController : MonoBehaviour
{
    [SerializeField] private Button _upgradeButton;
    [SerializeField] private Button _openSlotButton;
    [SerializeField] private Button _surpriseButton;
    [SerializeField] List<LevelShopUpgrades> levelShop;



    private PlayerProfile _playerProfile;


    [Inject]
    private void Construct(PlayerProfile playerProfile)
    {
        _playerProfile = playerProfile;
    }

    private void Awake()
    {
        _upgradeButton.onClick.AddListener(CasualUpgrade);
    }

    private void CasualUpgrade()
    {

        _playerProfile.percentUpgrade = levelShop[0].CasualUpgrade(_playerProfile);
        Debug.Log(_playerProfile.percentUpgrade);

    }


}
