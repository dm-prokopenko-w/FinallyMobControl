using Core;
using GameplaySystem;
using System;
using System.Collections.Generic;
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

		private List<Slot> _slots = new List<Slot>();
		private List<DragDropItem> _inBox = new List<DragDropItem>();
		private ChoiceView _mobView;
		private ChoiceView _champView;

		public void Start()
		{
			_menuContr.OnInitMenuItem += Init;
			_menuContr.OnInit += InitData;
		}

		private void InitData(GameData data)
		{
			var progress = _progrss.Save;

			_mobView = InitView(data.PlayerParm.Mobs, progress.UseMob.Id, progress.UseMob.Lvl, TypeUnit.Mob);
			_champView = InitView(data.PlayerParm.Champs, progress.UseChamp.Id, progress.UseChamp.Lvl, TypeUnit.Champ);

			for (int i = 0; i < Constants.SlotCount; i++)
			{
				var save = _progrss.Save.Slots.Find(x => x.Num == i);
				int num = i;
				var slot = _merge.InitSlot(
					StartDrag,
					(DragDropItem item) => CheckMergeValue(item, () => _progrss.SaveInSlot(item, num), TypeUnit.All),
					i,
					save != null);

				if (save == null)
				{
					slot.SetValue(0, null, null, TypeUnit.All);
				}
				else
				{
					slot.SetValue(
						save.Lvl,
						GetIcon(data, save.Lvl, save.Id),
						save.Id,
						GetUnitType(data, save.Id));
				}
				_slots.Add(slot);
			}

			foreach (var save in _progrss.Save.InBox)
			{
				var obj = UnityEngine.Object.Instantiate(data.DropPrefab, _merge.ParentBox);
				obj.Init(StartDrag, true);
				obj.SetValue(
					save.Lvl,
					GetIcon(data, save.Lvl, save.Id),
					save.Id,
					GetUnitType(data, save.Id));
				_inBox.Add(obj);
			}
		}

		private ChoiceView InitView(List<UnitType> units, string id, int lvl, TypeUnit type)
		{
			Sprite unitSprite;

			var unitType = units.Find(x => x.Id.Equals(id));
			if (unitType != null)
			{
				unitSprite = unitType.Items[lvl].Icon;
			}
			else
			{
				unitSprite = units[0].Items[0].Icon;
			}

			var view = _merge.InitChoiceView(unitSprite, lvl, id, type);
			view.Init(StartDrag, (DragDropItem item) => CheckMergeValue(item, () => _progrss.SaveUseUnit(item, type), type));
			view.GetDragDropItem().gameObject.SetActive(unitType != null);
			return view;
		}

		private void Init(ItemMenu item)
		{
			if (item.Type != Views.Merge) return;
			_merge = item.View as MergeView;
		}

		public void Dispose()
		{
			_menuContr.OnInitMenuItem -= Init;
		}

		private void StartDrag(DragDropItem item)
		{
			_dropItem = item;
		}

		private void CheckMergeValue(DragDropItem item, Action onSave, TypeUnit type)
		{
			if (!item.gameObject.activeSelf)
			{
				if (type == TypeUnit.All || _dropItem.CurTypeUnit == type)
				{
					SetVelue(item, _dropItem.CurLvl, onSave);
				}
				return;
			}
			if (item.CurId.Equals(_dropItem.CurId) && item.CurLvl == _dropItem.CurLvl)
			{
				int lvl = _dropItem.CurLvl + 1;
				SetVelue(item, lvl, onSave);
				return;
			}
		}

		private void SetVelue(DragDropItem item, int lvl, Action onSave)
		{
			DisableDragDropItem();
			item.SetValue(lvl, _dropItem.CurSprite, _dropItem.CurId, _dropItem.CurTypeUnit);
			item.SetBlocksRaycasts(true);
			item.gameObject.SetActive(true);
			onSave?.Invoke();
		}

		private void DisableDragDropItem()
		{
			_dropItem.gameObject.SetActive(false);

			if (_mobView.GetDragDropItem() == _dropItem)
			{
				_progrss.SaveUseUnit(null, TypeUnit.Mob);
			}
			else if (_champView.GetDragDropItem() == _dropItem)
			{
				_progrss.SaveUseUnit(null, TypeUnit.Champ);
			}

			var unitSlot = _slots.Find(x => x.GetDragDropItem() == _dropItem);
			if (unitSlot != null)
			{
				_progrss.SaveInSlot(null, unitSlot.Num);
			}

			var unit = _inBox.Find(x => x == _dropItem);
			if (unit != null)
			{
				_progrss.RemoveInBox(_dropItem);
				_inBox.Remove(unit);
			}
		}

		private Sprite GetIcon(GameData data, int lvl, string id)
		{
			var mobType = data.PlayerParm.Mobs.Find(x => x.Id.Equals(id));
			if (mobType != null)
			{
				return mobType.Items[lvl].Icon;
			}

			var champType = data.PlayerParm.Champs.Find(x => x.Id.Equals(id));
			if (champType != null)
			{
				return champType.Items[lvl].Icon;
			}

			return null;
		}

		private TypeUnit GetUnitType(GameData data, string id)
		{
			var mobType = data.PlayerParm.Mobs.Find(x => x.Id.Equals(id));
			if (mobType != null)
			{
				return TypeUnit.Mob;
			}

			var champType = data.PlayerParm.Champs.Find(x => x.Id.Equals(id));
			if (champType != null)
			{
				return TypeUnit.Champ;
			}

			return TypeUnit.All;
		}
	}
}
