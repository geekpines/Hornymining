using System.Collections.Generic;

namespace App.Scripts.Common
{
    /// <summary>
    /// Класс содержит в себе все элементы подписки и
    /// предоставляет интерфейс взаимодействия с ними.
    /// </summary>
    public sealed class ObserverHandleManager
    {
        /// <summary>
        /// Количество подписок
        /// </summary>
        public int Count { get => handles.Count; }
        List<ObserverHandle> handles = new List<ObserverHandle>();

        /// <summary>
        /// Получить пустой элемент подписки
        /// </summary>
        /// <returns></returns>
        public ObserverHandle Alloc()
        {
            var handle = new ObserverHandle();
            Add(handle);
            return handle;
        }

        /// <summary>
        /// Добавить элемент подписки в список
        /// </summary>
        /// <param name="handle"></param>
        public void Add(ObserverHandle handle)
        {
            handles.Add(handle);
        }

        /// <summary>
        /// Создать новый элемент подписки и добавить себя в
        /// лист подписок
        /// </summary>
        /// <param name="observable">Лист подписок</param>
        /// <param name="observer">Подписчик</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ObserverHandle Observe<T>(IObserverList<T> observable, T observer)
        {
            ObserverHandle handle = null;
            Observe(ref handle, observable, observer);
            return handle;
        }

        /// <summary>
        /// Создать новый элемент подписки и добавить себя в
        /// лист подписок
        /// </summary>
        /// <param name="handle">Элемент подписки</param>
        /// <param name="observable">Лист подписок</param>
        /// <param name="observer">Подписчик</param>
        /// <typeparam name="T"></typeparam>
        public void Observe<T>(ref ObserverHandle handle, IObserverList<T> observable, T observer)
        {
            observable.Add(ref handle, observer);
            handle.List = observable;
            Add(handle);
        }

        /// <summary>
        /// Удалить себя из списка подписок
        /// </summary>
        /// <param name="handle"></param>
        public void Unobserve(ObserverHandle handle)
        {
            if (handle != null) {
                handle.Dispose();
                handles.Remove(handle);
            }
        }

        /// <summary>
        /// Отчистить список элементов подписок
        /// </summary>
        public void Clear()
        {
            foreach (var handle in handles)
                handle.Dispose();
            handles.Clear();
        }
    }
}
