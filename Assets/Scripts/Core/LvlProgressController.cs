using Core;
using GameplaySystem;
using System;
using VContainer;
using VContainer.Unity;

namespace UI
{
	public class LvlProgressController : IStartable, IDisposable
	{
		[Inject] private MenuController _menuContr;

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

		private void Init(MenuItem item)
		{
			if (item.Type != Views.LvlProgress) return;

			_lvlProgress = item.View as LvlProgressView;
		}

		private void InitData(GameData data)
		{
			_lvlProgress.Init(data.Lvls);
		}
	}
}