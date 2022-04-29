using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamEvents : MonoBehaviour
{
    public event Action<string> OnGotThemAll;    //�������
    public event Action<string> OnGotAllBTC;     //�������
    public event Action<string> OnGotAllEther;   //�������
    public event Action<string> OnGotAllLTC;     //�������
    public event Action<string> OnGotAllUsdFork; //�������
    public event Action<string> OnGotAllTokken;  //�������
    public event Action<string> OnGotAllHearted; // �������
    public event Action<string> OnFilledAllActiveSlot; // �������
    public event Action<string> OnFirstLove;      //�������
    public event Action<string> OnFifthStar;      //�������
    public event Action<string> OnAllFifthStar; // �������
    public event Action<string> OnBackInPast; // �������


    private void AchievementUnlock(Action<string> action, string name)
    {
        action?.Invoke(name);
    }

    public void GotAll()
    {
        AchievementUnlock(OnGotThemAll, "GOT_THEM_ALL_ACHIEVEMENT");
    }

    public void GotAllCertain(string name)
    {
        switch (name)
        {
            case "Tokken":
                AchievementUnlock(OnGotAllTokken, "GOT_ALL_" + name.ToUpper());
                break;
            case "Usdfork":
                AchievementUnlock(OnGotAllUsdFork, "GOT_ALL_" + name.ToUpper());
                break;
            case "LTC":
                AchievementUnlock(OnGotAllLTC, "GOT_ALL_" + name.ToUpper());
                break;
            case "Ether":
                AchievementUnlock(OnGotAllEther, "GOT_ALL_" + name.ToUpper());
                break;
            case "BTC":
                AchievementUnlock(OnGotAllBTC, "GOT_ALL_" + name.ToUpper());
                break;
            default:
                break;
        }
        
    }

    public void AllHearted()
    {
        AchievementUnlock(OnGotAllHearted, "ALL_HEARTED");
    }

    public void FillActiveSlots()
    {
        AchievementUnlock(OnFilledAllActiveSlot, "FILLED_ALL_ACTIVE_SLOT");
    }

    public void FirstLove()
    {
        AchievementUnlock(OnFirstLove, "FIRST_LOVE");
    }

    public void FifthStar()
    {
        AchievementUnlock(OnFifthStar, "FIFTH_STAR_GIRL");
    }

    public void AllFifthStar()
    {
        AchievementUnlock(OnAllFifthStar, "ALL_FIFTH_STAR_GIRLS");
    }

    public void BackInPast()
    {
        AchievementUnlock(OnBackInPast, "BACK_IN_PAST");
    }
    
}
