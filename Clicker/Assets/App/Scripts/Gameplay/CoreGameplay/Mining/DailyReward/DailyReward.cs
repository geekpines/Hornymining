using App.Scripts.Gameplay.CoreGameplay.Coins;
using App.Scripts.Gameplay.CoreGameplay.Mining;
using App.Scripts.Gameplay.CoreGameplay.Player;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Zenject;

public class DailyReward : MonoBehaviour
{

    [SerializeField] MinerConfiguration minerConfiguration;
    private int day = 1;
    private double hourLeft = 0;
    private PlayerProfile _playerProfile;
    private string key = "HMDailyTime";
    private string hourKey = "HMHtime";
    private string leftKey = "HMTLeft";

    public event Action<int> dayLeft;


    [Inject]
    private void Construct(PlayerProfile playerProfile)
    {
        _playerProfile = playerProfile;
    }

    void Start()
    {       
        
        if (DayLeft())
        {
            switch (day)
            {
                case 1:
                    MiningUp(1.5f);
                    break;
                case 2:
                    AddCoin(CoinType.Tokken, 15);
                    break;
                case 3:
                    MinerUp();
                    break;
                case 4:
                    AddCoin(CoinType.Usdfork, 15);
                    break;
                case 5:
                    MiningUp(1.5f);
                    break;
                case 6:
                    AddCoin(CoinType.LTC, 15);
                    break;
                case 7:
                    AddExtraMiner();
                    break;
                default:
                    break;
            }
        }
        CheckTime();
    }
    
    void MiningUp(float added)
    {
        _playerProfile.percentUpgrade += added;
        PlayerPrefs.SetString(hourKey, DateTime.Now.ToString());
        PlayerPrefs.Save();
        StartCoroutine(CheckTime());
    }

    void MinerUp()
    {
        _playerProfile.GetAllMiners()[UnityEngine.Random.Range(0, _playerProfile.GetAllMiners().Count)].LevelUp();
    }

    void AddExtraMiner()
    {
        MinerCreatorSystem minerCreator = new MinerCreatorSystem();
        _playerProfile.AddMiner(minerCreator.CreateMiner(minerConfiguration));
    }

    void AddCoin(CoinType coinType, float count)
    {
        _playerProfile.AddScore(coinType, count);
    }

    public bool DayLeft()
    {
        var time = PlayerPrefs.GetString(key);
        Debug.Log(time);
        if(time != null && time != "")
        {
            DateTime today = DateTime.Now;
            DateTime yesterday = Convert.ToDateTime(time, CultureInfo.InvariantCulture);//DateTime.ParseExact(time, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            TimeSpan result = today - yesterday;
            if(PlayerPrefs.GetFloat(leftKey) != 0)
            {
                hourLeft = PlayerPrefs.GetFloat(leftKey);
            }
            
            
            if (hourLeft > 24 && day < 7)
            {
                day++;
                Day();
                hourLeft = 0;
                return true;                
            }
            else if (day < 7)
            {
                hourLeft += result.TotalHours;
                PlayerPrefs.SetFloat(leftKey, (float)hourLeft);
                PlayerPrefs.Save();
            }
        }
        else
        {
            PlayerPrefs.SetString(key, DateTime.Now.ToString());
            PlayerPrefs.Save();
        }
        return false;
    }

    IEnumerator CheckTime()
    {
        string time = PlayerPrefs.GetString(hourKey);
        yield return new WaitForSeconds(60f);
        if (time != null)
        {
            TimeSpan timeSpan = DateTime.Now - DateTime.ParseExact(time, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);

            if(timeSpan.TotalHours < 1)
            {
                StartCoroutine(CheckTime());
            }
            else
            {
                MiningUp(-1.5f);
                PlayerPrefs.DeleteKey(hourKey);
            }
        }
    }

    public void Day()
    {
        dayLeft?.Invoke(day);
    }
}
