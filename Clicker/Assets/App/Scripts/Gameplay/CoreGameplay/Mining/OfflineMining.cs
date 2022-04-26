using App.Scripts.Gameplay.CoreGameplay.Coins;
using App.Scripts.Gameplay.CoreGameplay.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class OfflineMining : MonoBehaviour
{
    DateTime start;
    DateTime end;
    TimeSpan result;

    void Start()
    {
        LoadData();
    }

    
    public IEnumerator SaveData()
    {
        yield return new WaitForSeconds(10);
        start = DateTime.Now;
        PlayerPrefs.SetString("HMTime", start.ToString());
        PlayerPrefs.Save();
        StartCoroutine(SaveData());
    }

    void LoadData()
    {
        end = DateTime.Now;
        string time = PlayerPrefs.GetString("HMTime");
        if (time != null && time != "")
        {
            start = DateTime.ParseExact(time, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture) ;
            result = start - end;
        }
    }

    public void AddScore(PlayerProfile playerProfile, CoinType coinType)
    {
        int k = (int)result.TotalHours;
        float score = playerProfile.GetAllMiners().Count * 0.25f;

        while (k != 0 )
        {
            if (k < 24)
            {
                playerProfile.AddScore(coinType, score);
            }
            k--;
            
        }
    }

    private void OnDestroy()
    {
        SaveData();
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }
}
