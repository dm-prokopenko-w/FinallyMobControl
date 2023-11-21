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
					if (_save != null)
					{
						return _save;
					}
				}

				_save = new Progress()
				{
					LoadLvl = 1,
					ProgressLvl = 1,
					NumMod = 0,
					NumModPower = 0,
					NumChamp = 0,
					NumChampPower = 0
				};

				return _save;
			}

			set { _save = value; }
		}

		private Progress _save;

		public void Start()
		{
			Save = _saveModule.Load<Progress>(Constants.ProgressKey);
		}

		public void WinLvl(int num)
		{
			Save.LoadLvl = num;

			if (num > Save.ProgressLvl)
			{
				Save.ProgressLvl = num;
			}
		}
	}

	public class Progress
	{
		public int LoadLvl;
		public int ProgressLvl;
		public int NumMod;
		public int NumModPower;
		public int NumChamp;
		public int NumChampPower;
	}
}
