using UnityEngine;

namespace App.Scripts.Utilities.Extensions
{
    public static class TransformExtensions
    {
        /// <summary>
        /// Переместить трансформ на Vector3 относительно его 
        /// текущей позиции.
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="addPosition">Добавить к текущей позиции этот вектор</param>
        public static void Move(this Transform transform, Vector3 addPosition)
        {
            var currentPos = transform.position;
            currentPos += addPosition;
            transform.position = currentPos;
        }
    }
}