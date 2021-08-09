using DragonBones;
using UnityEngine;
using UnityEngine.Localization;

namespace App.Scripts.Foundation
{
    [CreateAssetMenu(fileName = "MinerConfiguration", menuName = "Game/MinerConfiguration", order = 0)]
    public class MinerConfiguration : ScriptableObject
    {
        public LocalizedString Name;
        public LocalizedString Description;
        public Sprite Icon;
        public UnityArmatureComponent Armature;
        //список добываемой валюты
        
    }
}