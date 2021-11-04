using App.Scripts.Gameplay.CoreGameplay.Player;
using App.Scripts.UiControllers.GameScreen.MinersPanel;
using App.Scripts.UiViews.GameScreen.MinersPanel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ActiveMinerUiController : MonoBehaviour
{
    [SerializeField] private MinerActiveSlotsUiController minerActiveSlotsUi;
    private MinerSlotView minerSlotView;
    private PlayerProfile _playerProfile;
        

    [Inject]
    private void Construct(PlayerProfile playerProfile)
    {
        _playerProfile = playerProfile;
    }


    private void Awake()
    {
        minerActiveSlotsUi = GameObject.Find("MinersScroll").GetComponent<MinerActiveSlotsUiController>();
    }

    private void OnEnable()
    {
        Debug.Log(minerActiveSlotsUi.MinersSlotView.Count);
    }

}
