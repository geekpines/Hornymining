using App.Scripts.Gameplay.CoreGameplay.Player;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ShopScreenUiController : MonoBehaviour
{
    [SerializeField] private List<GameObject> _sellBuyUnits;
    [SerializeField] private Button _StockUpgradeButton;
    [SerializeField] private TextMeshProUGUI _levelText;

    private PlayerProfile _playerProfile;
    
    [SerializeField] private LevelShopUpgrades _shopUpgrades;
    
    private string _shopKey = "shop";

    [Inject]
    private void Construct(PlayerProfile playerProfile)
    {
        _playerProfile = playerProfile;
    }

    private void Awake()
    {
        _StockUpgradeButton.onClick.AddListener(OpenTrade);
        SetActiveUnits(false);
        StartCoroutine(LockCoinInfo());
        var k = _shopUpgrades.LoadLevel(_shopKey);

        while (k != 0)
        {
            k--;
            OpenTrade();
        }

        _shopUpgrades.SaveLevel(_shopKey);
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
        if (_shopUpgrades.CurrentLevel < 6 && _playerProfile.TryRemoveScore(_playerProfile.Coins[_shopUpgrades.CurrentLevel - 1].ID, 1))
        {
            _shopUpgrades.OpenSlot(_playerProfile, _sellBuyUnits[_shopUpgrades.CurrentLevel-1]);
            _sellBuyUnits[_shopUpgrades.CurrentLevel-1].GetComponent<CoinsTradeSystemView>().SetUnlock();
            _shopUpgrades.UpdateLevelText();

            foreach (var unit in _sellBuyUnits)
            {
                CoinsTradeSystemView coinTradeSystem = unit.GetComponent<CoinsTradeSystemView>();
                 coinTradeSystem.percent = _shopUpgrades.GetSale();
            }
        }
        
    }

    private IEnumerator LockCoinInfo()
    {
        yield return new WaitForSeconds(0.1f);
        for (int i = 1; i < _sellBuyUnits.Count; i++)
        {
            CoinsTradeSystemView coinTradeSystem = _sellBuyUnits[i].GetComponent<CoinsTradeSystemView>();
            coinTradeSystem.SetLock();
        }
    }

    public void RenewLevelText(LevelShopUpgrades levelShopUpgrades)
    {
        int level = levelShopUpgrades.CurrentLevel;
        _levelText.text = "Level: " + level;
    }
}
