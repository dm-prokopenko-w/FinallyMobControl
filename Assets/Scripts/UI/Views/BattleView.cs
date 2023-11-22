using Core;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class BattleView : ViewItem
	{
		[SerializeField] private Button _play;

		[SerializeField] private ResetPanel _resetPanel;

		public Action OnEnableBattleView;

		private void Start()
		{
			_play.onClick.AddListener(Play);
		}

		private void Play()	 => SceneLoader.LoadGameScene();

		public void Init(Action onReset)  => _resetPanel.Init(onReset);

		public void UpdateView(bool activePlayBtn) => _play.interactable = activePlayBtn;

		public override void ActiveBody(bool value)
		{
			base.ActiveBody(value);
			OnEnableBattleView?.Invoke();
		}
	}
}
