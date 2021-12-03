using System;

namespace App.Scripts.Common
{
    /// <summary>
    /// Базовый элемент системы подписки. Позволяет
    /// хранить индекс и лист в котором находится.
    /// </summary>
    public sealed class ObserverHandle : IDisposable
    {
        internal int Index;
        internal IObserverList List;

        /// <summary>
        /// Удалить себя из списка
        /// </summary>
        public void Dispose()
        {
            if (List != null)
            {
                List.Remove(this);
            }
        }
    }
}
