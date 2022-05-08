using App.Scripts.Gameplay.CoreGameplay.Coins;
using App.Scripts.Gameplay.CoreGameplay.Player;
using App.Scripts.UiControllers.GameScreen.MinersPanel;
using System;
using System.Collections;
using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;

public class LevelShopUpgrades : MonoBehaviour
{
    [field: SerializeField, Range(1, 5)] public int CurrentLevel { get; private set; } = 1;
    private string _shopLevelKey = "HMShopsLevel";
    [SerializeField] private TextMeshProUGUI _levelText;

    public void LevelUp()
    {
        CurrentLevel++;
    }

    private void OnEnable()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(UpdateLevelText);
    }

    private void OnDisable()
    {
        gameObject.GetComponent<Button>().onClick.RemoveListener(UpdateLevelText);
        SaveLevel(gameObject.name);
    }
    public float CasualUpgrade(PlayerProfile playerProfile)
    {
        if (playerProfile.TryRemoveScore(playerProfile.Coins[CurrentLevel - 1].ID, 100) && CurrentLevel < 5)
        {
            playerProfile.AddScore(playerProfile.Coins[CurrentLevel - 1].ID, -100);
            switch (CurrentLevel)
            {
                case 0:
                    break;
                case 1:
                    LevelUp();
                    return 0.25f;
                case 2:
                    LevelUp();
                    return 0.10f;
                case 3:
                    LevelUp();
                    return 0.15f;
                case 4:
                    LevelUp();
                    return 0.5f;
                case 5:
                    return 1f;

                default:
                    return 0;
            }
        }
        return 0;
    }

    public float LoadCasualUpgrade(PlayerProfile playerProfile)
    {
        switch (CurrentLevel)
        {
            case 0:
                break;
            case 1:
                LevelUp();
                return 0.25f;
            case 2:
                LevelUp();
                return 0.10f;
            case 3:
                LevelUp();
                return 0.15f;
            case 4:
                LevelUp();
                return 0.5f;
            case 5:
                return 1f;

            default:
                return 0;
        }
        return 0;
    }

    public float OpenStock(PlayerProfile playerProfile)
    {
        if (playerProfile.TryRemoveScore(playerProfile.Coins[CurrentLevel - 1].ID, -10)
            && playerProfile.Coins.Count > CurrentLevel) 
        {
            playerProfile.AddScore(playerProfile.Coins[CurrentLevel - 1].ID, -10);
            LevelUp();
            return GetSale();
        }
        else return 0;
    }

    public float LoadOpenSlot(GameObject _object)
    {
        _object.SetActive(true);
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
        try
        {
            if (playerProfile.TryRemoveScore(playerProfile.Coins[CurrentLevel].ID, 10))
            {
                playerProfile.AddScore(playerProfile.Coins[CurrentLevel - 1].ID, -10);
                LevelUp();
                return true;
            }
            else return false;
        }
        catch (ArgumentOutOfRangeException)
        {
            return false;
            throw;
        }
        
    }

    public List<AdditionalCoins> Surprise(AdditionalCoins additionalCoins)
    {
        List<AdditionalCoins> coins = new List<AdditionalCoins>();
        switch (CurrentLevel)
        {     
            case 1:
                {                    
                    additionalCoins.SetAdditionalCoin(CoinType.Usdfork, 0.5f);
                    coins.Add(additionalCoins);
                    LevelUp();
                    break;
                }
            case 2:
                {
                    additionalCoins.SetAdditionalCoin(CoinType.Usdfork, 1f);
                    coins.Add(additionalCoins);
                    additionalCoins.SetAdditionalCoin(CoinType.LTC, 0.03f);
                    coins.Add(additionalCoins);
                    LevelUp();
                    break;
                }
            case 3:
                {
                    additionalCoins.SetAdditionalCoin(CoinType.Usdfork, 1f);
                    coins.Add(additionalCoins);
                    additionalCoins.SetAdditionalCoin(CoinType.LTC, 0.07f);
                    coins.Add(additionalCoins);
                    additionalCoins.SetAdditionalCoin(CoinType.Ether, 0.003f);
                    coins.Add(additionalCoins);
                    LevelUp();
                    break;
                }
            case 4:
                {
                    additionalCoins.SetAdditionalCoin(CoinType.Usdfork, 1.5f);
                    coins.Add(additionalCoins);
                    additionalCoins.SetAdditionalCoin(CoinType.LTC, 1f);
                    coins.Add(additionalCoins);
                    additionalCoins.SetAdditionalCoin(CoinType.Ether, 0.03f);
                    coins.Add(additionalCoins);
                    additionalCoins.SetAdditionalCoin(CoinType.BTC, 0.0005f);
                    coins.Add(additionalCoins);
                    LevelUp();
                    break;
                }
            case 5:
                {
                    additionalCoins.SetAdditionalCoin(CoinType.Usdfork, 1.5f);
                    coins.Add(additionalCoins);
                    additionalCoins.SetAdditionalCoin(CoinType.LTC, 1);
                    coins.Add(additionalCoins);
                    additionalCoins.SetAdditionalCoin(CoinType.Ether, 0.05f);
                    coins.Add(additionalCoins);
                    additionalCoins.SetAdditionalCoin(CoinType.BTC, 0.005f);
                    coins.Add(additionalCoins);
                    break;
                }

            default:
                break;
        }
        return coins;
    }

    public void SaveLevel(string key)
    {
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
        if(CurrentLevel <= 5)
        {
            _levelText.text = "Level: " + CurrentLevel;
        }
        else
        {
            _levelText.text = "Level: MAX";
        }
        
    }

}
