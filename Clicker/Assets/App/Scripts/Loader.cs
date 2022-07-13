using App.Scripts.Gameplay.CoreGameplay.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Loader : MonoBehaviour
{
    [SerializeField] private UpgradeEvents _upgradeEvents;
    [SerializeField] private List<LevelShopUpgrades> _shopUpgrades;
    private PlayerProfile _player;

    [Inject]
    private void Construct(PlayerProfile playerProfile)
    {
        _player = playerProfile;
    }

    private void Start()
    {
        StartCoroutine(Slow(OnLoad));
    }

    private void OnLoad()
    {
        int k = _shopUpgrades[0].LoadLevel(_shopUpgrades[0].name);
        if(k > 1)
        {
            while (k > 1)
            {
                _player.percentUpgrade += _shopUpgrades[0].LoadCasualUpgrade();
                //Debug.Log("12");
                k--;
            }
        }
        k = _shopUpgrades[1].LoadLevel(_shopUpgrades[1].name);
        if (k > 1)
        {
            while (k > 1)
            {
                _shopUpgrades[1].LevelUp();
                _upgradeEvents.MinerSlotOppened(true);
                //Debug.Log("123");
                k--;
            }
        }
        k = _shopUpgrades[2].LoadLevel(_shopUpgrades[2].name);
        if( k > 1)
        {
            while(k > 1)
            {
                _upgradeEvents.SurpriseUpgrade(_shopUpgrades[2].Surprise());
                k--;
                //Debug.Log("23");
            }
        }
        k = _shopUpgrades[3].LoadLevel(_shopUpgrades[3].name);

        if (k > 1)
        {
            while (k > 1)
            {
                _upgradeEvents.StockOpened(_shopUpgrades[3].LoadOpenStock() != 0, _shopUpgrades[3].CurrentLevel - 1);
                k--;
                //Debug.Log("23");
            }
        }
    }

    private IEnumerator Slow(Action action)
    {
        yield return new WaitForSeconds(0.1f);
        action();
    }
}
