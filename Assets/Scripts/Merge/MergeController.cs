using Core;
using GameplaySystem;
using System;
using UI;
using UnityEditor;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using static UnityEditor.Progress;

namespace Merge
{
	public class MergeController : IStartable, IDisposable
	{
		[Inject] private MenuController _menuContr;
		[Inject] private ProgressController _progrss;

		private DragDropItem _dropItem;
		private MergeView _merge;

		public void Start()
		{
			_menuContr.OnInitMenuItem += Init;
			_menuContr.OnInit += InitData;
		}

		private void InitData(GameData data)
		{
			var progress = _progrss.Save;
			var mobSprite = data.PlayerParm.Mobs[progress.NumMod].Icon;
			var champSprite = data.PlayerParm.Mobs[progress.NumMod].Icon;

			_merge.InitChoiceView(
				() => StartDrag(mobSprite),
				SetDragItem,
				EndDrag,
				(ChoiceView view) => SetValueChoiceView(view, champSprite),
				mobSprite);

			_merge.InitChoiceView(
				() => StartDrag(champSprite),
				SetDragItem,
				EndDrag,
				(ChoiceView view) => SetValueChoiceView(view, champSprite),
				champSprite);

			_merge.InitSlots(OnDropInSlot);
		}

		private void Init(ItemMenu item)
		{
			if (item.Type != Views.Merge) return;
			_merge = item.View as MergeView;

			/*
		   _dropItem = Object.Instantiate(data.DropPrefab, item.View.transform);
		   _dropItem.Init(
			   () => StartDrag(data.PlayerParm.Mobs[progress.NumMod].Icon),
			   SetDragItem,
			   EndDrag);
		   */
		}

		public void OnDropInSlot(Slot slot)
		{
			slot.SetOnDrop(_dropItem.GetSprite());
		}

		public void Dispose()
		{
			_menuContr.OnInitMenuItem -= Init;
		}

		private void StartDrag(Sprite icon)
		{
			_dropItem.SetBlocksRaycasts(false);
			_dropItem.SetIcon(icon);
		}

		private void SetValueChoiceView(ChoiceView view, Sprite icon)
		{
			//view.SetValue(icon);
		}

		private void SetDragItem(Vector2 pos)
		{
			_dropItem.transform.position = pos;
			_dropItem.gameObject.SetActive(true);
		}

		public void EndDrag()
		{
			_dropItem.SetBlocksRaycasts(true);
			_dropItem.gameObject.SetActive(false);
		}
	}
}
