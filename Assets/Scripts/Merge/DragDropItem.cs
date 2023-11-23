using Core;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Merge
{
	public class DragDropItem : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler
	{
		[SerializeField] private RectTransform _rect;

		public Action<DragDropItem> OnStartDragItem;
		public Action OnDisable;

		[SerializeField] private Image _icon;
		[SerializeField] private CanvasGroup _canvasGroup;
		[SerializeField] private TMP_Text _counter;

		private Sprite _curIcon;
		private int _curLvl;
		private string _id;
		private TypeUnit _type;

		public void Init(Action<DragDropItem> onStartDrag, bool active = false)
		{
			OnStartDragItem += onStartDrag;
			gameObject.SetActive(active);
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			OnStartDragItem?.Invoke(this);
			SetBlocksRaycasts(false);
		}

		public void OnDrag(PointerEventData eventData)
		{
			SetPos(eventData.delta);
		}

		public void OnEndDrag(PointerEventData eventData)
		{
			SetBlocksRaycasts(true);
			_rect.anchoredPosition = Vector2.zero;
		}

		public void SetPos(Vector2 pos) => _rect.anchoredPosition += pos;

		public void SetValue(int lvl, Sprite icon, string id, TypeUnit type)
		{
			_curIcon = icon;
			_curLvl = lvl;
			_id = id;
			_type = type;

			_counter.text = (lvl + 1).ToString();
			_icon.sprite = icon;

			_rect.anchoredPosition = Vector2.zero;
		}

		public void SetBlocksRaycasts(bool value) => _canvasGroup.blocksRaycasts = value;

		public void DisableItem()
		{
			OnDisable?.Invoke();
			gameObject.SetActive(false);
		}

		public Sprite CurSprite => _curIcon;
		public int CurLvl => _curLvl;
		public string CurId => _id;
		public TypeUnit CurTypeUnit => _type;
	}
}
