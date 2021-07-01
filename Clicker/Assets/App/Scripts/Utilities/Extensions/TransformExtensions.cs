using UnityEngine;

namespace App.Scripts.Utilities.Extensions
{
    public static class TransformExtensions
    {
        public static void AddToCurrentPosition(this Transform transform, Vector3 addPosition)
        {
            var currentPos = transform.position;
            currentPos += addPosition;
            transform.position = currentPos;
        }
    }
}