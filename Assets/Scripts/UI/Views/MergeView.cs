using Merge;
using System;
using UnityEngine;
using Core;

namespace UI
{
	public class MergeView : ViewItem
	{
		[SerializeField] private ChoiceView _choiceView;
		[SerializeField] private Transform _parentChoiceView;

		[SerializeField] private Slot _slotPrefab;
		[SerializeField] private Transform _slotParent;

		public void InitChoiceView(Action onStart, Action<Vector2> onDrag, Action onEndDrag, Action<ChoiceView> onDrop, Sprite icon)
		{
			var view = Instantiate(_choiceView, _parentChoiceView);
			view.name = _choiceView.name;
			view.Init(onStart, onDrag, onEndDrag, onDrop);
		}

		public void InitSlots(Action<Slot> onDropItem)
		{
			for (int i = 0; i < Constants.SlotCount; i++)
			{
				var slot = Instantiate(_slotPrefab, _slotParent);
				slot.Set(onDropItem);
			}
		}
	}
}