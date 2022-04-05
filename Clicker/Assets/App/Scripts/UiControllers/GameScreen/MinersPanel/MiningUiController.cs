using App.Scripts.Gameplay.CoreGameplay.Coins.Static;
using App.Scripts.Gameplay.CoreGameplay.Mining;
using App.Scripts.Gameplay.CoreGameplay.Player;
using App.Scripts.UiViews.GameScreen.MinersPanel;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace App.Scripts.UiControllers.GameScreen.MinersPanel
{
    public class MiningUiController : MonoBehaviour
    {
        //todo: логику класса в идеале нужно перенести в Gameplay

        [SerializeField] private MinerActiveSlotsUiController _minerActiveSlotsUiController;
        private PlayerProfile _playerProfile;

        private Dictionary<int, Miner> IdToMiner = new Dictionary<int, Miner>();

        [Inject]
        private void Construct(PlayerProfile playerProfile)
        {
            _playerProfile = playerProfile;
        }

        private void Start()
        {
            //будет актуально, когда будет сделано сохранение/загрузка прогресса
            foreach (var miner in _playerProfile.GetActiveMiners())
            {
                if (!IdToMiner.ContainsKey(miner.ID))
                {
                    IdToMiner.Add(miner.ID, miner);
                }
            }
        }

        private void OnEnable()
        {
            _minerActiveSlotsUiController.OnMinerSelected += MinerClicked;
            _playerProfile.OnActiveMinersCountChanged += MinerChanged;
        }

        private void OnDisable()
        {
            _minerActiveSlotsUiController.OnMinerSelected -= MinerClicked;
            _playerProfile.OnActiveMinersCountChanged -= MinerChanged;
        }

        private void MinerClicked(MinerSlotView view)
        {
            if (IdToMiner.ContainsKey(view.Id))
            {
                var miner = IdToMiner[view.Id];
                foreach (var miningResource in miner.Configuration.Levels[miner.Level].MiningResources)
                {
                    _playerProfile.AddScore(miningResource.Type, miningResource.Value * _playerProfile.percentUpgrade);
                    view.ShowScoreLine(
                        CoinsInformation.GetCoinIcon(miningResource.Type),
                        miningResource.Value * _playerProfile.percentUpgrade);

                }
            }
        }

        private void MinerChanged(Miner miner)
        {
            if (IdToMiner.ContainsKey(miner.ID))
            {
                IdToMiner.Remove(miner.ID);
            }
            else
            {
                IdToMiner.Add(miner.ID, miner);
            }
        }
       public void RemoveMinerFromActiveMining(Miner miner)
        {
            if (IdToMiner.ContainsKey(miner.ID))
            {
                IdToMiner.Remove(miner.ID);
            }
        }
    }
}