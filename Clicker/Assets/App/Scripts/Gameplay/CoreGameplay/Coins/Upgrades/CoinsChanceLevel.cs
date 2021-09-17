using System.Collections.Generic;
using Assets.App.Scripts.Common;

namespace App.Scripts.Gameplay.CoreGameplay.Coins.Upgrades
{
    public class CoinsChanceLevel : AbstractService<CoinsChanceLevel>
    {
        public List<CoinsLevelChance> Levels = new List<CoinsLevelChance>();
    }
}