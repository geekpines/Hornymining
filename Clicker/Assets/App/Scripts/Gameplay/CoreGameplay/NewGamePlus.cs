using App.Scripts.Gameplay.CoreGameplay.Player;
using App.Scripts.UiControllers.GameScreen.SelectMinersPanel;
using NiobiumStudios;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class NewGamePlus : MonoBehaviour
{
    [SerializeField] private Button yesButton;
    [SerializeField] private MinersSelectPanelUiController minersSelectPanelUiController;

    private string minerKey = "HMinerName";
    private string levelKey = "HMinerLevel";
    private PlayerProfile _playerProfile;
    private string newGameKey = "HMNG";
    private string spinKey = "HMSpins";
    private string coinKey = "HMCoins";


    public int cycle = 0;

    int minerCounts = 0;
    [Inject]
    private void Construct(PlayerProfile playerProfile)
    {
        _playerProfile = playerProfile;
    }

    private void Start()
    {
        yesButton.onClick.AddListener(GameReset);
        cycle = PlayerPrefs.GetInt(newGameKey);
        if (cycle > 1)
        {

        }
    }

    private void GameReset()
    {

        //PlayerPrefs.DeleteAll();
        PlayerPrefs.DeleteKey(spinKey);
        PlayerPrefs.DeleteKey(coinKey);
        ClearUpgrades();
        SaveAllMiners();
        cycle += 1;

        PlayerPrefs.SetInt(newGameKey, cycle);
        PlayerPrefs.Save();

        PlayerPrefs.SetInt("HBTutorial", 1);
        PlayerPrefs.Save();

        GameObject[] TS = GameObject.FindGameObjectsWithTag("CoinTradeSys");
        
        foreach(var tradeSystem in TS)
        {
            tradeSystem.GetComponent<CoinsTradeSystemView>().NG();
        }
        
        minersSelectPanelUiController.Cleaner();
        
        float i = (float)Math.Sqrt( _playerProfile.Coins[5].Value * 0.001f) * 0.1f;
        _playerProfile.ResetPlayer();
        _playerProfile.percentUpgrade += i + PlayerPrefs.GetFloat("HMNGUpd");
        PlayerPrefs.SetFloat("HMNGUpd",_playerProfile.percentUpgrade);
        PlayerPrefs.Save();
        
        SceneManager.LoadScene("Loading");
    }


    private void SaveAllMiners()
    {
        minerCounts = _playerProfile.GetAllMiners().Count;
        PlayerPrefs.SetInt("HMinerCounts", minerCounts);
        PlayerPrefs.Save();
        Debug.Log(minerCounts);
        foreach (var miner in _playerProfile.GetAllMiners())
        {
            SaveMiner(miner.Name.ToString(), 0);
        }
    }
    private void SaveMiner(string MinerName, int MinerLevel)
    {
        PlayerPrefs.SetString(minerKey + minerCounts, MinerName);
        PlayerPrefs.SetInt(levelKey + minerCounts, MinerLevel);
        minerCounts--;
        PlayerPrefs.Save();
    }

    private void ClearUpgrades()
    {
        GameObject[] Upgrades = GameObject.FindGameObjectsWithTag("LevelShopUpgrade");
        foreach (var upgrade in Upgrades)
        {
            upgrade.GetComponent<LevelShopUpgrades>().OnReset(upgrade.name);

        }
    }
}
