using App.Scripts.Gameplay.CoreGameplay.Player;
using App.Scripts.UiControllers.GameScreen.SelectMinersPanel;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class NewGamePlus : MonoBehaviour
{
    [SerializeField] Button yesButton;
    [SerializeField] MinersSelectPanelUiController minersSelectPanelUiController;

    string minerKey = "HMinerName";
    string levelKey = "HMinerLevel";
    private PlayerProfile _playerProfile;
    private string newGameKey = "HMNG";
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
        
        PlayerPrefs.DeleteAll();
        SaveAllMiners();
        cycle += 1;
        PlayerPrefs.SetInt(newGameKey, cycle);
        PlayerPrefs.Save();
        GameObject[] TS = GameObject.FindGameObjectsWithTag("CoinTradeSys");
        foreach(var tradeSystem in TS)
        {
            tradeSystem.GetComponent<CoinsTradeSystemView>().NG();
        }
        minersSelectPanelUiController.Cleaner();
        float i = (float)Math.Sqrt( _playerProfile.Coins[5].Value * 0.001f) * 0.1f;
        _playerProfile.ResetPlayer();
        _playerProfile.percentUpgrade += i;
        //GameObject[] shopUpgrades = GameObject.FindGameObjectsWithTag("ShopUpgrade");
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
            
            SaveMiner(miner.Name.ToString(), miner.Level);
        }
    }
    private void SaveMiner(string MinerName, int MinerLevel)
    {
        PlayerPrefs.SetString(minerKey + minerCounts, MinerName);
        PlayerPrefs.SetInt(levelKey + minerCounts, MinerLevel);
        minerCounts--;
        PlayerPrefs.Save();
    }

}
