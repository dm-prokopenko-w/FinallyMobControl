using GameplaySystem;
using Merge;
using System;
using System.Collections.Generic;
using VContainer;
using VContainer.Unity;

namespace Core
{
	public class ProgressController : IStartable
	{
		[Inject] private SaveModule _saveModule;
		[Inject] private AssetLoader _assetLoader;

		public Progress Save => _save;

		private Progress _save;

		public void Start()
		{
			_save = _saveModule.Load<Progress>(Constants.ProgressKey);
			if (_save == null)
			{
				Init();
			}
		}

		private async void Init()
		{
			var data = await _assetLoader.LoadConfig(Constants.GameData) as GameData;

			_save = new Progress();
			_save.LoadLvl = 1;
			_save.UseMob = new ProgressItem(data.PlayerParm.Mobs[0].Id, 0);
			_save.UseChamp = new ProgressItem(data.PlayerParm.Champs[0].Id, 0);
			_save.Slots = new List<ProgressItem>();
			_save.InBox = new List<ProgressItem>();

			_saveModule.Save(Constants.ProgressKey, _save);
		}

		public void SaveLvl(int numLvl)
		{
			_save.LoadLvl = numLvl;
			_saveModule.Save(Constants.ProgressKey, _save);
		}

		public void SaveUseUnit(DragDropItem item, TypeUnit type)
		{
			if (item == null)
			{
				if (type == TypeUnit.Mob)
				{
					_save.UseMob = null;
				}
				else if (type == TypeUnit.Champ)
				{
					_save.UseChamp = null;
				}
			}
			else
			{
				if (type == TypeUnit.Mob)
				{
					_save.UseMob = new ProgressItem(item.CurId, item.CurLvl);
				}
				else if (type == TypeUnit.Champ)
				{
					_save.UseChamp = new ProgressItem(item.CurId, item.CurLvl);
				}
			}

			_saveModule.Save(Constants.ProgressKey, _save);
		}

		public void SaveInSlot(DragDropItem item, int numSlot)
		{
			var slot = _save.Slots.Find(x => x.Num == numSlot);
			if (item == null)
			{
				_save.Slots.Remove(slot);
			}
			else
			{
				if (slot != null)
				{
					slot.Id = item.CurId;
					slot.Lvl = item.CurLvl;
				}
				else
				{
					_save.Slots.Add(new ProgressItem(item.CurId, item.CurLvl, numSlot));
				}
			}

			_saveModule.Save(Constants.ProgressKey, _save);
		}

		public void RemoveInBox(DragDropItem item)
		{
			var save = _save.InBox.Find(x => x.Id.Equals(item.CurId));
			if (save != null)
			{
				_save.InBox.Remove(save);
			}

			_saveModule.Save(Constants.ProgressKey, _save);
		}

		public void WinLvl(int num, List<RewardItem> rewards)
		{
			Save.LoadLvl = num;
			foreach (var rew in rewards)
			{
				Save.InBox.Add(new ProgressItem(rew.Item.Id, rew.Count));
			}

			_saveModule.Save(Constants.ProgressKey, Save);
		}

		public void ResetGame()
		{
			_saveModule.Delete(Constants.ProgressKey);
			Init();
		}
	}

	[Serializable]
	public class Progress
	{
		public int LoadLvl;
		public ProgressItem UseMob;
		public ProgressItem UseChamp;
		public List<ProgressItem> Slots;
		public List<ProgressItem> InBox;
	}

	[Serializable]
	public class ProgressItem
	{
		public string Id;
		public int Lvl;
		public int Num;

		public ProgressItem(string id, int lvl)
		{
			Id = id;
			Lvl = lvl;
		}

		public ProgressItem(string id, int lvl, int num)
		{
			Id = id;
			Lvl = lvl;
			Num = num;
		}
	}
}
