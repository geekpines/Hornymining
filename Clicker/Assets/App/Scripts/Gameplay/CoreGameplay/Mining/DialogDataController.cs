using App.Scripts.Gameplay.CoreGameplay.Coins;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;


namespace App.Scripts.Gameplay.CoreGameplay.Mining
{
    [CreateAssetMenu(fileName = "Dialog_Data", menuName = "Game/Dialog_Data", order = 0)]
    public class DialogDataController : ScriptableObject
    {
        [Title("MinerConfig")]
        public MinerConfiguration MinerConf;
        
        [Title ("English dialogs")]
        public List<DialogLevel> Dialogs;

        [Title("Дополнительные реплики")]
        public List<DialogLevel> additionalDialogs = new List<DialogLevel>();

        [Title("Miner Sounds")]
        public List<AudioClip> lines;

        [Serializable]
        public class DialogLevel
        {
            public List<LocalizedString> Dialog;
        }

    }
}