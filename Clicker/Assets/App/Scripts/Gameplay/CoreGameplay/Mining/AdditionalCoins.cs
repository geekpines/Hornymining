using App.Scripts.Gameplay.CoreGameplay.Coins;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalCoins : MonoBehaviour
{
    public CoinType type { get; private set; }
    public float amount { get; private set; }

    public void SetAdditionalCoin(CoinType type, float amount)
    {
        
        this.type = type;
        this.amount = amount;
    }

    
}
