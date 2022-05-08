using App.Scripts.Gameplay.CoreGameplay.Mining;
using App.Scripts.Gameplay.CoreGameplay.Player;
using App.Scripts.UiViews.GameScreen.MinersPanel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;
using UnityEngine.UI.Extensions.Examples.FancyScrollViewExample03;
using Zenject;

namespace App.Scripts.UiControllers.GameScreen.MinersPanel
{
    /// <summary>
    /// Класс управляет ячейками майнеров на сцене (активные майнеры).
    /// Отвечает за отображение контента.
    /// </summary>
    public class MinerActiveSlotsUiController : MonoBehaviour
    {
        [SerializeField] private ScrollerExtension extension;
        [SerializeField] private Button RefreshMinerUI;

        [SerializeField] private LevelShopUpgrades _shopLevel;
        private string _slotKey = "slot";

        public event Action<MinerSlotView> OnMinerSelected;
        [SerializeField] private ExtendedScrollView _scrollMinerView;
        private PlayerProfile _playerProfile;

        public List<MinerSlotView> MinersSlotView { get; private set; } = new List<MinerSlotView>();
        private Dictionary<int, MinerSlotView> IdToView = new Dictionary<int, MinerSlotView>();

        [SerializeField] private UpgradeEvents _upgradeEvents;

        [Inject]
        private void Construct(PlayerProfile playerProfile)
        {
            _playerProfile = playerProfile;
        }

        private void Awake()
        {

            _scrollMinerView = gameObject.GetComponent<ExtendedScrollView>();
            extension = gameObject.GetComponent<ScrollerExtension>();

            
            //todo: добавить в систему инициализации
            StartCoroutine( InitializationActiveMiners());
            _upgradeEvents.OnMinerOpenSlot += MinersViewController; //Функция для открытия слотов майнера, тоже переделать
            //RefreshMinerUI.onClick.AddListener(UpdateVisual);
            int k = PlayerPrefs.GetInt("HMShopsLevel" + _shopLevel.gameObject.name) ;
            StartCoroutine(LoadMinersViews(k-1));
            
        }


        private IEnumerator InitializationActiveMiners()
        {
            yield return new WaitForSeconds(0.1f);
            InitializeScroll();
            InitializeViews();
            InitializeContent();

        }

        private void InitializeScroll()
        {
            //todo: заменить максимальное начение, на значение из конфига
            var items = Enumerable.Range(0, 5)
                .Select(i => new ItemData())
                .ToArray();
            _scrollMinerView.UpdateData(items);
            _scrollMinerView.SelectCell(0);
        }

        private void InitializeViews()
        {
            foreach (var element in _scrollMinerView.Elements)
            {
                if (element.TryGetComponent(out MinerSlotView view))
                {
                    
                    MinersSlotView.Add(view);
                    view.OnMinerClicked += MinerClicked;
                }
            }
        }

        private void InitializeContent()
        {
            MinersSlotView[0].IsOpen = true;
            MinersSlotView[0].SetVisible();
        }

        private void MinerClicked(MinerSlotView sender)
        {
            OnMinerSelected?.Invoke(sender);
        }

        public void AddMinerToSlot(MinerSlotView viewSlot, Miner miner)
        {


            if (viewSlot != null &&
                viewSlot.IsEmpty)
            {
                if (IdToView.ContainsKey(miner.ID))
                    return;

                var visual = Instantiate(
                    miner.Configuration.Visual,
                    viewSlot.RootPosition);

                viewSlot.SetVisual(
                    visual.gameObject,
                    visual.ArmatureComponent,
                    miner.ID);

                IdToView.Add(miner.ID, viewSlot);
                viewSlot.SetLock(false);

                viewSlot.SetLevelVisual(miner.Level);
                viewSlot.SetName(miner.Name);
            }

        }

        public void RemoveSlot(MinerSlotView viewSlot)
        {
            if (!viewSlot.IsEmpty)
            {
                viewSlot.DestroyVisual();
                viewSlot.SetLock(true);
            }
        }

        public void ClearSlot(MinerSlotView viewSlot)
        {
            if (!viewSlot.IsEmpty)
            {
                viewSlot.SetLock(true);
                Destroy(viewSlot.gameObject);
            }
        }

        public MinerSlotView GetView(int id)
        {
            if (IdToView.ContainsKey(id))
            {
                return IdToView[id];
            }
            return null;
        }

        public void SetLock(int id, bool state)
        {
            if (IdToView.ContainsKey(id))
            {
                IdToView[id].SetLock(state);
            }
        }

        private void OnDestroy()
        {
            foreach (var minerSlotView in MinersSlotView)
            {
                minerSlotView.OnMinerClicked -= MinerClicked;
            }
        }

        private void MinersViewController(bool flag)
        {
            if (flag && _shopLevel.CurrentLevel - 1 < MinersSlotView.Count)
            {
                try
                {
                    MinersSlotView[_shopLevel.CurrentLevel - 1].IsOpen = flag;
                    MinersSlotView[_shopLevel.CurrentLevel - 1].SetVisible();
                    _shopLevel.UpdateLevelText();

                }
                catch (ArgumentOutOfRangeException)
                {

                    throw;
                }
                
            }  
        }

        private IEnumerator LoadMinersViews(int level)
        {
            yield return new WaitForSeconds(0.5f);
            for (int i = 1; i < level && level != 1; i++)
            {
                _shopLevel.LevelUp();
                _shopLevel.UpdateLevelText();
                MinersSlotView[_shopLevel.CurrentLevel-1].IsOpen = true;
                MinersSlotView[_shopLevel.CurrentLevel-1].SetVisible();
                
            }
        }


        public void UpdateVisual()
        {
            var allActiveMiners = _playerProfile.GetActiveMiners();
            for (int i = 0; i < allActiveMiners.Count; i++)
            {
                for (int j = 0; j < allActiveMiners.Count; j++)
                {
                    if(MinersSlotView[j].GetMinerName() == allActiveMiners[i].Name)
                    {
                        CheckForVisualUpdate(MinersSlotView[j], allActiveMiners[i]);
                    }
                }
            }
        }

        private void CheckForVisualUpdate(MinerSlotView viewSlot, Miner miner)
        {
            if (viewSlot != null &&
               !viewSlot.IsEmpty)
            {
                //Debug.Log(miner.Name.GetLocalizedString() +" Lvl: " + miner.Level);
                //Debug.Log(viewSlot.name);
                //Debug.Log("_________________________");
                viewSlot.SetLevelVisual(miner.Level);
            }
        }

        
    }
}