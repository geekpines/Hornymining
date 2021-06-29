using UnityEngine;

namespace App.Scripts.Utilities.MonoBehaviours
{
    /// <summary>
    /// Абстрактная реализация синглтона
    /// </summary>
    /// <typeparam name="T">Класс, который необходимо сделать единственным</typeparam>
    public class Singleton<T> : MonoBehaviour 
        where T : MonoBehaviour
    {
        public static T Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this as T;
                DontDestroyOnLoad(this.gameObject);
                OnAwake();
            }
            else
            {
                Destroy(this);
            }
        }

        protected virtual void OnAwake()
        {
        }
    }
}