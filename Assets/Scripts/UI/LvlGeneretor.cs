using Core;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GameplaySystem
{
	public class LvlGeneretor : MonoBehaviour, IStartable
	{
		[Inject] private GameplayController _gameplay;
		[Inject] private ProgressController _progrss;

		public void Start()
		{
			_gameplay.OnInitWithContainer += InitData;
		}

		private void InitData(GameData data, IObjectResolver container)
		{
			var progress = _progrss.Save;
			var obj = container.Instantiate(data.Lvls[progress.LoadLvl - 1].LvlEnv);
			obj.transform.SetParent(transform);
		}

		private void OnDestroy()
		{
			_gameplay.OnInitWithContainer -= InitData;
		}
	}
}
