using UnityEngine;

namespace App.Scripts.Foundation
{
    [CreateAssetMenu(fileName = "Coin", menuName = "Game/Coins/Coin", order = 0)]
    public class Coin : ScriptableObject
    {
        public string Name;
        public string Description;
        public Sprite Icon;
        //todo: сделать генератор энамов
        public int ID;
    }
}
