using Steamworks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steamtest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!SteamManager.Initialized)
        {
            return;
        }
        string name = SteamFriends.GetPersonaName();
        Debug.Log(name);
        //SteamUserStats.SetAchievement("SPEEN_WHEEL"); https://partner.steamgames.com/doc/features/achievements/ach_guide 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
