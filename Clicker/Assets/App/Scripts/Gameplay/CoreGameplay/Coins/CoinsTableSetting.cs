using System;
using System.Collections.Generic;
using System.Linq;
using Alex.Common.tools;
using App.Scripts.Foundation.Coins;
using App.Scripts.Utilities.MonoBehaviours;
using UnityEngine;

namespace App.Scripts.Foundation
{
    [CreateAssetMenu(fileName = "CoinsTable", menuName = "Game/Settings/CoinsTable", order = 0)]
    public class CoinsTableSetting : ElementsTable<Coin>
    {
        private static CoinsTableSetting _instance;

        /// <summary>
        /// Получить конфиг коина по его ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Coin FindCoinToID(CoinType id)
        {
            return Elements.FirstOrDefault(coin => coin.ID == id);
        }

        /// <summary>
        /// Получить список всех коинов
        /// </summary>
        /// <returns></returns>
        public List<Coin> GetAllCoins()
        {
            return Elements;
        }

        /// <summary>
        /// Получить ссылку на экземпляр ScriptableObject
        /// </summary>
        public static CoinsTableSetting Instance
        {
            get
            {
                if (_instance == null)
                {
                    var path = $"Settings/{typeof(CoinsTableSetting).Name}";
                    _instance = Resources.Load(path) as CoinsTableSetting;
                }

                return _instance;
            }
        }
        
#if UNITY_EDITOR
        
        //todo: добавить генерацию

        // /// <summary>
        // /// Путь для генерации CoinType
        // /// </summary>
        // private const string PathEnum = "Assets/Scripts/Gameplay/Coins/CoinType.cs";
        //
        // [ContextMenu("Add Coin Type")]
        // public void InitializationNewItem()
        // {
        //     var arrayID = new List<string>();
        //     var path = $"Settings/HeroInformationDataBase";
        //     var heroInformationDataBase = Resources.Load(path) as HeroInformationDataBase;
        //
        //     heroInformationDataBase.TryAddHero(this);
        //     
        //     if (heroInformationDataBase == null)
        //     {
        //         Debug.LogError("HeroInformationDataBase не существует!");
        //         return;
        //     }
        //     
        //     for (int i = 0; i < heroInformationDataBase.AllHeroesInformations.Count - 1; i++)
        //     {
        //         if (!arrayID.Contains(heroInformationDataBase.AllHeroesInformations[i].HeroId.ToString()))
        //         {
        //             arrayID.Add(heroInformationDataBase.AllHeroesInformations[i].HeroId.ToString());
        //         }
        //     }
        //     
        //     if (!arrayID.Contains(_name))
        //     {
        //         arrayID.Add(_name);
        //     }
        //     
        //     var e1 = new CreateEnum.EnumClass(arrayID, "HeroID");
        //     CreateEnum.CreateEnumsInFile(PathEnum, e1);
        //     this._heroId = (HeroID) heroInformationDataBase.AllHeroesInformations.Count - 1;
        //     
        //     heroInformationDataBase.RefreshCollections();
        // }
        
#endif
    }
}