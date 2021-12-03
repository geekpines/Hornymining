using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace App.Scripts.Gameplay.CoreGameplay.Coins.Static
{
    public static class CoinsInformation
    {
        public static float GetBonus(Coin from, Coin to)
        {
            return CoinsTableSetting.Instance.BonusElement[from][to];
        }

        public static List<Coin> GetElements()
        {
            return CoinsTableSetting.Instance.Elements;
        }

        public static Coin GetCoinInformation(CoinType id)
        {
            return CoinsTableSetting.Instance.Elements.FirstOrDefault(coin => coin.ID == id);
        }

        public static Sprite GetCoinIcon(CoinType id)
        {
            return GetCoinInformation(id).Icon;
        }
    }
}