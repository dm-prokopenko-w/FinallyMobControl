using Core;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class BattleView : ViewItem
	{
		[SerializeField] private Button _play;
		[SerializeField] private Button _exit;

		[SerializeField] private ResetPanel _resetPanel;
		[SerializeField] private GameObject _dontPlay;

		public Action OnEnableView;

		private void Start()
		{
			_play.onClick.AddListener(Play);
			_exit.onClick.AddListener(Exit);
		}

		private void Play()
		{
			SceneLoader.LoadGameScene();
		}
		private void Exit()
		{
			Application.Quit();
		}

		public void Init(Action onReset)
		{
			_resetPanel.Init(onReset);
		}


		public void UpdateView(bool value)
		{
			_dontPlay.SetActive(!value);
			_play.interactable = value;
		}

		public override void ActiveBody(bool value)
		{
			base.ActiveBody(value);
			OnEnableView?.Invoke();
		}
	}
}
