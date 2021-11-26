using App.Scripts.Gameplay.CoreGameplay.Mining;
using App.Scripts.Gameplay.CoreGameplay.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace MkeyFW
{
    public class TestEvent : MonoBehaviour
    {

        [SerializeField] private List<MinerConfiguration> AddMiners = new List<MinerConfiguration>();

        private PlayerProfile _playerProfile;
        private MinerCreatorSystem _minerCreatorSystem;
        private List<MinerConfiguration> _minerConfs;

        [Inject]
        private void Construct(PlayerProfile playerProfile, MinerCreatorSystem minerCreatorSystem)
        {
            _playerProfile = playerProfile;
            _minerCreatorSystem = minerCreatorSystem;
        }


        public void TestEvent_1()
        {
            Debug.Log("Test Event _1");
        }

        public void HitEvent_2500000()
        {
            Debug.Log("Hit Event 2 500 000");
        }

        public void HitEvent_10000000()
        {
            Debug.Log("Hit Event 10 000 000");
        }

        public void ResultEvent(string coin)
        {
            if (coin != "nothing")
            {
                foreach (var miner in AddMiners)
                {
                    if (miner.Levels[0].MiningResources[0].Type.ToString() == coin)
                    {
                        _minerConfs.Add(miner);
                    }
                }
                _playerProfile.AddMiner(_minerCreatorSystem.CreateMiner(_minerConfs[Random.Range(0, _minerConfs.Count)]));

            }
            
            Debug.Log("Result event");
        }
    }
}