using App.Scripts.Gameplay.CoreGameplay.Player;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ShopScreenUiController : MonoBehaviour
{
    [SerializeField] private UpgradeEvents _openTradeEvent;
    [SerializeField] private List<GameObject> _sellBuyUnits;
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
        _openTradeEvent.OnOpenStockSlot += OpenTrade;
        SetActiveUnits(false);
        StartCoroutine(LockCoinInfo());
        var k = PlayerPrefs.GetInt("HMShopsLevel" + _shopUpgrades.name);
        
        while (k - 1 > 1)
        {
            k--;
            Debug.Log(k);
            LoadOpenTrade();
        }
        
    }

    private void OnDestroy()
    {
        _openTradeEvent.OnOpenStockSlot -= OpenTrade;
    }

    
    private void SetActiveUnits(bool state)
    {
        foreach (var unit in _sellBuyUnits)
        {
            unit.SetActive(state);
        }
    }

    private void OpenTrade(bool flag, int level)
    {
        try
        {
            _sellBuyUnits[level - 1].gameObject.SetActive(flag);
            _sellBuyUnits[level - 1].GetComponent<CoinsTradeSystemView>().SetUnlock();
            _shopUpgrades.UpdateLevelText();

            foreach (var unit in _sellBuyUnits)
            {
                CoinsTradeSystemView coinTradeSystem = unit.GetComponent<CoinsTradeSystemView>();
                coinTradeSystem.percent = _shopUpgrades.GetSale();
            }
        }
        catch (System.ArgumentOutOfRangeException)
        {

            throw;
        }
        
    }

    private void LoadOpenTrade()
    {
        _shopUpgrades.LoadOpenSlot(_sellBuyUnits[_shopUpgrades.CurrentLevel - 1]);
        _sellBuyUnits[_shopUpgrades.CurrentLevel - 1].GetComponent<CoinsTradeSystemView>().SetUnlock();
        _shopUpgrades.UpdateLevelText();

        foreach (var unit in _sellBuyUnits)
        {
            CoinsTradeSystemView coinTradeSystem = unit.GetComponent<CoinsTradeSystemView>();
            coinTradeSystem.percent = _shopUpgrades.GetSale();
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
