using System.Collections.Generic;
using System.Linq;
using Assets.App.Scripts.Common;

namespace App.Scripts.Foundation
{
    public class PlayerProfile : AbstractService<PlayerProfile>
    {
        public List<CoinData> Coins = new List<CoinData>();
        public int CoinLevelChance;
        
        private void Awake()
        {
            //todo: добавить сохранение
            Coins.Clear();
            var temp = new List<CoinData>();
            foreach (var coin in CoinsInformation.GetElements())
            {
                temp.Add(new CoinData(coin.ID));
            }
            Coins = temp.OrderBy(data => data.ID).ToList();
        }

    }
}