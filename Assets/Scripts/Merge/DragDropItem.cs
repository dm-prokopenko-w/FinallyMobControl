using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Merge
{
	public class DragDropItem : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler 
	{
		[SerializeField] private RectTransform _rect;

		public Action OnStartDragItem;
		public Action<Vector2> OnDragItem;
		public Action OnEndDragItem;

		[SerializeField] private Image _icon;
		[SerializeField] private CanvasGroup _canvasGroup;

		private Vector3 _startPos;
		private Sprite _curIcon;

		private void Start()
		{
			_startPos = _rect.position;
		}

		public void Init(Action onStartDrag, Action<Vector2> onDragItem, Action onEndDragItem)
		{
			OnStartDragItem += onStartDrag;
			OnDragItem += onDragItem;
			OnEndDragItem += onEndDragItem;
			gameObject.SetActive(false);
		}


		public void OnPointerDown(PointerEventData eventData)
		{
			OnStartDragItem?.Invoke();
			SetBlocksRaycasts(false);
		}

		public void OnDrag(PointerEventData eventData)
		{
			OnDragItem?.Invoke(eventData.position);
			SetPos(eventData.delta);
		}

		public void OnEndDrag(PointerEventData eventData)
		{
			SetBlocksRaycasts(true);
			_rect.position = _startPos;
			OnEndDragItem?.Invoke();
		}

		public void SetPos(Vector2 pos) => _rect.anchoredPosition += pos;

		public void SetIcon(Sprite icon)
		{
			_curIcon = icon;
			_icon.sprite = icon;
		}

		public void SetBlocksRaycasts(bool value) => _canvasGroup.blocksRaycasts = value;

		public Sprite GetSprite() => _curIcon;
	}
}
