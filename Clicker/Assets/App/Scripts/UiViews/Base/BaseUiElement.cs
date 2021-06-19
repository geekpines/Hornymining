using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace App.Scripts.UiViews
{
    public abstract class BaseUiElement<T> : MonoBehaviour, 
        IPointerClickHandler, IPointerEnterHandler, 
        IPointerExitHandler, IPointerUpHandler
    where T : MonoBehaviour
    {
        public event Action<T> OnStartHolder;
        public event Action<T> OnEndHolder;
        public event Action<T> OnPressDown;
        public event Action<T> OnPressUp;
        
        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            var result = this.GetComponent<T>();
            if (result != null)
            {
                OnPressDown?.Invoke(result);
            }
        }
    
        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            var result = this.GetComponent<T>();
            if (result != null)
            {
                OnStartHolder?.Invoke(result);
            }
        }
    
        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            var result = this.GetComponent<T>();
            if (result != null)
            {
                OnEndHolder?.Invoke(result);
            }
        }

        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            var result = this.GetComponent<T>();
            if (result != null)
            {
                OnPressUp?.Invoke(result);
            }
        }
    }
}
