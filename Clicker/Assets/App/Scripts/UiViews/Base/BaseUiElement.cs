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
            if (this.TryGetComponent(out T result))
            {
                OnPressDown?.Invoke(result);
            }
        }

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            if (this.TryGetComponent(out T result))
            {
                OnStartHolder?.Invoke(result);
            }
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            if (this.TryGetComponent(out T result))
            {
                OnEndHolder?.Invoke(result);
            }
        }

        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            if (this.TryGetComponent(out T result))
            {
                OnPressUp?.Invoke(result);
            }
        }
    }
}
