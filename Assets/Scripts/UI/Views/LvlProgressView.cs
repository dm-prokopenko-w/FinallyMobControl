using GameplaySystem;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
	public class LvlProgressView : ViewItem
	{
		[SerializeField] private LvlProgressBtn _btnPrefab;
		[SerializeField] private Transform _btnsParent;
		[SerializeField] private GameObject _dontPlay;

		public Action OnEnableView;

		public void Init(List<LvlData> lvls, Action<int> onLoadLvl, int curLvl)
		{
			int num = 1;
			foreach (var lvl in lvls)
			{
				var btn = Instantiate(_btnPrefab, _btnsParent);
				btn.Init(num, lvl.Rewards, onLoadLvl, num <= curLvl);
				num++;
			}
		}

		public void UpdateView(bool value) => _dontPlay.SetActive(!value);

		public override void ActiveBody(bool value)
		{
			base.ActiveBody(value);
			OnEnableView?.Invoke();
		}
	}
}
