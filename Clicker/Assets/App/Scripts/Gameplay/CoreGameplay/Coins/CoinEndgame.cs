using App.Scripts.Gameplay.CoreGameplay.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class CoinEndgame : MonoBehaviour
{

    PlayerProfile _playerProfile;
    private string Coinkey = "HMCoinEndKey";
    string levelKey = "HMinerLevel";
    string key = "HMinerName";
    int minerCounts;


    [SerializeField] private Button Reset;

    [Inject]

    private void Construct(PlayerProfile playerProfile)
    {
        _playerProfile = playerProfile;
    }

    // Start is called before the first frame update
    void Start()
    {
        Reset.onClick.AddListener(ResetCoin);

        float percent = PlayerPrefs.GetFloat(Coinkey);
        if(percent != 0)
        {
            _playerProfile.percentUpgrade = percent;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ResetCoin()
    {
        PlayerPrefs.DeleteAll();
        float percent = 0;
        foreach(var coin in _playerProfile.Coins)
        {
            if(coin.ID == _playerProfile.Coins[5].ID)
            {
                percent = coin.Value / 5000;
            }
            _playerProfile.AddScore(coin.ID, -coin.Value);
        }
        if (percent > 4) percent = 4;
        PlayerPrefs.SetFloat(Coinkey, percent);
        _playerProfile.percentUpgrade = percent;
        KeepMiner();
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
    
    private void KeepMiner()
    {
        minerCounts = _playerProfile.GetAllMiners().Count;
        PlayerPrefs.SetInt("HMinerCounts", minerCounts);
        PlayerPrefs.Save();
        foreach (var miner in _playerProfile.GetAllMiners())
        {
            SaveMiner(miner.Name.ToString(), 0);
        }
    }
    private void SaveMiner(string MinerName, int MinerLevel)
    {

        PlayerPrefs.SetString(key + minerCounts, MinerName);
        PlayerPrefs.SetInt(levelKey + minerCounts, MinerLevel);
        minerCounts--;
        PlayerPrefs.Save();
    }
}
