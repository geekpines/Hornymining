using App.Scripts.Gameplay.CoreGameplay.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class ResetGame : MonoBehaviour
{
    private PlayerProfile _playerProfile;
    [SerializeField] Button yesButton;

    [Inject]
    private void Construct(PlayerProfile playerProfile)
    {
        _playerProfile = playerProfile;
    }

    private void Start()
    {
        yesButton.onClick.AddListener(GameReset);
    }

    private void GameReset()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        _playerProfile.ResetPlayer(_playerProfile);
        //GameObject[] shopUpgrades = GameObject.FindGameObjectsWithTag("ShopUpgrade");
        SceneManager.LoadScene("Loading");
    }




}
