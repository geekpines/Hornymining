using App.Scripts.Gameplay.CoreGameplay.Mining;
using App.Scripts.Gameplay.CoreGameplay.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MinerEndgame : MonoBehaviour
{
    PlayerProfile _playerProfile;
    private string Coinkey = "HMCoinEndKey";

    [SerializeField] private Button Reset;
    [SerializeField] private List<MinerConfiguration> AddConfigs;


    [Inject]

    private void Construct(PlayerProfile playerProfile)
    {
        _playerProfile = playerProfile;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    
}
