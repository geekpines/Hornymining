using App.Scripts.Gameplay.CoreGameplay.Coins.Static;
using App.Scripts.Gameplay.CoreGameplay.Mining;
using App.Scripts.Gameplay.CoreGameplay.Player;
using App.Scripts.UiControllers.GameScreen.MinersPanel;
using App.Scripts.UiControllers.GameScreen.SelectMinersPanel;
using App.Scripts.Utilities.MonoBehaviours;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
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
        [SerializeField] private Button _sellMiner;
        [SerializeField] private Button _removeMiner;


        [Title("Элементы панели")]
        [SerializeField] private Transform _minerRootPosition;

        [SerializeField] private ForceRebuildLayout _rebuildLayout;

        [SerializeField] private LocalizeStringEvent Miner_name;
        [SerializeField] private LocalizeStringEvent Miner_description;

        [SerializeField] private DialogContainer dataContainer;

        [SerializeField] private DialogUiController dialogUiController;

        [Title ("Сторонние элементы")]
        [SerializeField] private MinerActiveSlotsUiController minerActiveSlotsUiController;
        
        private Miner _currentMiner;
        private bool _isShow;

        private int _outsideMinerId;


        private Dictionary<Miner, MinerVisualContext> MinerToVisual = new Dictionary<Miner, MinerVisualContext>();

        [Inject]
        private void Construct(PlayerProfile playerProfile)
        {
            _playerProfile = playerProfile;
        }

        private void OnEnable()
        {
            
            _minersSelectPanelUiController.OnMinerDoubleClicked += ShowInformation;
            _backButton.onClick.AddListener(HideInformation);
            _levelUpButton.onClick.AddListener(LevelUpClicked);
            _sellMiner.onClick.AddListener(MinerSeller);
            _removeMiner.onClick.AddListener(RemoveMiner);
        }

        private void OnDisable()
        {
            _minersSelectPanelUiController.OnMinerDoubleClicked -= ShowInformation;
            _backButton.onClick.RemoveListener(HideInformation);
            _levelUpButton.onClick.RemoveListener(LevelUpClicked);
            _sellMiner.onClick.RemoveListener(MinerSeller);
        }

        private void ShowInformation(int idMiner)
        {

            if (!TryInitializationMinerVisual(idMiner))
                return;
            InitializationUpgradeCosts(idMiner);

            //значение внешнего id майнера
            _outsideMinerId = idMiner;

            if (!_isShow)
            {
                _isShow = true;
                OnShowPanel?.Invoke();
            }
        }

        private bool TryInitializationMinerVisual(int idMiner)
        {
            _currentMiner = _playerProfile.GetAllMiners().FirstOrDefault(targetMiner => targetMiner.ID == idMiner);
            if (_currentMiner == null)
            {
                Debug.LogError("Попытка отобразить майнера, ID которого нет у игрока");
                return false;
            }

            if (!MinerToVisual.ContainsKey(_currentMiner))
            {
                var visualContext = Instantiate(
                    _currentMiner.Configuration.Visual,
                    _minerRootPosition);
                visualContext.transform.localScale = new Vector3(12, 12, 12);
                visualContext.transform.localPosition = new Vector3(0, -137, 0);
                _currentMiner.OnLevelUp += visualContext.UnlockComponents.SetUnlockLevel;
                MinerToVisual.Add(_currentMiner, visualContext);
                SetNameAndDescriprion(_currentMiner.Name, _currentMiner.Description);
            }
            MinerToVisual[_currentMiner].gameObject.SetActive(true);
            return true;
        }

        private void InitializationUpgradeCosts(int idMiner)
        {
            _levelUpButton.gameObject.SetActive(true);
            var costInformation = new List<CostViewPool.CoinInformation>();
            _currentMiner = _playerProfile.GetAllMiners().FirstOrDefault(targetMiner => targetMiner.ID == idMiner);
            if (_currentMiner != null && _currentMiner.Level + 1 < _currentMiner.Configuration.Levels.Count)
            {
                var costs = _currentMiner.Configuration.Levels[_currentMiner.Level + 1].UpgradeCost;
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
            _rebuildLayout.ForceRebuild();
        }

        private void HideInformation()
        {

            _isShow = false;
            HideMiner();
            OnHidePanel?.Invoke();
        }

        private void LevelUpClicked()
        {
            if (_currentMiner.Level + 1 > _currentMiner.Configuration.Levels.Count)
            {
                Debug.Log("У вас максимальный уровень!");
                return;
            }

            var costs = _currentMiner.Configuration.Levels[_currentMiner.Level + 1].UpgradeCost;
            if (CheckPossibleLevelUp(costs))
            {
                Debug.Log("Уровень повышен!");
                DecreaseResources(costs);
                _currentMiner.LevelUp();
                StartCoroutine(PopOffDialog(_currentMiner));
                _minersSelectPanelUiController.SetMinerLevel(_outsideMinerId, _currentMiner.Level + 1);
                InitializationUpgradeCosts(_currentMiner.ID);
            }
            else
            {
                Debug.Log("Недостаточно средств для повышения уровня!");
            }
        }

        private bool CheckPossibleLevelUp(List<MinerConfiguration.MiningResource> costs)
        {
            var isPossible = true;
            foreach (var cost in costs)
            {
                if (!_playerProfile.TryRemoveScore(cost.Type, cost.Value))
                {
                    isPossible = false;
                }
            }
            return isPossible;
        }

        private void DecreaseResources(List<MinerConfiguration.MiningResource> costs)
        {
            foreach (var cost in costs)
            {
                _playerProfile.RemoveScore(cost.Type, cost.Value);
            }
        }

        private void HideMiner()
        {
            if (MinerToVisual.ContainsKey(_currentMiner))
            {
                MinerToVisual[_currentMiner].gameObject.SetActive(false);
            }
        }

        private void SetNameAndDescriprion(LocalizedString name, LocalizedString description)
        {
            Miner_name.StringReference = name;
            Miner_description.StringReference = description;
        }

        private IEnumerator PopOffDialog(Miner activeMiner)
        {

            foreach (var dialog in dataContainer.dialogDataControllers)
            {
                if (_currentMiner.Name == dialog.MinerConf.Name)
                {
                    int dialogRand = UnityEngine.Random.Range(0, dialog.additionalDialogs[0].Dialog.Count);
                    dialogUiController.SetName(dialog.MinerConf.Name);
                    dialogUiController.OpenRuDialogContent(true, dialog.additionalDialogs[0].Dialog[dialogRand]);
                    yield return new WaitForSeconds(3);
                    dialogUiController.SetOff(false);
                }
            }
            
        }

        private void MinerSeller()
        {
            foreach (var miner in _playerProfile.GetActiveMiners())
            { 
                if(miner.ID == _outsideMinerId)
                {
                    minerActiveSlotsUiController.RemoveSlot(minerActiveSlotsUiController.GetView(miner.ID));
                    _playerProfile.RemoveMiner(miner);                    
                    HideInformation();
                    _playerProfile.AddScore(miner.Configuration.Levels[0].MiningResources[0].Type, miner.Configuration.Levels[0].MiningResources[0].Value * 100);
                    //_minersSelectPanelUiController.SetMinerLock(miner.ID, true);
                }
            }                
        }

        private void RemoveMiner()
        {
            foreach (var miner in _playerProfile.GetActiveMiners())
            {
                if (miner.ID == _outsideMinerId)
                {
                    minerActiveSlotsUiController.RemoveSlot(minerActiveSlotsUiController.GetView(miner.ID));
                    //_playerProfile.RemoveMiner(miner);
                    HideInformation();
                }
            }
        }
    }
}