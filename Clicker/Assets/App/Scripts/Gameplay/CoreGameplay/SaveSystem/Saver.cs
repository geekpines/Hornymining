using App.Scripts.Gameplay.CoreGameplay.Mining;
using App.Scripts.Gameplay.CoreGameplay.Player;
using App.Scripts.UiControllers.GameScreen.SelectMinersPanel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Settings;
using Zenject;

public class Saver : MonoBehaviour
{
    
    private string key = "HMinerName";
    private string levelKey = "HMinerLevel";
    private string coinKey = "HMCoins";
    private string volumeKey = "HMVolume";

    [SerializeField] public List<MinerConfiguration> AddMiners = new List<MinerConfiguration>();
    
    [SerializeField] private MinersSelectPanelUiController selectPanelUiController;
    
    [SerializeField] private List<AudioSource> audioSources;

    private PlayerProfile _playerProfile;
    private MinerCreatorSystem _minerCreatorSystem;

    private int minerCounts = 0;

    [Inject]
    private void Construct(PlayerProfile playerProfile, MinerCreatorSystem minerCreatorSystem)
    {
        _playerProfile = playerProfile;
        _minerCreatorSystem = minerCreatorSystem;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("HMLanguage") == 0)
        {
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[0];
        }
        else
        {
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[1];
        }
        //load
        StartCoroutine(LoadMiner());
        LoadCoin();
        LoadVolume();
        //save
        StartCoroutine(AllMinerSaver());
        StartCoroutine(SaveCoins());
        //checked

    }


    private IEnumerator AllMinerSaver()
    {
        Debug.Log("Save in 50 sec");
        
        yield return new WaitForSeconds(10);
        if (true)
        {
            minerCounts = _playerProfile.GetAllMiners().Count;
            PlayerPrefs.SetInt("HMinerCounts", minerCounts);
            PlayerPrefs.Save();
            foreach (var miner in _playerProfile.GetAllMiners())
            {
                SaveMiner(miner.Name.ToString(), miner.Level);
            }
            
        }
        StartCoroutine(AllMinerSaver());
    }

    private void SaveMiner(string MinerName, int MinerLevel)
    {
        
        PlayerPrefs.SetString(key + minerCounts, MinerName);
        PlayerPrefs.SetInt(levelKey + minerCounts, MinerLevel);
        minerCounts--;
        PlayerPrefs.Save();
    }


    private IEnumerator LoadMiner()
    {
        yield return new WaitForSeconds(0.3f);

        if (true)
        {
            Debug.Log("Trying Load");
            
            int count = PlayerPrefs.GetInt("HMinerCounts");

            if (count != 0)
                while (count != 0)
                {

                    foreach (var miner in AddMiners)
                    {

                        if (miner.Name.ToString() == PlayerPrefs.GetString(key + count))
                        {

                            Miner minerC = _minerCreatorSystem.CreateMiner(miner);

                            LoadLevel(minerC, count);
                        }
                    }
                    count--;
                }
            else
            {
                AddStartMiner();
            }
        }
    }

    private void LoadLevel(Miner miner, int count)
    {
        int level = PlayerPrefs.GetInt(levelKey + count);

        while (level != 0)
        {
            miner.LevelUp();
            level--;
        }
        
        _playerProfile.AddMiner(miner);

       
    }


    private IEnumerator SaveCoins()
    {
        yield return new WaitForSeconds(10);
        if (true)
        {
            
            foreach (var coin in _playerProfile.Coins)
            {
                SaveCoin(coin.Value, coin.ID.ToString());
            }
            
        }
        StartCoroutine(SaveCoins());
    }

    private void SaveCoin(float value, string coinName)
    {
        PlayerPrefs.SetFloat(coinKey + coinName, value);
        PlayerPrefs.Save();
    }

    private void LoadCoin()
    {
        foreach (var coin in _playerProfile.Coins)
        {
            float coinsValue = PlayerPrefs.GetFloat(coinKey + coin.ID.ToString());
            coin.Add(coinsValue);            
        }
    }

    private void AddStartMiner()
    {
        List<MinerConfiguration> confs = new List<MinerConfiguration>();
        foreach (var miner in AddMiners)
        {
            if (miner.Levels[0].MiningResources[0].Type == App.Scripts.Gameplay.CoreGameplay.Coins.CoinType.Tokken)
            {
                confs.Add(miner);
            }
        }
        Miner miner1 = _minerCreatorSystem.CreateMiner(confs[Random.Range(0, confs.Count)]);
        _playerProfile.AddMiner(miner1);
    }

    private void LoadVolume()
    {
        for (int i = 0; i < audioSources.Count; i++)
        {
            float vol = PlayerPrefs.GetFloat(volumeKey + i);
            if (vol != 0)
            {
                audioSources[i].volume = vol;
            }
        }
    }

    private void OnApplicationQuit()
    {
        AllMinerSaver();
        SaveCoins();
    }
}
