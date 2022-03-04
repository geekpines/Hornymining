using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyRewardWarning : MonoBehaviour
{

    [SerializeField] private GameObject _warningWindow;
    [SerializeField] private DailyReward dailyReward;

    private void Start()
    {
        dailyReward.dayLeft += StartWarning;
    }

    private void StartWarning(int start)
    {

        _warningWindow.SetActive(true);
        
    }
}
