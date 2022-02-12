using App.Scripts.Gameplay.CoreGameplay.Mining;
using App.Scripts.Gameplay.CoreGameplay.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class RouletteRewardController : MonoBehaviour
{
    [SerializeField] private GameObject _rewardMinerWindow;
    [SerializeField] private GameObject _wheelContainer;
    [SerializeField] private Transform RewardMinerPosition;

    private PlayerProfile _playerProfile;

    [Inject]
    private void Construct(PlayerProfile playerProfile)
    {
        _playerProfile = playerProfile;
    }

    void Start()
    {
        _playerProfile.OnAllMinersCountChanged += AddMinerToRewardPosition;
    }

    void AddMinerToRewardPosition(Miner miner)
    {        
        _wheelContainer.SetActive(false);
        _rewardMinerWindow.SetActive(true);
        Instantiate(miner.Configuration.Visual, RewardMinerPosition);
    }

    
}
