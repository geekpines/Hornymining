using System;
using System.Linq;
using App.Scripts.Gameplay.CoreGameplay.Player;
using App.Scripts.UiControllers.GameScreen.SelectMinersPanel;
using UnityEngine;
using Zenject;

namespace App.Scripts.UiControllers.GameScreen.MinersPanel
{
    /// <summary>
    /// Класс связывает логику активных майнеров с панелью выбора
    /// майнера.
    /// </summary>
    public class MinerActiveSlotsEventsUiController : MonoBehaviour
    {
        [SerializeField] private MinerActiveSlotsUiController _activeSlots;
        [SerializeField] private MinersSelectPanelUiController _selectPanel;
        private PlayerProfile _playerProfile;
        
        private bool _selectFreeActiveSlot;
        private int _selectedId;

        [Inject]
        private void Construct(PlayerProfile playerProfile)
        {
            _playerProfile = playerProfile;
        }

        private void OnEnable()
        {
            _activeSlots.OnMinerSelected += ActiveClick;
            _selectPanel.OnMinerClicked += MiniViewClick;
        }

        private void OnDisable()
        {
            _activeSlots.OnMinerSelected -= ActiveClick;
            _selectPanel.OnMinerClicked -= MiniViewClick;
        }

        private void MiniViewClick(int id)
        {
            Debug.Log($"MiniViewClick: {id}");
            if (_selectFreeActiveSlot &&
                !_selectPanel.CheckActiveMiner(id))
            {
                var miner = _playerProfile.GetAllMiners().FirstOrDefault(item => item.ID == id);
                if (miner != null)
                {
                    _activeSlots.AddMinerToSlot(_activeSlots.GetView(id), miner);
                    _selectPanel.SetMinerActive(id, true);
                }
                ResetLockActiveMinersOnSelectPanel();
            }
        }

        private void ActiveClick(int id)
        {
            Debug.Log($"ActiveClick: {id}");
            if (_activeSlots.GetAvailableSlot(id))
            {
                _selectFreeActiveSlot = true;

                foreach (var activeMiner in _playerProfile.GetActiveMiners())
                {
                    _selectPanel.SetMinerLock(activeMiner.ID, true);
                }
            }
            else
            {
                _selectFreeActiveSlot = false;
                ResetLockActiveMinersOnSelectPanel();
            }
        }

        private void ResetLockActiveMinersOnSelectPanel()
        {
            foreach (var activeMiner in _playerProfile.GetActiveMiners())
            {
                _selectPanel.SetMinerLock(activeMiner.ID, false);
            }
        }
    }
}