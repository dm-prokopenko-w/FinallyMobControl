using UnityEngine;
using UnityEngine.EventSystems;
using VContainer;

namespace Core
{
    public class TouchZone : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        [Inject] private ControlModule _control;

        public void OnPointerDown(PointerEventData eventData)
        {
            _control.OnPointerDown(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _control.OnPointerUp(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            _control.OnDrag(eventData);
        }
    }
}