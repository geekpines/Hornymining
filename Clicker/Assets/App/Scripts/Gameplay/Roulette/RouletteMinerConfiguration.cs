using App.Scripts.Foundation;
using App.Scripts.Foundation.Roulette.Core;
using UnityEngine;

namespace App.Scripts.Gameplay.Roulette
{
    [CreateAssetMenu(fileName = "RouletteConfiguration", menuName = "Game/Settings/RouletteConfigurations", order = 0)]
    public class RouletteMinerConfiguration : RouletteConfiguration<MinerConfiguration>
    {
    }
}