using VContainer;
using VContainer.Unity;

namespace Core
{
	public class ProgressController : IStartable
	{
		[Inject] private SaveModule _saveModule;

		private Progress _save;

		public void Start()
		{

			_save = _saveModule.Load<Progress>(Constants.ProgressKey);
			CheckInit();
		}

		private void CheckInit()
		{
			if (_save == null)
			{
				_save = new Progress()
				{
					Lvl = 1,
					NumMod =0,
					NumModPower = 0,
					NumChamp = 0,
					NumChampPower = 0
				};
			}
		}
		public Progress GetSave()
		{
			CheckInit();
			return _save;
		}
	}

	public class Progress
	{
		public int Lvl;
		public int NumMod;
		public int NumModPower;
		public int NumChamp;
		public int NumChampPower;
	}
}
