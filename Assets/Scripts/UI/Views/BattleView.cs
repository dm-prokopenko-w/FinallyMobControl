using Core;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class BattleView : ViewItem
	{
		[SerializeField] private Button _play;

		private void Start()
		{
			_play.onClick.AddListener(Play);
		}

		private void Play()
		{
			SceneLoader.LoadGameScene();
		}
	}
}
