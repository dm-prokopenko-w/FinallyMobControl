using Core;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Merge
{
	public class Slot : MonoBehaviour, IDropHandler
	{
		[SerializeField] private DragDropItem _item;

		public Action<DragDropItem> OnDropItem;
		public int Num => _num;
		private int _num;

		public void OnDrop(PointerEventData eventData)
		{
			OnDropItem?.Invoke(_item);
		}

		public void Set(Action<DragDropItem> onStartDrag, Action<DragDropItem> onDropItem, int num, bool active)
		{
			OnDropItem = onDropItem;
			_item.OnStartDragItem = onStartDrag;
			_num = num;
			_item.gameObject.SetActive(active);
		}

		public void SetValue(int lvl, Sprite icon, string id, TypeUnit type) => _item.SetValue(lvl, icon, id, type);

		public DragDropItem GetDragDropItem() => _item;
	}
}
