using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
	public class ChoiceView : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler
	{
		[SerializeField] private Image _icon;
		[SerializeField] private TMP_Text _counter;

		public Action OnStartDrag;
		public Action<Vector2> OnDragItem;
		public Action OnEndDragItem;

		public void Init(Sprite icon, Action onStartDrag, Action<Vector2> onDragItem, Action onEndDragItem)
		{
			_icon.sprite = icon;
			OnStartDrag += onStartDrag;
			OnDragItem += onDragItem;
			OnEndDragItem += onEndDragItem;
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			OnStartDrag?.Invoke();
		}

		public void OnDrag(PointerEventData eventData)
		{
			OnDragItem?.Invoke(eventData.position);
		}

		public void OnEndDrag(PointerEventData eventData)
		{
			OnEndDragItem?.Invoke();
		}

		public void SetCounter(int count) => _counter.text = count.ToString();
	}
}