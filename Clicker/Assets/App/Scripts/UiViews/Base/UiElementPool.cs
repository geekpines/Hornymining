using App.Scripts.Utilities.MonoBehaviours;
using System.Collections.Generic;
using UnityEngine;

namespace App.Scripts.UiViews.Base
{
    public class UiElementPool<T> : MonoBehaviour where T : MonoBehaviour
    {
        public delegate void ElementPoolHandler(T sender);

        public event ElementPoolHandler Spawned;
        public event ElementPoolHandler Despawned;

        public List<T> Elements => _elements;

        public int PoolSize { get => _pool.Count; }

        [SerializeField] private List<T> _elements = new List<T>();
        [SerializeField] private T _prefabUi;
        [SerializeField] private int _poolSize;
        private PoolObject<T> _pool;

        private void Awake()
        {
            _pool = new PoolObject<T>(_prefabUi, _poolSize, this.transform, true);
        }

        public T Spawn()
        {
            T spawnedElement;
            do
            {
                spawnedElement = _pool.GetObject();
            } while (_elements.Contains(spawnedElement));
            _elements.Add(spawnedElement);

            Spawned?.Invoke(spawnedElement);
            return spawnedElement;
        }

        public void Spawn(int count)
        {
            for (int i = 0; i < count; i++)
            {
                Spawn();
            }
        }

        public void Despawn(T element)
        {
            if (_elements.Contains(element))
            {
                _elements.Remove(element);
                _pool.ReturnObject(element);
                Despawned?.Invoke(element);
            }
        }

        public void DestroyObj(T element)
        {
            if (_elements.Contains(element))
            {
                _elements.Remove(element);
                _pool.DestroyObject(element);
                Despawned?.Invoke(element);
            }
        }

        public void DespawnAll()
        {
            var chachedElements = new List<T>();

            foreach (var element in _elements)
            {
                chachedElements.Add(element);
            }

            foreach (var element in chachedElements)
            {
                Despawn(element);
            }
            _elements.Clear();
        }
    }
}
