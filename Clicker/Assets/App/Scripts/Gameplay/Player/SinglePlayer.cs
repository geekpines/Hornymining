using System.Collections.Generic;
using App.Scripts.Gameplay.Coins;
using App.Scripts.Utilities.MonoBehaviours;

namespace App.Scripts.Gameplay.Player
{
    public class SinglePlayer : Singleton<SinglePlayer>
    {
        public List<CoinData> Coins = new List<CoinData>();

        protected override void OnAwake()
        {
            //todo: добавить сохранение
            Coins.Clear();
            foreach (var coin in CoinsInformation.GetElements())
            {
                Coins.Add(new CoinData(coin.ID));
            }
        }
    }
}