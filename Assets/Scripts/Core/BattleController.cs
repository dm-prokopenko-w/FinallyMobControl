using Core;
using GameplaySystem;
using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace UI
{
	public class BattleController : IStartable, IDisposable
	{
		[Inject] private MenuController _menuContr;
		[Inject] private ProgressController _progress;

		private BattleView _battle;

		public void Start()
		{
			_menuContr.OnInitMenuItem += Init;
			_menuContr.OnInit += InitData;
		}

		private void UpdateBattle()
		{
			if (_progress.Save.UseMob == null)
			{
				_battle.UpdateView(false);
				return;
			}
			if (_progress.Save.UseChamp == null)
			{
				_battle.UpdateView(false);
				return;
			}
			if (string.IsNullOrEmpty(_progress.Save.UseMob.Id))
			{
				_battle.UpdateView(false);
				return;
			}
			if (string.IsNullOrEmpty(_progress.Save.UseChamp.Id))
			{
				_battle.UpdateView(false);
				return;
			}

			_battle.UpdateView(true);
		}

		public void Dispose()
		{
			_menuContr.OnInit -= InitData;
			_menuContr.OnInitMenuItem -= Init;
			_battle.OnEnableBattleView -= UpdateBattle;
		}

		private void Init(ItemMenu item)
		{
			if (item.Type != Views.Battle) return;
			_battle = item.View as BattleView;
			_battle.OnEnableBattleView += UpdateBattle;
			UpdateBattle();
		}

		private void InitData(GameData data)
		{
			_battle.Init(ResetGame);
		}

		private void ResetGame()
		{
			_progress.ResetGame();
		}
	}
}
