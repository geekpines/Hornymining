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

    [SerializeField] GameObject LoadigScreen;




    [Inject]
    private void Construct(PlayerProfile playerProfile, MinerCreatorSystem minerCreatorSystem)
    {
        _playerProfile = playerProfile;
        _minerCreatorSystem = minerCreatorSystem;
    }


    // Start is called before the first frame update
    void Start()
    {
        
        StartCoroutine(LoadMiner());
        StartCoroutine(GameSaver());
    }


    private IEnumerator GameSaver()
    {
        Debug.Log("Save in 50 sec");

        yield return new WaitForSeconds(10);
        if (true)
        {
            SaveGame.Clear();
            SaveGame.Save<List<Miner>>(key, _playerProfile.GetAllMiners());
        }
        StartCoroutine(GameSaver());
    }

    private IEnumerator LoadMiner()
    {
        yield return new WaitForSeconds(2);
        if (SaveGame.Load<List<Miner>>(key) != null)
        {
            Debug.Log("Trying Load");
            foreach (var miner in SaveGame.Load<List<Miner>>(key))
            {
                foreach(var v in AddMiners)
                    if(v.Name.ToString() == miner.Name.ToString())
                    {
                        Debug.Log(miner.Level);
                        Miner created = _minerCreatorSystem.CreateMiner(v);
                        int k = miner.Level;
                        while (k != 0)
                        {
                            created.LevelUp();
                            k--;
                        }                        
                        Debug.Log(created.Level);
                        _playerProfile.AddMiner(created);
                        selectPanelUiController.SetMinerLevel(created.ID, created.Level + 1);
                    }
                
                
            }

        }
    }
}
