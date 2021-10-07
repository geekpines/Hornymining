using System.Collections;
using System.Collections.Generic;
using App.Scripts.Gameplay.CoreGameplay.Coins.Static;
using App.Scripts.Gameplay.CoreGameplay.Mining;
using App.Scripts.Gameplay.CoreGameplay.Player;
using App.Scripts.UiViews.GameScreen.MinersPanel;
using UnityEngine;
using Zenject;

namespace App.Scripts.UiControllers.GameScreen.MinersPanel
{
    /// <summary>
    /// Система автоматической добычи валюты (пассивный майнинг)
    /// </summary>
    public class AutoMiningSystem : MonoBehaviour
    {
        [SerializeField] private MinerActiveSlotsUiController _minerActiveSlotsUiController;
        private PlayerProfile _playerProfile;
        private Dictionary<int, Miner> IdToActiveMiner = new Dictionary<int, Miner>();
        private Dictionary<Miner, IEnumerator> ActiveMinerToTimer = new Dictionary<Miner, IEnumerator>();
        private Dictionary<Miner, MinerSlotView> ActiveMinerToView = new Dictionary<Miner, MinerSlotView>();
        
        [Inject]
        private void Construct(PlayerProfile playerProfile)
        {
            _playerProfile = playerProfile;
        }
        
        private void OnEnable()
        {
            _playerProfile.OnActiveMinersCountChanged += MinerChanged;
        }
        
        private void OnDisable()
        {
            _playerProfile.OnActiveMinersCountChanged -= MinerChanged;
        }
        
        private void MinerChanged(Miner miner)
        {
            if (IdToActiveMiner.ContainsKey(miner.ID))
            {
                RemoveMiner(miner);
            }
            else
            {
                AddMiner(miner);
            }
        }

        private IEnumerator StartMining(Miner miner)
        {
            while (true)
            {
                yield return new WaitForSeconds(miner.Configuration.Levels[miner.Level].PeriodAutoMining);
                foreach (var miningResource in miner.Configuration.Levels[miner.Level].MiningResources)
                {
                    _playerProfile.AddScore(miningResource.Type, miningResource.Value);
                    if (ActiveMinerToView.ContainsKey(miner))
                    {
                        ActiveMinerToView[miner].ShowScoreLine(
                            CoinsInformation.GetCoinIcon(miningResource.Type),
                            miningResource.Value);
                    }
                }
            }
        }

        private void AddMiner(Miner miner)
        {
            if (IdToActiveMiner.ContainsKey(miner.ID) ||
                ActiveMinerToView.ContainsKey(miner) ||
                ActiveMinerToTimer.ContainsKey(miner))
            {
                Debug.LogWarning($"Не удалось добавить майнера {miner.Name.GetLocalizedString()} в автодобычу!");
                return;
            }
            
            IdToActiveMiner.Add(miner.ID, miner);
            ActiveMinerToTimer.Add(miner, StartMining(miner));
            
            var view = _minerActiveSlotsUiController.GetView(miner.ID);
            if (view != null)
            {
                ActiveMinerToView.Add(miner, view);
            }
            
            StartCoroutine(ActiveMinerToTimer[miner]);
        }

        private void RemoveMiner(Miner miner)
        {
            if (!IdToActiveMiner.ContainsKey(miner.ID) &&
                !ActiveMinerToView.ContainsKey(miner) &&
                !ActiveMinerToTimer.ContainsKey(miner))
            {
                Debug.LogWarning($"Не удалось удалить майнера {miner.Name.GetLocalizedString()} из автодобычи!");
                return;
            }
            
            StopCoroutine(ActiveMinerToTimer[miner]);
            ActiveMinerToTimer[miner] = null;

            ActiveMinerToTimer.Remove(miner);
            ActiveMinerToView.Remove(miner);
            IdToActiveMiner.Remove(miner.ID);
        }

    }
}