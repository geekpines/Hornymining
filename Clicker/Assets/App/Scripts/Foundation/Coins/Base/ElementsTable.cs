using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace App.Scripts.Foundation.Coins
{
    /// <summary>
    /// Абстрактная таблица элементов. Может хранить список элементов и 
    /// их отношение друг к другу
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ElementsTable<T> : ScriptableObject
        where T : ScriptableObject
    {
        /// <summary>
        /// Класс для временного хранения данных в таблице
        /// </summary>
        [System.Serializable]
        public class ElementData
        {
            public List<float> Data = new List<float>();
        }
        
        /// <summary>
        /// Словарь в котором хранятся все данные о отношении
        /// элементов друг к другу. 
        /// </summary>
        public Dictionary<T, Dictionary<T, float>> BonusElement => _bonusElement;
        
        /// <summary>
        /// Список всех элементов.
        /// При необходимости добавить новый элемент в игру -
        /// добавить сюда через инспектор.
        /// </summary>
        public List<T> Elements => _elements;
        
        [SerializeField] private List<T> _elements = new List<T>();
        [SerializeField] private Dictionary<T, Dictionary<T, float>> _bonusElement = 
            new Dictionary<T, Dictionary<T, float>>();

        /// <summary>
        /// Кэш - нужен для сохранения/восстановления данных в таблице
        /// </summary>
        [SerializeField, HideInInspector] private List<ElementData> _chachedBonusData = new List<ElementData>();
        
        private void OnValidate()
        {
            foreach (var element in Elements)
            {
                if (!_bonusElement.ContainsKey(element) && element != null)
                {
                    _bonusElement.Add(element, new Dictionary<T, float>(){ {element, 1f} });
                }
            }
        }

        /// <summary>
        /// Сохранить данные из словаря в кэш
        /// </summary>
        public void SaveData()
        {
            _chachedBonusData.Clear();
            InitChachedData();
            
            for (int i = 0; i < _elements.Count; i++)
            {
                for (int j = 0; j < _elements.Count; j++)
                {
                    if (!_bonusElement[_elements[i]].ContainsKey(_elements[j]))
                    {
                        _bonusElement[_elements[i]].Add(_elements[j], 0);
                    }
                    _chachedBonusData[i].Data[j] = _bonusElement[_elements[i]][_elements[j]];
                }
            }
            
            #if UNITY_EDITOR
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
            #endif
        }
        
        /// <summary>
        /// Загрузить закэшированные данные в словарь
        /// </summary>
        public void LoadData()
        {
            for (int i = 0; i < _elements.Count; i++)
            {
                for (int j = 0; j < _elements.Count; j++)
                {
                    if (!_bonusElement[_elements[i]].ContainsKey(_elements[j]))
                    {
                        _bonusElement[_elements[i]].Add(_elements[j], _chachedBonusData[i].Data[j]);
                    }
                    else
                    {
                        _bonusElement[_elements[i]][_elements[j]] = _chachedBonusData[i].Data[j];
                    }
                }
            }
        }
        
        private void InitChachedData()
        {
            for (int i = 0; i < _elements.Count; i++)
            {
                _chachedBonusData.Add(new ElementData());
                for (int j = 0; j < _elements.Count; j++)
                {
                    _chachedBonusData[i].Data.Add(0);
                }
            }
        }
    }
}