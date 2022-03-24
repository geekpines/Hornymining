using App.Scripts.Gameplay.CoreGameplay.Mining;
using App.Scripts.Gameplay.CoreGameplay.Player;
using App.Scripts.UiControllers.GameScreen.SelectMinersPanel;
using App.Scripts.UiViews.GameScreen.MinersPanel;
using System.Collections;
using System.Collections.Generic;
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
        [SerializeField] AudioSource audioSource;

        private int _countsActiveClick = 0;
        private int _maxCountsActiveClick = 100;
        private List<AdditionalCoins> _additionalCoins;

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
            //Debug.Log($"ActiveClick: {view.name} / Id: {view.Id}");
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
            //Выпадение рандомной валюты при surprise button upgrade
            if(_additionalCoins != null)
            {
                foreach (var additionalCoin in _additionalCoins)
                {
                    AddSpecialScore(additionalCoin);
                }
            }            
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
                        int dialogRand = Random.Range(0, dialog.Dialogs.Count);
                        view.dialogUiController.SetName(dialog.MinerConf.Name);
                        view.dialogUiController.OpenRuDialogContent(true, dialog.Dialogs[heartsCount-1].Dialog[Random.Range(0,4)]);
                        yield return new WaitForSeconds(3);
                        view.dialogUiController.SetOff(false);
                        MinerSoundStart(dialog.lines[Random.Range(0, dialog.lines.Count)]);
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

                if (_countsActiveClick == _maxCountsActiveClick)
                {
                    _countsActiveClick = 0;
                    _maxCountsActiveClick *= 10;
                    foreach (var activeMiner in _playerProfile.GetActiveMiners())
                    {
                        if (activeMiner.ID == view.Id)
                        {
                            SetHearts(view, activeMiner);
                            PopOffDialog(view, 0, activeMiner);
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


        private void AddSpecialScore(AdditionalCoins additionalCoin)
        {
            if (Random.Range(0, 100) <= additionalCoin.chance)
            {
                _playerProfile.AddScore(additionalCoin.type, 1f);
            }
            
            
        }
        public void AddAdditionalCoin(AdditionalCoins additionalCoin)
        {
            _additionalCoins.Add(additionalCoin);
        }

        public void MinerSoundStart(AudioClip clip)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }



    }
}