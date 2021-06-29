﻿using System.Collections.Generic;
using App.Scripts.Utilities.MonoBehaviours;
using UnityEngine;

namespace App.Scripts.Gameplay.Coins
{
    public class CoinsInformation : Singleton<CoinsInformation>
    {
        public static float GetBonus(Coin from, Coin to)
        {
            return CoinsTable.Instance.BonusElement[from][to];
        }

        public static List<Coin> GetElements()
        {
            return CoinsTable.Instance.Elements;
        }

    }
} 