using App.Scripts.Gameplay.CoreGameplay.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class WarningWindowController : MonoBehaviour
{
    [SerializeField] private GameObject _warningWindow;

    PlayerProfile _playerProfile;

    [Inject]
    private void Construct(PlayerProfile playerProfile)
    {
        _playerProfile = playerProfile;
    }
    // Start is called before the first frame update
    void Start()
    {
        _playerProfile.OnMoneyNotEnough += StartWarning;
    }

    private void OnDestroy()
    {
        _playerProfile.OnMoneyNotEnough -= StartWarning;
    }
    private void StartWarning(bool start)
    {
        if (start)
        {
            _warningWindow.SetActive(start);
        }
    }
}
