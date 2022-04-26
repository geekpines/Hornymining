using App.Scripts.Gameplay.CoreGameplay.Mining;
using App.Scripts.Gameplay.CoreGameplay.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace App.Scripts.Gameplay.Prototypes
{
    public class PrototypeAddMiner : MonoBehaviour
    {
        [SerializeField] private List<MinerConfiguration> AddMiners = new List<MinerConfiguration>();
        private PlayerProfile _playerProfile;
        private MinerCreatorSystem _minerCreatorSystem;

        [SerializeField] GameObject LoadigScreen;
        
        string key = "playerHMData";


        [Inject]
        private void Construct(PlayerProfile playerProfile, MinerCreatorSystem minerCreatorSystem)
        {
            _playerProfile = playerProfile;
            _minerCreatorSystem = minerCreatorSystem;
        }

        private void Start()
        {
            StartCoroutine(MinerAdder());
        }

        private IEnumerator MinerAdder()
        {
            yield return new WaitForSeconds(0.1f);
            foreach (var minerConfiguration in AddMiners)
            {
                _playerProfile.AddMiner(_minerCreatorSystem.CreateMiner(minerConfiguration));
            }
        }

        private IEnumerator GameSaver()
        {
            Debug.Log("Save in 50 sec");
            yield return new WaitForSeconds(50);
            if(true)
            {
                Debug.Log("Save Started");
                string value = JsonUtility.ToJson(_playerProfile);

                PlayerPrefs.SetString(key, value);
                PlayerPrefs.Save();
                Debug.Log("Save Finished");
            }
            StartCoroutine(GameSaver());
        }

        private IEnumerator LoadGame()
        {
            yield return new WaitForSeconds(2);
            if (true)
            {
                Debug.Log("TryingLoad");
                string value = PlayerPrefs.GetString(key);
                Debug.Log(value);

                if (value != null)
                {
                    foreach(var miner in JsonUtility.FromJson<PlayerProfile>(value).GetAllMiners())
                    {
                        _playerProfile.AddMiner(miner);
                    }
                }

            }
        }


    }
}