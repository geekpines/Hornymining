using App.Scripts.Gameplay.CoreGameplay.Mining;
using App.Scripts.Gameplay.CoreGameplay.Player;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace MkeyFW
{
    public class TestEvent : MonoBehaviour
    {

        [SerializeField] private List<MinerConfiguration> AddMiners = new List<MinerConfiguration>();
        [SerializeField] private RollGirlController rollGirl;


        private PlayerProfile _playerProfile;
        private MinerCreatorSystem _minerCreatorSystem;
       

        private int minerCounter = 0;

        [Inject]
        private void Construct(PlayerProfile playerProfile, MinerCreatorSystem minerCreatorSystem)
        {
            _playerProfile = playerProfile;
            _minerCreatorSystem = minerCreatorSystem;
        }

        private void Start()
        {
            foreach (var miner in _playerProfile.GetAllMiners())
            {
                AddMiners.Remove(miner.Configuration);
            }
        }


        public void TestEvent_1(string coin)
        {

            List<MinerConfiguration> _minerConfs = new List<MinerConfiguration>();

            if (coin != "nothing")
            {
                rollGirl.PlayHappy();
                foreach (var miner in AddMiners)
                {
                    if (miner.Levels[0].MiningResources[0].Type.ToString() == coin && miner != null)
                    {
                        _minerConfs.Add(miner);                        
                    }
                }
                if (_minerConfs != null && _minerConfs.Count != 0)
                {
                    Debug.Log(_minerConfs);
                    MinerConfiguration minerConfiguration = _minerConfs[UnityEngine.Random.Range(0, _minerConfs.Count - 1)];
                    _playerProfile.AddMiner(_minerCreatorSystem.CreateMiner(minerConfiguration));
                    AddMiners.Remove(minerConfiguration);
                }
                else
                {
                    Debug.LogError("Out Of Miners");

                }

            }
        }

        public void HitEvent_2500000()
        {
            Debug.Log("Hit Event 2 500 000");
        }

        public void HitEvent_10000000()
        {
            rollGirl.PlaySad();
        }

        public void ResultEvent()
        {
            

            Debug.Log("Result event");
        }
    }
}