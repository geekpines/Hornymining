﻿using App.Scripts.Gameplay.CoreGameplay.Mining;
using App.Scripts.Gameplay.CoreGameplay.Player;
using App.Scripts.UiControllers.GameScreen.SelectMinersPanel;
using App.Scripts.UiViews.GameScreen.MinersPanel;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
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
        [SerializeField] Button stock;

        [SerializeField] DialogContainer dataContainer;

        private int _countsActiveClick = 0;

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

            if (_selectedActiveView != null &&
                !_selectPanel.CheckActiveMiner(id) &&
                _selectedActiveView.IsEmpty &&
                _selectedActiveView.IsOpen
                )
            {
                var miner = _playerProfile.GetAllMiners().FirstOrDefault(item => item.ID == id);
                if (miner != null)
                {
                    _activeSlots.AddMinerToSlot(_selectedActiveView, miner);
                    _activeSlots.SetLock(id, false);
                    _selectPanel.SetMinerActive(id, true);
                    _playerProfile.AddActiveMiner(miner);

                }
                ResetLockActiveMinersOnSelectPanel();
            }
            else
            {
                Debug.LogError("Слот для майнера занят");
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
            //Открытие случайных диалогов для майнера
            OpenDialog(view);
            //увеличение количества сердечек
            AddHeartCounts(view);
        }


        private void ResetLockActiveMinersOnSelectPanel()
        {
            foreach (var activeMiner in _playerProfile.GetActiveMiners())
            {
                _selectPanel.SetMinerLock(activeMiner.ID, false);
            }


        }


        private IEnumerator PopOffDialog(MinerSlotView view, int rand, Miner activeMiner)
        {
            int heartsCount = _selectPanel.GetHearts(activeMiner.ID);
            if (rand < 2)
            {
                foreach (var dialog in dataContainer.dialogDataControllers)
                {
                    if (view.CheckName(dialog.MinerConf.Name))
                    {
                        int dialogRand = Random.Range(0, dialog.Dialogs_Ru[heartsCount].Dialog.Count-1);
                        view.dialogUiController.SetName(dialog.MinerConf.Name);
                        view.dialogUiController.OpenRuDialogContent(true, dialog.Dialogs_Ru[heartsCount-1].Dialog[dialogRand]);
                        yield return new WaitForSeconds(3);
                        view.dialogUiController.SetOff(false);
                    }
                }
            }
            
        }

        private void SetHearts(MinerSlotView view, Miner activeMiner)
        {
            _selectPanel.SetHearts(activeMiner.ID);
        }

        private void OpenDialog(MinerSlotView view)
        {
            int rand = Random.Range(0, 100);
            foreach (var activeMiner in _playerProfile.GetActiveMiners())
            {
                if (activeMiner.ID == view.Id)
                {
                    StartCoroutine(PopOffDialog(view, rand, activeMiner));
                }
            }
        }

        private void AddHeartCounts(MinerSlotView view)
        {
            if (!view.IsEmpty)
            {
                _countsActiveClick++;

                if (_countsActiveClick == 100)
                {
                    _countsActiveClick = 0;
                    foreach (var activeMiner in _playerProfile.GetActiveMiners())
                    {
                        if (activeMiner.ID == view.Id)
                        {
                            SetHearts(view, activeMiner);
                        }
                    }

                }
            }
        }

        public void SpecialDialogPopOff(MinerSlotView view)
        {
            int rand = Random.Range(0, 100);
            foreach (var activeMiner in _playerProfile.GetActiveMiners())
            {
                if (activeMiner.ID == view.Id)
                {
                    StartCoroutine(PopOffDialog(view, rand, activeMiner));
                }
            }
        }
    }
}