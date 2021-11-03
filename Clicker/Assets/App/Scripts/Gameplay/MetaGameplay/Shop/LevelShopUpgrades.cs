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
                return 2f;
            case 5:
                playerProfile.AddScore(CoinType.BTC, -100);
                return 3f;

            default:
                return 1;
        }
        return 1;
    }

    public void OpenSlot(PlayerProfile playerProfile, GameObject _object)
    {
        LevelUp();
        _object.SetActive(true);
        switch (CurrentLevel)
        {
            case 0:
                break;
            case 1:
                playerProfile.AddScore(CoinType.Bit, -10);
                break;
            case 2:
                playerProfile.AddScore(CoinType.Dash, -10);
                break;
            case 3:
                playerProfile.AddScore(CoinType.LTC, -10);
                break;            
            case 4:
                playerProfile.AddScore(CoinType.Ether, -10);
                break;          
            case 5:
                playerProfile.AddScore(CoinType.BTC, -10);
                break;           
            default:
                break;     
        }        
    }

    
    public float GetSale()
    {
        switch (CurrentLevel)
        {
            case 0:
                break;
            case 1:
                return 1;
            case 2:
                return 0.95f;
            case 3:
                return 0.85f;
            case 4:
                return 0.7f;
            case 5:
                return 0.55f;

            default:
                return 1;
        }
        return 1;
    }
    
    public float OpenMinerSlot(PlayerProfile playerProfile)
    {
        LevelUp();

        switch (CurrentLevel)
        {
            case 0:
                return 0.54f;
            case 1:
                playerProfile.AddScore(CoinType.Bit, -100);
                return 0.54f;
            case 2:
                playerProfile.AddScore(CoinType.Dash, -100);
                return 0.39f;
            case 3:
                playerProfile.AddScore(CoinType.LTC, -100);
                return 0.26f;
            case 4:
                playerProfile.AddScore(CoinType.Ether, -100);
                return 0.24f;
            case 5:
                playerProfile.AddScore(CoinType.BTC, -100);
                return 0.24f;

            default:
                return 1;
        }
        return 1;
    }

    
}
