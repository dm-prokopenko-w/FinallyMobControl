using Core;
using GameplaySystem;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class LvlProgressBtn : MonoBehaviour
	{
		[SerializeField] private Button _btn;
		[SerializeField] private TMP_Text _numLvl;

		[SerializeField] private RewardView _view;
		[SerializeField] private Transform _parent;

		private int _currentLvl;
		private Action<int> _onLoadLvl;

		private void Start()
		{
			_btn.onClick.AddListener(Click);
		}

		public void Init(int num, List<RewardItem> rewards, Action<int> onLoadLvl, bool activeBtn)
		{
			_numLvl.text = num.ToString();
			_currentLvl = num;
			_onLoadLvl = onLoadLvl;
			_btn.interactable = activeBtn;
			foreach (var reward in rewards)
			{
				var view = Instantiate(_view, _parent);
				view.Init(reward.Item.Icon, reward.Count);
			}
		}

		private void Click()
		{
			_onLoadLvl?.Invoke(_currentLvl);
		}
	}
}