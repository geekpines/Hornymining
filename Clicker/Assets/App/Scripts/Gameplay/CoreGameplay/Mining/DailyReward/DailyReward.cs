using App.Scripts.Gameplay.CoreGameplay.Coins;
using App.Scripts.Gameplay.CoreGameplay.Mining;
using App.Scripts.Gameplay.CoreGameplay.Player;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Zenject;
using NiobiumStudios;

public class DailyReward : MonoBehaviour
{

    [SerializeField] MinerConfiguration minerConfiguration;
    private PlayerProfile _playerProfile;

    public event Action<int> dayLeft;


    [Inject]
    private void Construct(PlayerProfile playerProfile)
    {
        _playerProfile = playerProfile;
    }

    void Start()
    {
        DailyRewards.instance.onClaimPrize += Reward;
    }
    
    void MiningUp(float added)
    {
        _playerProfile.percentUpgrade += added;
    }

    void MinerUp()
    {
        _playerProfile.GetAllMiners()[UnityEngine.Random.Range(0, _playerProfile.GetAllMiners().Count)].LevelUp();
    }

    void AddExtraMiner()
    {
        MinerCreatorSystem minerCreator = new MinerCreatorSystem();
        _playerProfile.AddMiner(minerCreator.CreateMiner(minerConfiguration));
    }

    void AddCoin(CoinType coinType, float count)
    {
        _playerProfile.AddScore(coinType, count);
    }

    void Reward(int day)
    {
        switch (day)
        {
            case 1:
                MiningUp(1.5f);
                break;
            case 2:
                AddCoin(CoinType.Tokken, 15);
                break;
            case 3:
                MinerUp();
                break;
            case 4:
                AddCoin(CoinType.Usdfork, 15);
                break;
            case 5:
                MiningUp(1.5f);
                break;
            case 6:
                AddCoin(CoinType.LTC, 15);
                break;
            case 7:
                AddExtraMiner();
                break;
            default:
                break;
        }
        
    }
}
