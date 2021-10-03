﻿using System;
using App.Scripts.Gameplay.CoreGameplay.Mining;
using App.Scripts.Gameplay.CoreGameplay.Player;
using App.Scripts.UiControllers.GameScreen.SelectMinersPanel;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace App.Scripts.UiControllers.Services
{
    public class PlayerSelectPanelUiController : MonoBehaviour
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
            if (_playerProfile.ContainsMiner(miner))
            {
                _minersSelectPanel.AddMinerInformation(new MinersSelectPanelUiController.MiniMinerElementData(
                    miner.Name,
                    miner.Icon,
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