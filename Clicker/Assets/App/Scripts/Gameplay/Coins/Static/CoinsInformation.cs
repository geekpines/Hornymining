using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace App.Scripts.Foundation
{
    public class CoinsInformation
    {
        public static float GetBonus(Coin from, Coin to)
        {
            return CoinsTableSetting.Instance.BonusElement[from][to];
        }

        public static List<Coin> GetElements()
        {
            return CoinsTableSetting.Instance.Elements;
        }

        public static Coin GetCoinInformation(int id)
        {
            return CoinsTableSetting.Instance.Elements.FirstOrDefault(coin => coin.ID == id);
        }

        public static Sprite GetCoinIcon(int id)
        {
            return GetCoinInformation(id).Icon;
        }
    }
} 