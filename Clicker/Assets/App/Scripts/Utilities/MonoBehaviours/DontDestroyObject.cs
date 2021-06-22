using UnityEngine;

namespace App.Scripts.Utilities.MonoBehaviours
{
    /// <summary>
    /// Сделать объект неуничтожаемым при
    /// переходах между сценами
    /// </summary>
    public class DontDestroyObject : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
}