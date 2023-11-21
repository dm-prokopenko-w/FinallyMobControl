using Core;
using GameplaySystem;
using System;
using UI;
using UnityEditor;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace Merge
{
	public class MergeController : IStartable, IDisposable
	{
		[Inject] private MenuController _menuContr;

		[Inject] private AssetLoader _assetLoader;
		[Inject] private ProgressController _progrss;

		private DragDropItem _dropItem;
		private Sprite _mobSprite;
		private Sprite _champSprite;

		public void Start()
		{
			_menuContr.OnInitMenuItem += Init;
			_menuContr.OnInit += InitData;
		}

		private void InitData(GameData data)
		{
			var progress = _progrss.GetSave();
			_mobSprite = data.PlayerParm.Mobs[progress.NumMod].Icon;
			_champSprite = data.PlayerParm.Mobs[progress.NumMod].Icon;
		}

		private async void Init(UI.MenuItem item)
		{
			if (item.Type != Views.Merge) return;

			GameData data = await _assetLoader.LoadConfig(Constants.GameData) as GameData;
			var progress = _progrss.GetSave();

			_dropItem = Object.Instantiate(data.DropPrefab, item.View.transform);
			_dropItem.Init(
				() => StartDrag(data.PlayerParm.Mobs[progress.NumMod].Icon),
				SetDragItem,
				EndDrag);


			MergeView merge = item.View as MergeView;

			merge.InitChoiceView(
				() => StartDrag(data.PlayerParm.Mobs[progress.NumMod].Icon),
				SetDragItem,
				EndDrag,
				data.PlayerParm.Mobs[progress.NumMod].Icon);

			merge.InitChoiceView(
				() => StartDrag(data.PlayerParm.Champs[progress.NumChamp].Icon),
				SetDragItem,
				EndDrag,
				data.PlayerParm.Champs[progress.NumChamp].Icon);

			merge.InitSlots(OnDropInSlot);

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
