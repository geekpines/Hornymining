using System;
using System.Collections.Generic;
using System.Linq;
using App.Scripts.Gameplay.CoreGameplay.Coins.Static;
using App.Scripts.Gameplay.CoreGameplay.Mining;
using App.Scripts.Gameplay.CoreGameplay.Player;
using App.Scripts.UiControllers.GameScreen.SelectMinersPanel;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace App.Scripts.UiControllers.GameScreen.MinerInformationPanel
{
    public class MinerInformationPanelUiController : MonoBehaviour
    {
        public event Action OnShowPanel;
        public event Action OnHidePanel;
        public event Action OnNextMiner;
        public event Action OnPreviousMiner;
        
        [SerializeField] private MinersSelectPanelUiController _minersSelectPanelUiController;
        [SerializeField] private CostViewPool _costViewPool;
        private PlayerProfile _playerProfile;
        
        [Title("Кнопки")]
        [SerializeField] private Button _levelUpButton;
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _nextMiner;
        [SerializeField] private Button _previousMiner;

        [Title("Элементы панели")]
        [SerializeField] private Transform _minerRootPosition;
        private MinerVisualContext _visualMiner;
        private bool _isShow;
        
        [Inject]
        private void Construct(PlayerProfile playerProfile)
        {
            _playerProfile = playerProfile;
        }
        
        private void OnEnable()
        {
            _minersSelectPanelUiController.OnMinerDoubleClicked += ShowInformation;
            _backButton.onClick.AddListener(HideInformation);
        }

        private void OnDisable()
        {
            _minersSelectPanelUiController.OnMinerDoubleClicked -= ShowInformation;
            _backButton.onClick.RemoveListener(HideInformation);
        }

        private void ShowInformation(int idMiner)
        {
            if (!TryInitializationMinerVisual(idMiner))
                return;
            InitializationUpgradeCosts(idMiner);
            if (!_isShow)
            {
                _isShow = true;
                OnShowPanel?.Invoke();
            }
        }

        private bool TryInitializationMinerVisual(int idMiner)
        {
            if (_visualMiner != null)
            {
                Destroy(_visualMiner);
            }

            var miner = _playerProfile.GetAllMiners().FirstOrDefault(targetMiner => targetMiner.ID == idMiner);
            if (miner == null)
            {
                Debug.LogError("Попытка отобразить майнера, ID которого нет у игрока");
                return false;
            }

            _visualMiner = Instantiate(
                miner.Configuration.Visual, 
                _minerRootPosition);

            return true;
        }

        private void InitializationUpgradeCosts(int idMiner)
        {
            _levelUpButton.gameObject.SetActive(true);
            var costInformation = new List<CostViewPool.CoinInformation>();
            var miner = _playerProfile.GetAllMiners().FirstOrDefault(targetMiner => targetMiner.ID == idMiner);
            if (miner.Level + 1 <= miner.Configuration.Levels.Count)
            {
                var costs = miner.Configuration.Levels[miner.Level + 1].UpgradeCost;
                foreach (var cost in costs)
                {
                    costInformation.Add(new CostViewPool.CoinInformation(
                        CoinsInformation.GetCoinIcon(cost.Type),
                        cost.Value));
                }
                _costViewPool.ShowCosts(costInformation);
            }
            else
            {
                _levelUpButton.gameObject.SetActive(false);
            }
        }

        private void HideInformation()
        {
            _isShow = false;
            OnHidePanel?.Invoke();
        }
    }
}