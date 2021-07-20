using System;
using System.Collections;
using UnityEngine;

namespace App.Scripts.Common
{
    /// <summary>
    /// Набор стандартных корутин
    /// </summary>
    public static class StandartCoroutines
    {
        /// <summary>
        /// Вызвать метод в конце фрейма
        /// </summary>
        /// <param name="method">Целевой метод</param>
        /// <returns></returns>
        public static IEnumerator InvokeEndFrame(Action method)
        {
            yield return new WaitForEndOfFrame();
            method?.Invoke();
        }
    }
}
