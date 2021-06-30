using System.Collections.Generic;
using App.Scripts.Gameplay.Coins;
using Assets.App.Scripts.Common;

namespace App.Scripts.Gameplay.Player
{
    public class PlayerProfile : AbstractService<PlayerProfile>
    {
        public List<CoinData> Coins = new List<CoinData>();

        private void Awake()
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