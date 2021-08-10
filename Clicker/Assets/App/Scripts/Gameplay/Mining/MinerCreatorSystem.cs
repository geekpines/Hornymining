using App.Scripts.Foundation.Interfaces;
using Assets.App.Scripts.Common;

namespace App.Scripts.Foundation
{
    public class MinerCreatorSystem : AbstractService<MinerCreatorSystem>
    {
        public Miner CreateMiner(MinerConfiguration configuration, int grade)
        {
            return new Miner(configuration, grade);
        }
    }
}