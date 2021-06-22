using System.Collections.Generic;
using UnityEngine;

namespace App.Scripts.Utilities.MonoBehaviours
{
    /// <summary>
    /// Пул игровых объектов, может быть использован для любых GameObject
    /// </summary>
    public class PoolObject<T> where T : MonoBehaviour
    {
    private int _size; //размер пула

    /// <summary>
    /// Количество объектов в пуле на данный момент
    /// </summary>
    public int Count => _objectsList.Count;

    /// <summary>
    /// Должен ли пул автоматически расширяться если нет достаточного количества объектов
    /// </summary>
    public bool Expandable { get; private set; }

    /// <summary>
    /// Объект-родитель в иерархии сцены
    /// </summary>
    public Transform ParentObject { get; private set; }

    /// <summary>
    /// Префаб, которым будет заполнен пул объектов, при инициализации
    /// </summary>
    private T _poolObject;

    /// <summary>
    /// Пул объектов
    /// </summary>
    private List<T> _objectsList;

    /// <summary>
    /// Инициализация пула объектов
    /// </summary>
    /// <param name="poolObject"></param>
    /// <param name="parentObject"></param>
    /// <param name="expandable"></param>
    public PoolObject(T poolObject, int size, Transform parentObject, bool expandable = false)
    {
        _objectsList = new List<T>();

        _poolObject = poolObject;
        Expandable = expandable;
        ParentObject = parentObject;
        _size = size;

        for (var i = 0; i < _size; i++)
        {
            Add(poolObject);
        }
    }

    /// <summary>
    /// Индексатор, возвращает выбраный объект пула
    /// </summary>
    /// <param name="index">Индекс требуемого объекта</param>
    /// <returns></returns>
    public T this[int index]
    {
        get { return _objectsList[index]; }
        set { _objectsList[index] = value; }
    }

    /// <summary>
    /// Метод добавляет новый объект в пул объектов
    /// </summary>
    /// <param name="prefab">Префаб создаваемого объекта</param>
    /// <param name="objectName">Имя для создаваемого объекта</param>
    /// <returns></returns>
    private void Add(T prefab, string objectName = "")
    {
        var newObject = Object.Instantiate(prefab, ParentObject);
        if (!string.IsNullOrEmpty(objectName))
            newObject.name = $"{objectName}_{Count.ToString()}";

        newObject.gameObject.SetActive(false);
        _objectsList.Add(newObject);
    }

    /// <summary>
    /// Метод получения из пула объекта, находит неактивный объект, активирует его и возвращает. Если нет подходящего объекта, и пул нерасширяемый - возвращает null
    /// </summary>
    /// <param name="autoActivate">Необходимо ли автоматически активировать объект</param>
    /// <returns></returns>
    public T GetObject(bool autoActivate = true)
    {
        if (_objectsList.Count != 0)
        {
            for (var index = 0; index < _objectsList.Count; index++)
            {
                var candidate = _objectsList[index];
                if (candidate.gameObject.activeSelf) continue;

                //Если необходимо автоматически активировать объекты при запросе - активируем.
                if (autoActivate)
                    candidate.gameObject.SetActive(true);
                return candidate;
            }
        }

        //Если не найден неактивный объект
        if (Expandable)
        {
            Add(_poolObject);
        }

        return null;
    }

    /// <summary>
    /// Вернуть объект в пул
    /// </summary>
    /// <param name="poolObj"></param>
    public void ReturnObject(T poolObj)
    {
        if (_objectsList.Contains(poolObj) && poolObj.gameObject.activeSelf)
        {
            poolObj.gameObject.SetActive(false);
        }
    }
    }
}