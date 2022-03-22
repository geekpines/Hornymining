using App.Scripts.Gameplay.CoreGameplay.Mining;
using App.Scripts.Gameplay.CoreGameplay.Player;
using System.Collections;
using UnityEngine;
using Zenject;

namespace App.Scripts.UiControllers.GameScreen.SelectMinersPanel
{
    /// <summary>
    /// Обновляет информацию о майнерах на панели выбора в
    /// зависимости от изменений информации у игрока
    /// </summary>
    public class SelectPanelInformationUpdater : MonoBehaviour
    {
        [SerializeField] private MinersSelectPanelUiController _minersSelectPanel;
        private PlayerProfile _playerProfile;

        [Inject]
        private void Construct(PlayerProfile playerProfile)
        {
            _playerProfile = playerProfile;
        }

        private void OnEnable()
        {
            _playerProfile.OnAllMinersCountChanged += ChangeMinerInformation;
            _playerProfile.OnActiveMinersCountChanged += ChangeActiveMinerInformation;
        }

        private void OnDisable()
        {
            _playerProfile.OnAllMinersCountChanged -= ChangeMinerInformation;
            _playerProfile.OnActiveMinersCountChanged -= ChangeActiveMinerInformation;
        }

        private void ChangeMinerInformation(Miner miner)
        {
            StartCoroutine(SlowChangeMinerInformation(miner));
        }

        private IEnumerator SlowChangeMinerInformation(Miner miner)
        {
            yield return new WaitForSeconds(0.1f);
            if (_playerProfile.ContainsMiner(miner))
            {
                _minersSelectPanel.AddMinerInformation(new MinersSelectPanelUiController.MiniMinerElementData(
                    miner.Name,
                    miner.Icon,
                    miner.CoinIcon,
                    miner.Grade,
                    miner.Level,
                    miner.ID));
            }
            else
            {
                _minersSelectPanel.RemoveMinerInformation(miner.ID);
            }
        }

        private void ChangeActiveMinerInformation(Miner miner)
        {
            if (_playerProfile.ContainsActiveMiner(miner))
            {
                _minersSelectPanel.SetMinerActive(miner.ID, true);
            }
            else
            {
                _minersSelectPanel.SetMinerActive(miner.ID, false);
            }
        }

        public void InitializationPanel()
        {
            _minersSelectPanel.RemoveAllMinersInformation();
            var allMiners = _playerProfile.GetAllMiners();
            var activeMiners = _playerProfile.GetActiveMiners();
            foreach (var miner in allMiners)
            {
                _minersSelectPanel.AddMinerInformation(new MinersSelectPanelUiController.MiniMinerElementData(
                    miner.Name,
                    miner.Icon,
                    miner.CoinIcon,
                    miner.Grade,
                    miner.Level,
                    miner.ID));
                if (activeMiners.Contains(miner))
                {
                    _minersSelectPanel.SetMinerActive(miner.ID, true);
                }
            }
        }

    }
}