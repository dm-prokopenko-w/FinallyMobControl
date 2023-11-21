using Merge;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
	public class ChoiceView : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler, IDropHandler
	{
		[SerializeField] private Image _icon;
		[SerializeField] private TMP_Text _counter;
		[SerializeField] private DragDropItem _item;

		public Action OnStartDrag;
		public Action<Vector2> OnDragItem;
		public Action OnEndDragItem;
		public Action<ChoiceView> OnDropItem;

		public void Init(Action onStartDrag, Action<Vector2> onDragItem, Action onEndDragItem, Action<ChoiceView> onDropItem)
		{
			OnStartDrag += onStartDrag;
			OnDragItem += onDragItem;
			OnEndDragItem += onEndDragItem;
			OnDropItem += onDropItem;
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

		public void OnDrop(PointerEventData eventData)
		{
			OnDropItem?.Invoke(this);
		}

		public void SetValue(int count, Sprite icon)
		{
			_counter.text = count.ToString();
			_icon.sprite = icon;
		}
	}
}