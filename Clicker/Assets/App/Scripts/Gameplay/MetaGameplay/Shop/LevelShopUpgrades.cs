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
    public int CurrentLevel { get; private set; } = 1;
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
        if (playerProfile.TryRemoveScore(playerProfile.Coins[CurrentLevel - 1].ID, 100) && CurrentLevel <= 5)
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
                    LevelUp();
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
                LevelUp();
                return 1f;


            default:
                return 0;
        }
        return 0;
    }

    public float OpenStock(PlayerProfile playerProfile)
    {
        if (CurrentLevel < 6)
        {
            if (playerProfile.TryRemoveScore(playerProfile.Coins[CurrentLevel - 1].ID, 10))
            {
                playerProfile.AddScore(playerProfile.Coins[CurrentLevel - 1].ID, -10);
                LevelUp();
                return GetSale();
            }
            else return 0;
        }
        return 0;
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
            if (CurrentLevel < 5)
            {


                if (playerProfile.TryRemoveScore(playerProfile.Coins[CurrentLevel].ID, 10))
                {
                    playerProfile.AddScore(playerProfile.Coins[CurrentLevel].ID, -10);
                    LevelUp();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                LevelUp();
                return false;
            }
        }
        catch (ArgumentOutOfRangeException)
        {
            return false;
            throw;
        }
        
    }

    public List<AdditionalCoins> Surprise()
    {
        List<AdditionalCoins> coins = new List<AdditionalCoins>();
        switch (CurrentLevel)
        {     
            case 1:
                {            
                    coins.Add(SetAdditionalCoins(CoinType.Usdfork, 0.5f));
                    LevelUp();
                    break;
                }
            case 2:
                {
                    coins.Add(SetAdditionalCoins(CoinType.Usdfork, 1f));
                    coins.Add(SetAdditionalCoins(CoinType.LTC, 0.03f));
                    LevelUp();
                    break;
                }
            case 3:
                {
                    coins.Add(SetAdditionalCoins(CoinType.Usdfork, 1f));
                    coins.Add(SetAdditionalCoins(CoinType.LTC, 0.07f));
                    coins.Add(SetAdditionalCoins(CoinType.Ether, 0.003f));
                    LevelUp();
                    break;
                }
            case 4:
                {
                    coins.Add(SetAdditionalCoins(CoinType.Usdfork, 1.5f));
                    coins.Add(SetAdditionalCoins(CoinType.LTC, 1f));
                    coins.Add(SetAdditionalCoins(CoinType.Ether, 0.03f));
                    coins.Add(SetAdditionalCoins(CoinType.BTC, 0.0005f));
                    LevelUp();
                    break;
                }
            case 5:
                {
                    coins.Add(SetAdditionalCoins(CoinType.Usdfork, 1.5f));
                    coins.Add(SetAdditionalCoins(CoinType.LTC, 1));
                    coins.Add(SetAdditionalCoins(CoinType.Ether, 0.05f));
                    coins.Add(SetAdditionalCoins(CoinType.BTC, 0.005f));
                    LevelUp();
                    break;
                }

            default:
                break;
        }
        return coins;
    }

    private AdditionalCoins SetAdditionalCoins(CoinType type, float amount)
    {
        AdditionalCoins coins = new AdditionalCoins();

        coins.SetAdditionalCoin(type, amount);

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

    public void OnReset()
    {
        PlayerPrefs.DeleteKey(_shopLevelKey + gameObject.name);
    }
}
