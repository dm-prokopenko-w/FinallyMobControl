using Core;
using GameplaySystem;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace UI
{
	public class LvlProgressBtn : MonoBehaviour
	{
		[Inject] private ProgressController _progress;

		[SerializeField] private Button _btn;
		[SerializeField] private TMP_Text _numLvl;

		[SerializeField] private ChoiceView _view;
		[SerializeField] private Transform _parent;

		private int _currentLvl;

		private void Start()
		{
			_btn.onClick.AddListener(Click);
		}

		public void Init(int num, List<RewardItem> rewards)
		{
			_numLvl.text = num.ToString();
			_currentLvl = num;

			foreach (var reward in rewards)
			{
				var view = Instantiate(_view, _parent);
				view.SetCounter(reward.Count);
			}
		}

		private void Click()
		{
			_progress.Save.LoadLvl = _currentLvl;
			SceneLoader.LoadGameScene();
		}
	}
}