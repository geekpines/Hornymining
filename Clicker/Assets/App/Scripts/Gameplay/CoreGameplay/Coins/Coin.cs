using UnityEngine;
using UnityEngine.Localization;

namespace App.Scripts.Foundation
{
    [CreateAssetMenu(fileName = "Coin", menuName = "Game/Coins/Coin", order = 0)]
    public class Coin : ScriptableObject
    {
        public LocalizedString Name;
        public LocalizedString Description;
        public Sprite Icon;
        public CoinType ID;
    }
}
