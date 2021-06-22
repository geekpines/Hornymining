using System;
using UnityEngine;

namespace App.Scripts.Utilities.MonoBehaviours
{
    /// <summary>
    /// Класс для "статического" экземпляра ScriptableObject, который должен храниться в папке Resources/Settings.
    /// Класс наследуемый от StaticScriptableObject будет доступен в коде с помощью метода Instance.
    ///
    /// Предполагаемый способ исопльзования:
    /// Общие настройки для проекта, конфигурация сторонних сервисов,
    /// ссылки или общая конфигурация игры и т.п.
    /// </summary>
    /// <typeparam name="T">Имя наследуемого класса (имя ассета должно быть на английском, без пробелов и специальных символов)</typeparam>
    public class StaticScriptableObject<T> : ScriptableObject 
        where T : ScriptableObject
    {
        private static T _instance;

        /// <summary>
        /// Получить ссылку на экземпляр ScriptableObject
        /// </summary>
        [Obsolete ("Для получения доступа к данным используйте классы обертки")]
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    var path = $"Settings/{typeof(T).Name}";
                    _instance = Resources.Load(path) as T;
                }

                return _instance;
            }
        }
    }
}