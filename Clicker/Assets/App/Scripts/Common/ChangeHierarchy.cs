using UnityEngine;

namespace App.Scripts.Common
{
    public class ChangeHierarchy : MonoBehaviour
    {
        [SerializeField] private Transform _targetParant;
        [SerializeField] private Transform _followObject;

        private Vector3 _offsetPosition;

        private void Start()
        {
            StartCoroutine(StandartCoroutines.InvokeEndFrame(Initialization));
        }

        private void Update()
        {
            transform.position = _followObject.position - _offsetPosition;
        }

        private void Initialization()
        {
            _offsetPosition = new Vector3(0, transform.position.y / 2, 0);
            if (_targetParant != null)
            {
                transform.SetParent(_targetParant);
            }
            // else
            // {
            //     var targetParant = GameObject.FindGameObjectWithTag("FirstLayer");
            //     transform.SetParent(targetParant.transform);
            // }
        }
    }
}