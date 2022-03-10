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
    
    private LevelShopUpgrades shopUpgrades = new LevelShopUpgrades();
    
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
        var k = shopUpgrades.LoadLevel(_shopKey);

        while (k != 0)
        {
            k--;
            OpenTrade();
        }

        shopUpgrades.SaveLevel(_shopKey);
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
            _sellBuyUnits[shopUpgrades.CurrentLevel].GetComponent<CoinsTradeSystemView>().SetUnlock();
            _levelText.text = "Level: " + shopUpgrades.CurrentLevel + 1;


            foreach (var unit in _sellBuyUnits)
            {
                CoinsTradeSystemView coinTradeSystem = unit.GetComponent<CoinsTradeSystemView>();

                coinTradeSystem.percent = shopUpgrades.GetSale();
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
}
