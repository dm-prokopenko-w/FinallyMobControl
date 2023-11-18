using System;
using UnityEngine.EventSystems;

namespace Core
{
    public class ControlModule 
    {
        public event Action<PointerEventData> TouchStart;
        public event Action<PointerEventData> TouchEnd;
        public event Action<PointerEventData> TouchMoved;

        public void OnPointerDown(PointerEventData eventData)
        {
            TouchStart?.Invoke(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            TouchEnd?.Invoke(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            TouchMoved?.Invoke(eventData);
        }
    }
}
