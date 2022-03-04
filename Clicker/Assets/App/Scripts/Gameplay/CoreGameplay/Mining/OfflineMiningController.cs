using App.Scripts.Gameplay.CoreGameplay.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class OfflineMiningController : MonoBehaviour
{
    
    [SerializeField] private Button _levelUpOfflineMiner;
    
    private PlayerProfile _playerProfile;
    private LevelShopUpgrades _level = new LevelShopUpgrades();
    private OfflineMining _offlineMining = new OfflineMining();

    [Inject]
    private void Construct(PlayerProfile playerProfile)
    {
        _playerProfile = playerProfile;
    }


    void Start()
    {
        
        _levelUpOfflineMiner.onClick.AddListener(LevelUp);
        int k = _level.CurrentLevel - 1;
        while (k >= 0)
        {
            _offlineMining.AddScore(_playerProfile, _playerProfile.Coins[k].ID);
            k--;
        }
        
    }
    void LevelUp()
    {
        if(_playerProfile.TryRemoveScore(_playerProfile.Coins[_level.CurrentLevel].ID, 100))
        {
            _level.LevelUp();
            _playerProfile.AddScore(_playerProfile.Coins[_level.CurrentLevel].ID, -100);
        }
        
    }

    
}
