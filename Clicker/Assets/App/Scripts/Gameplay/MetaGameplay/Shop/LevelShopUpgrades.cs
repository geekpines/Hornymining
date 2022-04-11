using App.Scripts.Gameplay.CoreGameplay.Coins;
using App.Scripts.Gameplay.CoreGameplay.Player;
using App.Scripts.UiControllers.GameScreen.MinersPanel;
using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class LevelShopUpgrades : MonoBehaviour
{
    [field: SerializeField, Range(1, 5)] public int CurrentLevel { get; private set; } = 1;
    private string _shopLevelKey = "HMShopsLevel";
    [SerializeField] private TextMeshProUGUI _levelText;

    public void LevelUp()
    {
        CurrentLevel++;
    }

    private void OnDisable()
    {
        SaveLevel(gameObject.name);
    }
    public float CasualUpgrade(PlayerProfile playerProfile)
    {
        if (playerProfile.TryRemoveScore(playerProfile.Coins[CurrentLevel - 1].ID, 100) && CurrentLevel < 5)
        {
            switch (CurrentLevel)
            {
                case 0:
                    break;
                case 1:
                    playerProfile.AddScore(CoinType.Tokken, -100);
                    LevelUp();
                    return 0.25f;
                case 2:
                    playerProfile.AddScore(CoinType.Usdfork, -100);
                    LevelUp();
                    return 0.10f;
                case 3:
                    playerProfile.AddScore(CoinType.LTC, -100);
                    LevelUp();
                    return 0.15f;
                case 4:
                    playerProfile.AddScore(CoinType.Ether, -100);
                    LevelUp();
                    return 0.5f;
                case 5:
                    playerProfile.AddScore(CoinType.BTC, -100);
                    return 1f;

                default:
                    return 0;
            }
        }
        return 0;
    }

    public float OpenSlot(PlayerProfile playerProfile, GameObject _object)
    {            
            _object.SetActive(true);
            playerProfile.AddScore(playerProfile.Coins[CurrentLevel-1].ID, -1);
            LevelUp();
            return GetSale();
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

    public bool OpenMinerSlot(PlayerProfile playerProfile)
    {
        

        playerProfile.AddScore(playerProfile.Coins[CurrentLevel - 1].ID, -100);
        return playerProfile.TryRemoveScore(playerProfile.Coins[CurrentLevel].ID, 100);
    }

    public void Surprise(AdditionalCoins additionalCoins, MinerActiveSlotsEventsUiController _minerActiveSlotsEventsUiController)
    {
        
        switch (CurrentLevel)
        {     
            case 1:
                {                    
                    additionalCoins.SetAdditionalCoin(CoinType.Usdfork, 5);
                    _minerActiveSlotsEventsUiController.AddAdditionalCoin(additionalCoins);
                    LevelUp();
                    break;
                }
            case 2:
                {
                    additionalCoins.SetAdditionalCoin(CoinType.Usdfork, 7);
                    _minerActiveSlotsEventsUiController.AddAdditionalCoin(additionalCoins);
                    additionalCoins.SetAdditionalCoin(CoinType.LTC, 0.3f);
                    _minerActiveSlotsEventsUiController.AddAdditionalCoin(additionalCoins);
                    LevelUp();
                    break;
                }
            case 3:
                {
                    additionalCoins.SetAdditionalCoin(CoinType.Usdfork, 7);
                    _minerActiveSlotsEventsUiController.AddAdditionalCoin(additionalCoins);
                    additionalCoins.SetAdditionalCoin(CoinType.LTC, 0.7f);
                    _minerActiveSlotsEventsUiController.AddAdditionalCoin(additionalCoins);
                    additionalCoins.SetAdditionalCoin(CoinType.Ether, 0.3f);
                    _minerActiveSlotsEventsUiController.AddAdditionalCoin(additionalCoins);
                    LevelUp();
                    break;
                }
            case 4:
                {
                    additionalCoins.SetAdditionalCoin(CoinType.Usdfork, 50);
                    _minerActiveSlotsEventsUiController.AddAdditionalCoin(additionalCoins);
                    additionalCoins.SetAdditionalCoin(CoinType.LTC, 3);
                    _minerActiveSlotsEventsUiController.AddAdditionalCoin(additionalCoins);
                    additionalCoins.SetAdditionalCoin(CoinType.Ether, 1);
                    _minerActiveSlotsEventsUiController.AddAdditionalCoin(additionalCoins);
                    additionalCoins.SetAdditionalCoin(CoinType.BTC, 0.05f);
                    _minerActiveSlotsEventsUiController.AddAdditionalCoin(additionalCoins);
                    LevelUp();
                    break;
                }
            case 5:
                {
                    additionalCoins.SetAdditionalCoin(CoinType.Usdfork, 100);
                    _minerActiveSlotsEventsUiController.AddAdditionalCoin(additionalCoins);
                    additionalCoins.SetAdditionalCoin(CoinType.LTC, 15);
                    _minerActiveSlotsEventsUiController.AddAdditionalCoin(additionalCoins);
                    additionalCoins.SetAdditionalCoin(CoinType.Ether, 5);
                    _minerActiveSlotsEventsUiController.AddAdditionalCoin(additionalCoins);
                    additionalCoins.SetAdditionalCoin(CoinType.BTC, 0.5f);
                    _minerActiveSlotsEventsUiController.AddAdditionalCoin(additionalCoins);
                    break;
                }

            default:
                break;
        }
    }

    public void SaveLevel(string key)
    {
        Debug.Log("**");
        //yield return new WaitForSeconds(10f);
        PlayerPrefs.SetInt(_shopLevelKey + key, CurrentLevel);
        PlayerPrefs.Save();
    }

    public int LoadLevel(string key)
    {
       return PlayerPrefs.GetInt(_shopLevelKey + key);        
    }

    public void UpdateLevelText()
    {
        _levelText.text = "Level: " + CurrentLevel;
    }
}
