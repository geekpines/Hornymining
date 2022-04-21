using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeEvents : MonoBehaviour
{
    public event Action OnCasualUpgrade;
    public event Action<int> OnLevelUpInt;
    public event Action<bool> OnMinerOpenSlot;
    public event Action<bool, int> OnOpenStockSlot;
    public event Action<List<AdditionalCoins>> OnSurpriseUpgraded;

    public void CasualUpgraded()
    {
        OnCasualUpgrade?.Invoke();
    }
    public void levelUpped(int number)
    {
        OnLevelUpInt?.Invoke(number);
    }

    public void MinerSlotOppened(bool flag)
    {
        OnMinerOpenSlot?.Invoke(flag);
    }

    public void SurpriseUpgrade(List<AdditionalCoins> coins)
    {
        OnSurpriseUpgraded?.Invoke(coins);
    }

    public void StockOpened(bool flag, int level)
    {
        OnOpenStockSlot?.Invoke(flag, level);
    }
}
