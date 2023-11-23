using Merge;
using System;
using UnityEngine;
using Core;
using UnityEditor;
using System.Collections.Generic;

namespace UI
{
	public class MergeView : ViewItem
	{
		[SerializeField] private ChoiceView _choiceView;
		[SerializeField] private Transform _parentChoiceView;
		[SerializeField] private Transform _parentChoiceViewBG;

		[SerializeField] private InBoxView _inBoxPrefab;
		[SerializeField] private Transform _parentBox;

		[SerializeField] private Slot _slotPrefab;
		[SerializeField] private Transform _slotParent;

		public ChoiceView InitChoiceView(Sprite icon, int lvl, string id, TypeUnit type)
		{
			var view = Instantiate(_choiceView, _parentChoiceView);
			view.name = _choiceView.name;
			view.SetParentBg(_parentChoiceViewBG);
			view.SetValue(lvl, icon, id, type);
			return view;
		}

		public Slot InitSlot(Action<DragDropItem> onStart, Action<DragDropItem> onDropItem, int num, bool active)
		{
			var slot = Instantiate(_slotPrefab, _slotParent);
			slot.Set(onStart, onDropItem, num, active);
			return slot;
		}

		public InBoxView InitInBox(Action<DragDropItem> onStart)
		{
			var obj = Instantiate(_inBoxPrefab, _parentBox);
			obj.Init(onStart, true);
			return obj;
		}
	}
}