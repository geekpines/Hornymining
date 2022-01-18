using App.Scripts.Gameplay.CoreGameplay.Mining;
using App.Scripts.Gameplay.CoreGameplay.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using BayatGames.SaveGameFree;
using App.Scripts.UiControllers.GameScreen.SelectMinersPanel;

public class Saver : MonoBehaviour
{
    
    string key = "playerHMData";
    [SerializeField] private List<MinerConfiguration> AddMiners = new List<MinerConfiguration>();
    private PlayerProfile _playerProfile;
    private MinerCreatorSystem _minerCreatorSystem;
    [SerializeField] private MinersSelectPanelUiController selectPanelUiController;


    [Inject]
    private void Construct(PlayerProfile playerProfile, MinerCreatorSystem minerCreatorSystem)
    {
        _playerProfile = playerProfile;
        _minerCreatorSystem = minerCreatorSystem;
    }


    // Start is called before the first frame update
    void Start()
    {
        //load
        StartCoroutine(LoadMiner());
        //LoadCoin();
        //save
        StartCoroutine(MinerSaver());
        //StartCoroutine(SaveCoins());
    }


    private IEnumerator MinerSaver()
    {
        Debug.Log("Save in 50 sec");

        yield return new WaitForSeconds(10);
        if (true)
        {
            SaveGame.Clear();
            SaveGame.Save<List<Miner>>(key, _playerProfile.GetAllMiners());
        }
        StartCoroutine(MinerSaver());
    }

    private IEnumerator LoadMiner()
    {
        yield return new WaitForSeconds(0.5f);
        if (SaveGame.Load<List<Miner>>(key) != null)
        {
            Debug.Log("Trying Load");
            SearchMinerToLoad(SaveGame.Load<List<Miner>>(key));
        }
    }

    private void SearchMinerToLoad(List<Miner> Miners)
    {
        foreach (var miner in Miners)
        {
            foreach (var v in AddMiners)
            {
                if (v.Name.ToString() == miner.Name.ToString())
                {
                    Miner created = _minerCreatorSystem.CreateMiner(v);
                    int k = miner.Level;
                    while (k != 0)
                    {
                        created.LevelUp();
                        k--;
                    }
                    _playerProfile.AddMiner(created);
                    selectPanelUiController.SetMinerLevel(created.ID, created.Level + 1);
                }
            }
        }
    }

    private IEnumerator SaveCoins()
    {
        yield return new WaitForSeconds(10);
        if (true)
        {
            List<float> CoinsValue = new List<float>();
            foreach (var coin in _playerProfile.Coins)
            {
                CoinsValue.Add(coin.Value);
            }
            SaveGame.Save<List<float>>("CoinHM", CoinsValue);    
        }
        StartCoroutine(SaveCoins());
    }

    private void LoadCoin()
    {
        List<float> CoinsValue = SaveGame.Load<List<float>>("CoinHM"); ;
        if (CoinsValue != null)
        {
            for (int i = 0; i < _playerProfile.Coins.Count; i++)
            {
                if (_playerProfile.Coins[i].Value == 0)
                {
                    _playerProfile.AddScore(_playerProfile.Coins[i].ID, CoinsValue[i]);
                }
            }
        }
    }
}
