using Core;
using Merge;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
	public class ChoiceView : MonoBehaviour,  IDropHandler
	{
		[SerializeField] private DragDropItem _item;
		[SerializeField] private GameObject _bg;

		public Action<DragDropItem> OnDropItem;

		public void Init(Action<DragDropItem> onStartDrag, Action<DragDropItem> onDropItem)
		{
			_item.OnStartDragItem += onStartDrag;
			OnDropItem += onDropItem;
		}

		public void OnDrop(PointerEventData eventData)
		{
			OnDropItem?.Invoke(_item);
		}

		public void SetParentBg(Transform tr) => _bg.transform.SetParent(tr);

		public void SetValue(int lvl, Sprite icon, string id, TypeUnit type) => _item.SetValue(lvl, icon, id, type);

		public DragDropItem GetDragDropItem() => _item;
	}
}