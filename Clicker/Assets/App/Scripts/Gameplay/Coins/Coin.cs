using Unity.Collections;
using UnityEngine;

namespace App.Scripts.Gameplay.Coins
{
    [CreateAssetMenu(fileName = "Coin", menuName = "Game/Coins/Coin", order = 0)]
    public class Coin : ScriptableObject
    {
        public string Name;
        public string Description;
        public Sprite Icon;
        public int ID { get => Icon.GetHashCode(); }
    }
}
