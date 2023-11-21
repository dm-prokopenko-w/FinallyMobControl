using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Merge
{
	public class Slot : MonoBehaviour, IDropHandler
	{
		[SerializeField] private DragDropItem _item;

		public Action<Slot> OnDropItem;

		public void OnDrop(PointerEventData eventData)
		{
			_item.gameObject.SetActive(true);
			OnDropItem?.Invoke(this);
		}

		public void Set(Action<Slot> onDropItem)
		{
			OnDropItem = onDropItem;
		}

		public void SetOnDrop(Sprite icon)
		{
			_item.SetIcon(icon);
		}
	}
}
