using App.Scripts.Gameplay.CoreGameplay.Coins;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalCoins : MonoBehaviour
{
    public CoinType type { get; private set; }
    public int chance { get; private set; } = 0;


    public void SetAdditionalCoin(CoinType type, int chance)
    {
        this.chance = chance;
        this.type = type;
    }

    public void SetAdditionalCoin(CoinType type, float chance)
    {
        while(chance < 1)
        {
            chance *= 10;
        }
        this.chance = (int)chance;
        this.type = type;
    }
}
