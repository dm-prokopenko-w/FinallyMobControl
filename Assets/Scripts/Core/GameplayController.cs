using Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VContainer;
using VContainer.Unity;

namespace GameplaySystem
{
	public class GameplayController : IStartable, IDisposable
	{
		[Inject] private AssetLoader _assetLoader;
		[Inject] private ProgressController _progress;

		public Action<GameData> OnInit;
		public Action<GameData, IObjectResolver> OnInitWithContainer;
		public Action<bool> OnEndGame;

		private bool _isEndGameValue;
		private IObjectResolver _container;
		private List<RewardItem> _rewards = new List<RewardItem>();
		private int _maxLvl;

		public GameplayController(IObjectResolver container)
		{
			_container = container;
		}

		public async void Start()
		{
			OnEndGame += EndGame;

			var gameData = await _assetLoader.LoadConfig(Constants.GameData) as GameData;
			_maxLvl = gameData.Lvls.Count;
			await Task.Delay(100);
			_rewards = gameData.Lvls[_progress.Save.LoadLvl - 1].Rewards;

			OnInit?.Invoke(gameData);
			OnInitWithContainer?.Invoke(gameData, _container);
		}

		private void EndGame(bool value) => _isEndGameValue = value;

		public async void Exit()
		{
			if (_isEndGameValue)
			{
				int curLvl = _progress.Save.LoadLvl;

				if (curLvl < _maxLvl)
				{
					curLvl++;
				}

				_progress.WinLvl(curLvl, _rewards);
			}

			await Task.Delay(1500);
			SceneLoader.LoadMenuScene();
		}

		public void Dispose()
		{
			OnEndGame -= EndGame;
		}
	}
}
