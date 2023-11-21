using System;
using UnityEngine.EventSystems;

namespace Core
{
    public class ControlModule 
    {
        public event Action<PointerEventData> TouchStart;
        public event Action<PointerEventData> TouchEnd;
        public event Action<PointerEventData> TouchMoved;
        public event Action<PointerEventData> TouchBeginDrag;
        public event Action<PointerEventData> TouchEndDrag;
        public event Action<PointerEventData> TouchDrop;

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

        public void OnBeginDrag(PointerEventData eventData)
        {
			TouchBeginDrag?.Invoke(eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
			TouchEndDrag?.Invoke(eventData);
        }

        public void OnDrop(PointerEventData eventData)
        {
            TouchMoved?.Invoke(eventData);
        }
    }
}
