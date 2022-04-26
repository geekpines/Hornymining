using Steamworks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamTest : MonoBehaviour
{
    [SerializeField] private SteamEvents _achievementEvents;

    void Start()
    {
        if (!SteamManager.Initialized)
        {
            return;
        }
        string name = SteamFriends.GetPersonaName();
        _achievementEvents.OnAllFifthStar += SetAchievement;
        _achievementEvents.OnBackInPast += SetAchievement;
        _achievementEvents.OnFifthStar += SetAchievement;
        _achievementEvents.OnAllFifthStar += SetAchievement;
        _achievementEvents.OnFilledAllActiveSlot += SetAchievement;
        _achievementEvents.OnFirstLove += SetAchievement;
        _achievementEvents.OnGotAllBTC += SetAchievement;
        _achievementEvents.OnGotAllEther += SetAchievement;
        _achievementEvents.OnGotAllHearted += SetAchievement;
        _achievementEvents.OnGotAllTokken += SetAchievement;
        _achievementEvents.OnGotAllUSDC += SetAchievement;
        _achievementEvents.OnGotAllLTC += SetAchievement;
    }


    private void SetAchievement(string nameAchivement)
    {
        bool a;
        SteamUserStats.GetAchievement(nameAchivement, out a);
        
        if (a)
        {
            SteamUserStats.SetAchievement(nameAchivement); // https://partner.steamgames.com/doc/features/achievements/ach_guide 
        }
    }

    private void OnDestroy()
    {
        _achievementEvents.OnAllFifthStar -= SetAchievement;
        _achievementEvents.OnBackInPast -= SetAchievement;
        _achievementEvents.OnFifthStar -= SetAchievement;
        _achievementEvents.OnAllFifthStar -= SetAchievement;
        _achievementEvents.OnFilledAllActiveSlot += SetAchievement;
        _achievementEvents.OnFirstLove -= SetAchievement;
        _achievementEvents.OnGotAllBTC -= SetAchievement;
        _achievementEvents.OnGotAllEther -= SetAchievement;
        _achievementEvents.OnGotAllHearted -= SetAchievement;
        _achievementEvents.OnGotAllTokken -= SetAchievement;
        _achievementEvents.OnGotAllUSDC -= SetAchievement;
        _achievementEvents.OnGotAllLTC -= SetAchievement;
    }
}
