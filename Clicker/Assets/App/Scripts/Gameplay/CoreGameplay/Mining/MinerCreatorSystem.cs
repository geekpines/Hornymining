using Assets.App.Scripts.Common;

namespace App.Scripts.Gameplay.CoreGameplay.Mining
{
    public class MinerCreatorSystem : AbstractService<MinerCreatorSystem>
    {
        public Miner CreateMiner(MinerConfiguration configuration, int grade)
        {
            return new Miner(configuration, grade);
        }
    }
}