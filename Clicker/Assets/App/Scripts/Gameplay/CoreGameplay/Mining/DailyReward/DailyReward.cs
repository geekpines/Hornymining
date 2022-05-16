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

    [SerializeField] private MinerConfiguration _WavesCoin;
    [SerializeField] private MinerConfiguration _XRPCoin;
    private PlayerProfile _playerProfile;

    public event Action<int> dayLeft;


    [Inject]
    private void Construct(PlayerProfile playerProfile)
    {
        _playerProfile = playerProfile;

        foreach (var miner in _playerProfile.GetAllMiners())
        {
            if (_WavesCoin == miner.Configuration)
            {
                _WavesCoin = null;
            }
            if (_XRPCoin == miner.Configuration)
            {
                _XRPCoin = null;
            }
        }
    }

    private void Start()
    {
        DailyRewards.instance.onClaimPrize += Reward;
    }
    
    private IEnumerator MiningUp(float added)
    {
        _playerProfile.percentUpgrade += added;
        yield return new WaitForSeconds(1800f);
        _playerProfile.percentUpgrade -= added;
    }

    private void MinerUp()
    {
        _playerProfile.GetAllMiners()[UnityEngine.Random.Range(0, _playerProfile.GetAllMiners().Count)].LevelUp();
    }

    private void AddExtraMiner(MinerConfiguration miner)
    {
        if(miner == null)
        {
            return;
        }
        MinerCreatorSystem minerCreator = new MinerCreatorSystem();
        _playerProfile.AddMiner(minerCreator.CreateMiner(miner));
    }

    private void Reward(int day)
    {
        switch (day)
        {
            case 1:
                MiningUp(1.5f);
                break;
            case 2:
                MinerUp();
                break;
            case 3:
                AddExtraMiner(_XRPCoin);
                break;
            case 4:
                _playerProfile.AddScore(CoinType.HornyBucks, 5);
                break;
            case 5:
                MinerUp();
                break;
            case 6:
                _playerProfile.AddScore(CoinType.HornyBucks, 25);
                break;
            case 7:
                AddExtraMiner(_WavesCoin);
                break;
            default:
                break;
        }
        
    }
}
