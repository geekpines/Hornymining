using System;
using Random = UnityEngine.Random;

namespace App.Scripts.Common
{
    public static class UniqueID
    {
        /// <summary>
        /// Сгенерировать уникальный ID
        /// </summary>
        /// <returns>ID</returns>
        public static int Generate()
        {
            return Random.Range(Int32.MinValue, Int32.MaxValue).GetHashCode();
        }
    }
}