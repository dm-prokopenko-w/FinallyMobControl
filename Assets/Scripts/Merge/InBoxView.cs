using Core;
using System;
using UnityEngine;

namespace Merge
{
	public class InBoxView : MonoBehaviour
	{
		[SerializeField] private DragDropItem _item;

		public void Init(Action<DragDropItem> onStartDrag, bool active)
		{
			_item.Init(onStartDrag, active);
			_item.OnDisable += DisableDragDrop;
		}

		public void SetValue(int lvl, Sprite icon, string id, TypeUnit type) => _item.SetValue(lvl, icon, id, type);

		public DragDropItem GetDragDropItem() => _item;

		private void DisableDragDrop()
		{
			gameObject.SetActive(false);
			_item.OnDisable -= DisableDragDrop;
		}

		private void OnDestroy()
		{
			_item.OnDisable -= DisableDragDrop;
		}
	}
}
