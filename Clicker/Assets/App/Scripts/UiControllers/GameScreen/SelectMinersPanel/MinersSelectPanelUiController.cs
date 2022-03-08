using App.Scripts.UiControllers.GameScreen.SelectMinersPanel.MinersListPanel;
using App.Scripts.UiViews.GameScreen.MinersListPanel;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

namespace App.Scripts.UiControllers.GameScreen.SelectMinersPanel
{
    /// <summary>
    /// Контроллер отображения иконок майнеров на панели выбора.
    /// </summary>
    public class MinersSelectPanelUiController : MonoBehaviour
    {
        public event Action<int> OnMinerClicked;
        public event Action<MiniMinerElementView> OnMinerDoubleClicked;
        [SerializeField] private MiniMinerElementsPool _miniMinersPool;
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
                data.Icon,
                data.ID, 3, data.Level + 1);
            
            IdtoViews.Add(data.ID, minerView);
            minerView.OnMinerClicked += MinerClicked;
            minerView.OnMinerDoubleClicked += MinerDoubleClicked;
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
        /// Установить флаг блокировки
        /// </summary>
        /// <param name="id"></param>
        /// <param name="state"></param>
        public void SetMinerLock(int id, bool state)
        {
            if (IdtoViews.ContainsKey(id))
            {
                IdtoViews[id].SetLockMask(state);
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
                IdtoViews[id].OnMinerClicked -= MinerClicked;
                IdtoViews[id].OnMinerDoubleClicked -= MinerDoubleClicked;

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

        /// <summary>
        /// Проверить, активен ли майнер
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool CheckActiveMiner(int id)
        {
            if (IdtoViews.ContainsKey(id))
            {
                return IdtoViews[id].IsActive;
            }
            return false;
        }

        private void MinerClicked(MiniMinerElementView sender)
        {
            OnMinerClicked?.Invoke(sender.ID);
        }

        private void MinerDoubleClicked(MiniMinerElementView sender)
        {
            OnMinerDoubleClicked?.Invoke(sender);
        }

        public void SetHearts(int id)
        {
            IdtoViews[id].SetHearts();
        }

        public int GetHearts(int id)
        {
            return IdtoViews[id].GetHearts();
        }
    }
}