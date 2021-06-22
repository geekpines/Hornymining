using App.Scripts.UiControllers.GameScreen;
using UnityEngine;

namespace App.Scripts.Gameplay.Mining
{
    public class Miner : MonoBehaviour
    {
        public MinerConfiguration Configuration { get; private set; }
        [SerializeField] private MinerController _minerController;

        public void SetConfiguration(MinerConfiguration configuration)
        {
            Configuration = configuration;
            //todo: reinit all views
        }



    }
}