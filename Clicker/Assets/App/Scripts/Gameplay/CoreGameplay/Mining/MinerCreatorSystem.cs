using Assets.App.Scripts.Common;
using Sirenix.OdinInspector;

namespace App.Scripts.Gameplay.CoreGameplay.Mining
{
    /// <summary>
    /// Система с помощью которой можно создать майнера из
    /// конфига
    /// </summary>
    [InfoBox("Система через которую будут создаваться майнеры из " +
             "конфига")]
    public class MinerCreatorSystem : AbstractService<MinerCreatorSystem>
    {
        public Miner CreateMiner(MinerConfiguration configuration, int grade = 3)
        {
            return new Miner(configuration, grade);
        }
    }
}