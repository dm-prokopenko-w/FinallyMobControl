using GameplaySystem;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace UI
{
	public class LvlProgressView : ViewItem
	{
		[SerializeField] private LvlProgressBtn _btnPrefab;
		[SerializeField] private Transform _btnsParent;

		public void Init(List<LvlData> lvls)
		{
			int num = 1;
			foreach (var lvl in lvls)
			{
				var btn = Instantiate(_btnPrefab, _btnsParent);
				btn.Init(num, lvl.Rewards);
				num++;	
			}
		}
	}
}
