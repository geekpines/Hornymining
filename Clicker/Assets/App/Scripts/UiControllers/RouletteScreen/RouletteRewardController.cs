using App.Scripts.Gameplay.CoreGameplay.Mining;
using App.Scripts.Gameplay.CoreGameplay.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class RouletteRewardController : MonoBehaviour
{
    [SerializeField] private GameObject _rewardMinerWindow;
    [SerializeField] private GameObject _wheelContainer;
    [SerializeField] private Transform _rewardMinerPosition;
    [SerializeField] private Button _backButton;

    private MinerVisualContext visualContext;

    private PlayerProfile _playerProfile;

    [Inject]
    private void Construct(PlayerProfile playerProfile)
    {
        _playerProfile = playerProfile;
    }

    void Start()
    {
        _playerProfile.OnAllMinersCountChanged += AddMinerToRewardPosition;
        _backButton.onClick.AddListener(Back);
    }

    void AddMinerToRewardPosition(Miner miner)
    {        
        _wheelContainer.SetActive(false);
        _rewardMinerWindow.SetActive(true);
        visualContext = Instantiate(miner.Configuration.Visual, _rewardMinerPosition);
        visualContext.gameObject.transform.localScale = new Vector3(12, 12, 12);
        visualContext.gameObject.transform.localPosition = new Vector3(0, -137, 0);
        
    }

    private void Back()
    {
        Destroy(visualContext.gameObject);
        _wheelContainer.SetActive(true);
        _rewardMinerWindow.SetActive(false);
        
    }
}
