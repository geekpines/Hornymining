using System.Collections.Generic;
using Assets.App.Scripts.Common;
using UnityEngine;

namespace App.Scripts.Foundation.Upgrades
{
    public class CoinsChanceLevel : AbstractService<CoinsChanceLevel>
    {
        public List<CoinsLevelChance> Levels = new List<CoinsLevelChance>();
    }
}