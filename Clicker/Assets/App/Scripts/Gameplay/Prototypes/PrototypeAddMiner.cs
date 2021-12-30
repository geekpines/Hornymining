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
    }
}