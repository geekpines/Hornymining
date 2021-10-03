using System.Collections.Generic;
using App.Scripts.UiControllers.GameScreen.SelectMinersPanel.MinersListPanel;
using App.Scripts.UiViews.GameScreen.MinersListPanel;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;

namespace App.Scripts.UiControllers.GameScreen.SelectMinersPanel
{
    /// <summary>
    /// Контроллер отображения иконок майнеров на панели выбора.
    /// </summary>
    public class MinersSelectPanelUiController : MonoBehaviour
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

        /// <summary>
        /// Добавить на панель нового майнера
        /// </summary>
        /// <param name="data"></param>
        public void AddMinerInformation(MiniMinerElementData data)
        {
            var minerView = _miniMinersPool.Spawn();
            minerView.SetMinerInformation(
                data.Name,
                data.Icon);
            IdtoViews.Add(data.ID, minerView);
        }
        
        /// <summary>
        /// Установить уровень майнера
        /// </summary>
        /// <param name="id"></param>
        /// <param name="level"></param>
        public void SetMinerLevel(int id, int level)
        {
            if (IdtoViews.ContainsKey(id))
            {
                IdtoViews[id].SetLevel(level);
            }
        }
        
        /// <summary>
        /// Установить флаг активности
        /// </summary>
        /// <param name="id"></param>
        /// <param name="active"></param>
        public void SetMinerActive(int id, bool active)
        {
            if (IdtoViews.ContainsKey(id))
            {
                IdtoViews[id].SetUseMask(active);
            }
        }

        /// <summary>
        /// Скрыть майнера на панели
        /// </summary>
        /// <param name="id"></param>
        public void RemoveMinerInformation(int id)
        {
            if (IdtoViews.ContainsKey(id))
            {
                _miniMinersPool.Despawn(IdtoViews[id]);
                IdtoViews.Remove(id);
            }
        }
        
        /// <summary>
        /// Скрыть всех майнеров с панели
        /// </summary>
        public void RemoveAllMinersInformation()
        {
            var keyList = new List<int>();
            foreach (var idtoViewsKey in IdtoViews.Keys)
            {
                keyList.Add(idtoViewsKey);
            }

            foreach (var key in keyList)
            {
                RemoveMinerInformation(key);
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