using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Core
{
	public class ProgressController : IStartable
	{
		[Inject] private SaveModule _saveModule;

		public Progress Save
		{
			get
			{
				if (_save == null)
				{
					_save = _saveModule.Load<Progress>(Constants.ProgressKey);
					if (_save == null)
					{
						Init();
					}
				}

				return _save;
			}
			set
			{
				_save = value;

				if (_save != null) return;
				Init();
			}
		}

		private Progress _save;

		public void Start()
		{
			Save = _saveModule.Load<Progress>(Constants.ProgressKey);
		}

		private void Init()
		{
			_save = new Progress()
			{
				LoadLvl = 1,
				//ProgressLvl = 1,
				NumMod = 0,
				NumModPower = 0,
				NumChamp = 0,
				NumChampPower = 0
			};
		}
		
		public void SaveLvl(int numLvl)
		{
			_save.LoadLvl = numLvl;
			_saveModule.Save(Constants.ProgressKey, _save);
		}

		public void WinLvl(int num)
		{
			_save.LoadLvl = num;
			_saveModule.Save(Constants.ProgressKey, _save);
		}
	}

	public class Progress
	{
		public int LoadLvl;
		public int NumMod;
		public int NumModPower;
		public int NumChamp;
		public int NumChampPower;
	}
}
