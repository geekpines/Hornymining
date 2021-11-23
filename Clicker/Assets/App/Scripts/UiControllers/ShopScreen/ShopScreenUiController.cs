using App.Scripts.Gameplay.CoreGameplay.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ShopScreenUiController : MonoBehaviour
{
    [SerializeField] private List<GameObject> _sellBuyUnits;
    [SerializeField] private Button _StockUpgradeButton;
    private PlayerProfile _playerProfile;
    private LevelShopUpgrades shopUpgrades = new LevelShopUpgrades();


    [Inject]
    private void Construct(PlayerProfile playerProfile)
    {
        _playerProfile = playerProfile;
    }

    private void Awake()
    {
        _StockUpgradeButton.onClick.AddListener(OpenTrade);
        SetActiveUnits(false);  
    }

    private void SetActiveUnits(bool state)
    {
        foreach (var unit in _sellBuyUnits)
        {
            unit.SetActive(state);
        }
    }

    private void OpenTrade()
    {
        if (shopUpgrades.CurrentLevel < 5)
        {
            shopUpgrades.OpenSlot(_playerProfile, _sellBuyUnits[shopUpgrades.CurrentLevel]);
        }
        
        foreach (var unit in _sellBuyUnits)
        {
            CoinsTradeSystemView coinTradeSystem = unit.GetComponent<CoinsTradeSystemView>();

            coinTradeSystem.percent = shopUpgrades.GetSale();
        }
        
    }
}
