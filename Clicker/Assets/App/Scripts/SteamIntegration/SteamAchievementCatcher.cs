using App.Scripts.Gameplay.CoreGameplay.Mining;
using App.Scripts.Gameplay.CoreGameplay.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SteamAchievementCatcher : MonoBehaviour
{
    [SerializeField] private SteamEvents SteamEvents;

    [Header ("Увеличение уровня майнера")]
    [SerializeField] private Button _minerLevelUp;

    [Header("Кнопка NG+")]
    [SerializeField] private Button _ngStart;

    private PlayerProfile _playerProfile;

    [Inject]
    private void Construct(PlayerProfile playerProfile)
    {
        _playerProfile = playerProfile;
    }
    
    private void Start()
    {
        _minerLevelUp.onClick.AddListener(CheckMinersLevel);
        _ngStart.onClick.AddListener(CheckNgStart);

        _playerProfile.OnAllMinersCountChanged += CatchMinerToAddListener;
        _playerProfile.OnActiveMinersCountChanged += IsAllAcitveMinerSlotFull;
    }

    private void OnDestroy()
    {
        _minerLevelUp.onClick.RemoveListener(CheckMinersLevel);
        _ngStart.onClick.RemoveListener(CheckNgStart);

        _playerProfile.OnAllMinersCountChanged -= CatchMinerToAddListener;
        _playerProfile.OnActiveMinersCountChanged -= IsAllAcitveMinerSlotFull;
    }

    private void OnDisable()
    {
        _minerLevelUp.onClick.RemoveListener(CheckMinersLevel);
        _ngStart.onClick.RemoveListener(CheckNgStart);

        _playerProfile.OnAllMinersCountChanged -= CatchMinerToAddListener;
        _playerProfile.OnActiveMinersCountChanged -= IsAllAcitveMinerSlotFull;
    }

    private void CheckMinersLevel()
    {
        int minerCount = 0;
        foreach (var miner in _playerProfile.GetAllMiners())
        {
            if (miner.Level == 5)
            {
                minerCount++;
            }
        }

        if (minerCount == 26)
        {
            SteamEvents.AllFifthStar();
        }
    }

    private void CheckMinerHearts()
    {
        int minerCount = 0;
        foreach (var miner in _playerProfile.GetAllMiners())
        {
            minerCount++;
        }
        if (minerCount == 26)
        {
            SteamEvents.AllHearted();
        }
    }

    private void CatchMinerToAddListener(Miner miner)
    {
        miner.OnHeartsUp += CheckMinerHearts;
    }

    private void IsAllAcitveMinerSlotFull(Miner miner)
    {
        if (_playerProfile.GetActiveMiners().Count == 5)
        {
            SteamEvents.FillActiveSlots();
        }
    }

    private void CheckNgStart()
    {
        SteamEvents.BackInPast();
    }
}
