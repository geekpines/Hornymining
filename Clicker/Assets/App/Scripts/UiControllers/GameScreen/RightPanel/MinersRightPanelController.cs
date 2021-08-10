using System;
using System.Collections.Generic;
using App.Scripts.UiControllers.GameScreen.RightPanel.MinersListPanel;
using App.Scripts.UiViews.GameScreen.MinersListPanel;
using Assets.App.Scripts.Common;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;

namespace App.Scripts.UiControllers.GameScreen.RightPanel
{
    public class MinersRightPanelController : MonoBehaviour
    {
        [SerializeField] private MiniMinerElementsPool _miniMinersPool;
        [SerializeField] private Button _infoButton;
        [SerializeField] private Button _minersButton;
        [SerializeField] private GameObject _panelInfo;
        [SerializeField] private GameObject _panelMinersList;
        private Dictionary<int, MiniMinerElementView> IdtoViews = new Dictionary<int, MiniMinerElementView>();
        
        public class MiniMinerElementData
        {
            public LocalizedString Name { get; }
            public Sprite Icon { get; }
            public int Grade { get; }
            public int Level { get; }
            public int ID { get; }

            public MiniMinerElementData(LocalizedString name, Sprite icon, int grade, int level, int id)
            {
                Name = name;
                Icon = icon;
                Grade = grade;
                Level = level;
                ID = id;
            }
        }

        public void AddMinerInformation(MiniMinerElementData data)
        {
            var minerView = _miniMinersPool.Spawn();
            minerView.SetMinerInformation(
                data.Name,
                data.Icon,
                data.Grade,
                data.Level);
            IdtoViews.Add(data.ID, minerView);
        }

        public void SetMinerLevel(int id, int level)
        {
            if (IdtoViews.ContainsKey(id))
            {
                IdtoViews[id].SetLevel(level);
            }
        }

        private void OnEnable()
        {
            _infoButton.onClick.AddListener(ShowInfo);
            _minersButton.onClick.AddListener(ShowMiners);
        }

        private void OnDisable()
        {
            _infoButton.onClick.RemoveListener(ShowInfo);
            _minersButton.onClick.RemoveListener(ShowMiners);
        }

        private void ShowMiners()
        {
            _panelMinersList.SetActive(true);
            _panelInfo.SetActive(false);
        }

        private void ShowInfo()
        {
            _panelMinersList.SetActive(false);
            _panelInfo.SetActive(true);
        }
    }
}