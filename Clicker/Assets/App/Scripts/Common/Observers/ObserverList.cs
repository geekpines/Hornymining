using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.Scripts.Common
{
    /// <summary>
    /// Список подписчиков определенного типа
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class ObserverList<T> : IObserverList<T>
    {
        /// <summary>
        /// Структура элемента подписок
        /// </summary>
        struct Item
        {
            public ObserverHandle Handle;
            public T Observer;
        }

        /// <summary>
        /// Количество подписчиков
        /// </summary>
        public int ObserverCount => _items.Count;

        List<Item> _items = new List<Item>();
        List<Item> _chachedItems = new List<Item>();

        void IObserverList.Add(ref ObserverHandle handle, object observer)
        {
            if (observer is T typedObserver)
                Add(ref handle, typedObserver);
            else
            {
                if (observer == null)
                    Debug.LogError("Observer is null.");
                else
                    Debug.LogError($"Was expecting type {typeof(T)}, got type {observer.GetType().Name}.");
            }
        }

        /// <summary>
        /// Добавить подписчика в список
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="observer"></param>
        public void Add(ref ObserverHandle handle, T observer)
        {
            if (handle == null)
                handle = new ObserverHandle();

            Debug.Assert(observer != null, "Observer is null.");
            Debug.Assert(handle.List == null, "Handle is already in use.");

            handle.Index = _items.Count;
            _items.Add(new Item { Handle = handle, Observer = observer });
        }

        /// <summary>
        /// Удалить подписчика из списка
        /// </summary>
        /// <param name="handle"></param>
        public void Remove(ObserverHandle handle)
        {
            Debug.Assert(handle != null, "Handle is null.");
            Debug.Assert(handle.List == this, "Handle is not registered with this list.");

            int lastIndex = _items.Count - 1;
            if (handle.Index != lastIndex)
            {
                var replacement = _items[lastIndex];
                _items[handle.Index] = replacement;
                replacement.Handle.Index = handle.Index;
            }

            handle.List = null;
            _items.RemoveAt(lastIndex);
        }

        IEnumerable IObserverList.Enumerate()
        {
            return Enumerate();
        }

        /// <summary>
        /// Перечислитель всех подписчиков
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> Enumerate()
        {
            _chachedItems.Clear();
            _chachedItems.AddRange(_items);
            foreach (var item in _chachedItems)
                yield return item.Observer;
        }
    }
}
