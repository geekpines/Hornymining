using App.Scripts.Gameplay.CoreGameplay.Coins;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalCoins : MonoBehaviour
{
    public CoinType type { get; private set; }
    public float chance { get; private set; } = 0;

    public float amount { get; private set; }

    public void SetAdditionalCoin(CoinType type, float chance, float amount)
    {
        this.chance = chance;
        this.type = type;
        this.amount = amount;
    }

    
}
