using System.Collections.Generic;
using Assets.App.Scripts.Common;
using Sirenix.OdinInspector;

namespace App.Scripts.Gameplay.CoreGameplay.Coins.Upgrades
{
    [InfoBox("Общие настройки зависимости добычи той или иной валюты от " +
             "уровня")]
    public class CoinsChanceLevel : AbstractService<CoinsChanceLevel>
    {
        public List<CoinsLevelChance> Levels = new List<CoinsLevelChance>();
    }
}