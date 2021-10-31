using App.Scripts.Gameplay.CoreGameplay.Coins;
using App.Scripts.Gameplay.CoreGameplay.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelShopUpgrades : MonoBehaviour
{
    [field: SerializeField, Range(0, 5)] public int CurrentLevel { get; private set; } = 0;

    private void LevelUp()
    {
        CurrentLevel++;
    }

    public float CasualUpgrade(PlayerProfile playerProfile)
    {
        LevelUp();

        switch (CurrentLevel)
        {
            case 0:
                break;
            case 1:
                playerProfile.AddScore(CoinType.Bit, -100);
                return 1.25f;                
            case 2:
                playerProfile.AddScore(CoinType.Dash, -100);
                return 1.35f;
            case 3:
                playerProfile.AddScore(CoinType.LTC, -100);
                return 1.5f;
            case 4:
                playerProfile.AddScore(CoinType.Ether, -100);
                return 1f;
            case 5:
                playerProfile.AddScore(CoinType.BTC, -100);
                return 3f;

            default:
                return 1;
        }
        return 1;
    }
}
