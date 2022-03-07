using App.Scripts.Gameplay.CoreGameplay.Player;
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
  
    private PlayerProfile _playerProfile;
    private string newGameKey = "HMNG";
    public int cycle = 0;

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
        PlayerPrefs.Save();
        cycle += 1;
        PlayerPrefs.SetInt(newGameKey, cycle);
        PlayerPrefs.Save();
        GameObject[] TS = GameObject.FindGameObjectsWithTag("CoinTradeSys");
        foreach(var tradeSystem in TS)
        {
            tradeSystem.GetComponent<CoinsTradeSystemView>().NG();
        } 

        float i = (float)Math.Sqrt( _playerProfile.Coins[5].Value * 0.001f) * 0.1f;
        _playerProfile.ResetPlayer();
        _playerProfile.percentUpgrade += i;
        //GameObject[] shopUpgrades = GameObject.FindGameObjectsWithTag("ShopUpgrade");
        SceneManager.LoadScene("Loading");
    }

}
