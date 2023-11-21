using Core;
using GameplaySystem;
using System;
using System.Threading.Tasks;
using VContainer;
using VContainer.Unity;

namespace UI
{
	public class LvlProgressController : IStartable, IDisposable
	{
		[Inject] private MenuController _menuContr;
		[Inject] private ProgressController _progress;

		private LvlProgressView _lvlProgress;

		public void Start()
		{
			_menuContr.OnInitMenuItem += Init;
			_menuContr.OnInit += InitData;
		}

		public void Dispose()
		{
			_menuContr.OnInitMenuItem -= Init;
		}

		private void Init(ItemMenu item)
		{
			if (item.Type != Views.LvlProgress) return;

			_lvlProgress = item.View as LvlProgressView;
		}

		private void InitData(GameData data)
		{
			_lvlProgress.Init(data.Lvls, SaveProgress);
		}

		private async void SaveProgress(int numLvl)
		{
			_progress.SaveLvl(numLvl);
			await Task.Delay(50);
			SceneLoader.LoadGameScene();
		}
	}
}