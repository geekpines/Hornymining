using App.Scripts.Gameplay.CoreGameplay.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class OfflineMiningController : MonoBehaviour
{
    [Header ("Ёлементы Upgrade")]
    [SerializeField] private LevelShopUpgrades _level;
    [SerializeField] private Button _levelUpOfflineMiner;
    
    private PlayerProfile _playerProfile;
    private OfflineMining _offlineMining = new OfflineMining();

    [Inject]
    private void Construct(PlayerProfile playerProfile)
    {
        _playerProfile = playerProfile;
    }


    private void Start()
    {
        
        _levelUpOfflineMiner.onClick.AddListener(LevelUp);
        int k = _level.LoadLevel(gameObject.name)-1;
        while (k >= 0)
        {
            _offlineMining.AddScore(_playerProfile, _playerProfile.Coins[k].ID);
            k--;
        }
        
    }
    private void LevelUp()
    {
        if(_playerProfile.TryRemoveScore(_playerProfile.Coins[_level.CurrentLevel-1].ID, 100) && _level.CurrentLevel < 5)
        {
            _level.LevelUp();
            _level.UpdateLevelText();
            _playerProfile.AddScore(_playerProfile.Coins[_level.CurrentLevel - 1 ].ID, -100);
        }
    }

    private void LoadLevel()
    {
        _level.LevelUp();
        _level.UpdateLevelText();
    }

    
}
