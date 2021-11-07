using App.Scripts.Gameplay.CoreGameplay.Player;
using App.Scripts.UiControllers.GameScreen.MinersPanel;
using App.Scripts.UiViews.GameScreen.MinersPanel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ActiveMinerUiController : MonoBehaviour
{
    private MinerActiveSlotsUiController minerActive;
    
    private MinerSlotView minerSlotView;
    private PlayerProfile _playerProfile;
    
    

    [Inject]
    private void Construct(PlayerProfile playerProfile)
    {
        _playerProfile = playerProfile;
    }


    private void Awake()
    {
        minerSlotView = gameObject.GetComponent<MinerSlotView>();
        minerActive = GameObject.Find("MinersScroll").GetComponent<MinerActiveSlotsUiController>();
    }

    private void OnEnable()
    {
        //minerSlotView.IsEmpty = true;
        minerActive.MinersSlotView.Add(minerSlotView);
        minerSlotView.SetLock(true);
        gameObject.GetComponent<ActiveMinerUiController>().enabled = false;
    }

}
