using System.ComponentModel.Design;
using App.Scripts.Common;
using UnityEngine;

namespace Assets.App.Scripts.Common
{
    public class GameLoop : AbstractService<GameLoop>
    {
        public ObserverList<IOnFixedUpdate> OnFixedUpdate { get; } = new ObserverList<IOnFixedUpdate>();
        public ObserverList<IOnLateUpdate> OnLateUpdate { get; } = new ObserverList<IOnLateUpdate>();
        public ObserverList<IOnUpdate> OnUpdate { get; } = new ObserverList<IOnUpdate>();
        
        void Update()
        {
            float timeDelta = Time.deltaTime;
            foreach (var ticker in OnUpdate.Enumerate())
                ticker.Do(timeDelta);
        }

        void FixedUpdate()
        {
            foreach (var ticker in OnFixedUpdate.Enumerate())
                ticker.Do();
        }

        void LateUpdate()
        {
            float timeDelta = Time.deltaTime;
            foreach (var ticker in OnLateUpdate.Enumerate())
                ticker.Do(timeDelta);
        }
    }
}