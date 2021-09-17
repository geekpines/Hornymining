using App.Scripts.Foundation.Roulette.Core;
using App.Scripts.Gameplay.CoreGameplay.Mining;
using UnityEngine;

namespace App.Scripts.Gameplay.MetaGameplay.Roulette
{
    [CreateAssetMenu(fileName = "RouletteConfiguration", menuName = "Game/Settings/RouletteConfigurations", order = 0)]
    public class RouletteMinerConfiguration : RouletteConfiguration<MinerConfiguration>
    {
    }
}