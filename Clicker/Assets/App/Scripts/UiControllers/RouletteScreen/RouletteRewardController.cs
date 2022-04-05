using App.Scripts.Gameplay.CoreGameplay.Mining;
using App.Scripts.Gameplay.CoreGameplay.Player;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;
using Zenject;

public class RouletteRewardController : MonoBehaviour
{
    [SerializeField] private GameObject _costDialogue;
    [SerializeField] private GameObject _rewardMinerWindow;
    [SerializeField] private GameObject _wheelContainer;
    [SerializeField] private Transform _rewardMinerPosition;
    [SerializeField] private Button _backButton;
    [SerializeField] private TextMeshProUGUI _coinInfo;
    [SerializeField] private TextMeshProUGUI _spinCost;
    

    [Title("Miner Info Elements")]
    [SerializeField] private LocalizeStringEvent minerName;
    [SerializeField] private LocalizeStringEvent minerDescription;
    [SerializeField] private Button getButton;

    private MinerVisualContext visualContext;


    private PlayerProfile _playerProfile;

    [Inject]
    private void Construct(PlayerProfile playerProfile)
    {

        _playerProfile = playerProfile;
        
    }

    private void OnEnable()
    {
        _costDialogue.SetActive(true);

        _playerProfile.OnAllMinersCountChanged += AddMinerToRewardPosition;
        _playerProfile.OnAllMinersCountChanged += AddMinerInfo;
        _wheelContainer = GameObject.FindGameObjectWithTag("WheelContainer");
        _backButton.onClick.AddListener(Back);
        getButton.onClick.AddListener(Back);
    }

    private void Update()
    {
        _coinInfo.text = _playerProfile.Coins[5].Value.ToString();
    }

    private void OnDisable()
    {
        _playerProfile.OnAllMinersCountChanged -= AddMinerToRewardPosition;
        _playerProfile.OnAllMinersCountChanged -= AddMinerInfo;
        _backButton.onClick.RemoveListener(Back);
        getButton.onClick.RemoveListener(Back);
    }
    private void AddMinerToRewardPosition(Miner miner)
    {
        _wheelContainer.SetActive(false);
        _rewardMinerWindow.SetActive(true);
        visualContext = Instantiate(miner.Configuration.Visual, _rewardMinerPosition);
        visualContext.gameObject.transform.localScale = new Vector3(22, 22, 22);
        visualContext.gameObject.transform.localPosition = new Vector3(0, -240, 0);
        
    }

    private void AddMinerInfo(Miner miner)
    {
        minerName.StringReference = miner.Name;
        minerDescription.StringReference = miner.Description;
    }

    private void Back()
    {
        if (visualContext != null)
        {
            Destroy(visualContext.gameObject);
        }        
        _wheelContainer.SetActive(true);
        _costDialogue.SetActive(true);
        _rewardMinerWindow.SetActive(false);
        
    }

    
    
}
