using System;
using System.Linq;
using App.Scripts.Gameplay.CoreGameplay.Player;
using App.Scripts.UiControllers.GameScreen.SelectMinersPanel;
using App.Scripts.UiViews.GameScreen.MinersPanel;
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
        
        private MinerSlotView _selectedActiveView;

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
            if (_selectedActiveView != null &&
                !_selectPanel.CheckActiveMiner(id))
            {
                var miner = _playerProfile.GetAllMiners().FirstOrDefault(item => item.ID == id);
                if (miner != null)
                {
                    _playerProfile.AddActiveMiner(miner);
                    _activeSlots.AddMinerToSlot(_selectedActiveView, miner);
                    _activeSlots.SetLock(id, false);
                    _selectPanel.SetMinerActive(id, true);
                }
                ResetLockActiveMinersOnSelectPanel();
            }
        }

        private void ActiveClick(MinerSlotView view)
        {
            Debug.Log($"ActiveClick: {view.name} / Id: {view.Id}");
            if (view.IsEmpty)
            {
                _selectedActiveView = view;
                Debug.Log("Выберите майнера для того, чтобы сделать его активным");
                foreach (var activeMiner in _playerProfile.GetActiveMiners())
                {
                    _selectPanel.SetMinerLock(activeMiner.ID, true);
                }
            }
            else
            {
                _selectedActiveView = null;
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