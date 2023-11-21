using Core;
using System;
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

		public GameplayController(IObjectResolver container)
		{
			_container = container;
		}

		public async void Start()
		{
			OnEndGame += EndGame;

			var gameData = await _assetLoader.LoadConfig(Constants.GameData) as GameData;
			await Task.Delay(100);

			OnInit?.Invoke(gameData);
			OnInitWithContainer?.Invoke(gameData, _container);
		}

		private void EndGame(bool value) => _isEndGameValue = value;

		public async void Exit()
		{
			await Task.Delay(1500);

			if (_isEndGameValue)
			{
				int curLvl = _progress.Save.LoadLvl;
				_progress.WinLvl(curLvl);
			}

			SceneLoader.LoadMenuScene();
		}

		public void Dispose()
		{
			OnEndGame -= EndGame;
		}
	}
}
