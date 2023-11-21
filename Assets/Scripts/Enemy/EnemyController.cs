using System;
using UnityEngine;
using VContainer.Unity;
using GameplaySystem.Units;
using VContainer;
using Core;
using System.Collections.Generic;
using UnityEngine.EventSystems;

namespace GameplaySystem.Enemy
{
	public class EnemyController : IStartable, ITickable, IDisposable
	{
		[Inject] private GameplayController _gameplay;

		[Inject] private UnitController _unitContr;
		[Inject] private ProgressController _progrss;
		[Inject] private ControlModule _control;

		public Transform StartPoint { get; set; }
		public Vector3 TargetPoint { get; set; }

		private List<EnemyItem> _items = new List<EnemyItem>();
		private bool _isStopGame = false;
		private bool _isStartGame = false;

		public void Start()
		{
			_gameplay.OnInit += Init;
			_gameplay.OnEndGame += StopGame;
			_control.TouchStart += OnStart;
		}

		public void OnStart(PointerEventData eventData)
		{
			_control.TouchStart -= OnStart;
			_isStartGame = true;
		}

		private void Init(GameData data)
		{
			var progress = _progrss.Save;

			var lvlData = data.Lvls[progress.ProgressLvl - 1];

			foreach (var item in lvlData.MobEnemis)
			{
				EnemyItem enemy = new EnemyItem()
				{
					UnitItem = item.Item.Prefab,
					TimeAwait = item.TimeAwait,
					OnSpawn = Spawn,
					Dam = item.PowerItem
				};

				_items.Add(enemy);
			}

			foreach (var item in lvlData.ChampEnemis)
			{
				EnemyItem enemy = new EnemyItem()
				{
					UnitItem = item.Item.Prefab,
					TimeAwait = item.TimeAwait,
					OnSpawn = Spawn,
					Dam = item.PowerItem
				};

				_items.Add(enemy);
			}
		}

		public void Spawn(Unit unit, float dam) => _unitContr.Spawn(unit, GetStartPoint(), StartPoint.rotation, TargetPoint, dam);

		private Vector3 GetStartPoint()
		{
			float x = UnityEngine.Random.Range(-1f, 1f);
			float z = UnityEngine.Random.Range(-1f, 1f);
			return new Vector3(StartPoint.position.x + x, 0, StartPoint.position.z + z);
		}

		public void Tick()
		{
			if (_isStopGame) return;
			if (!_isStartGame) return;

			foreach (var item in _items)
			{
				item.UpdateItem();
			}
		}

		public void Dispose()
		{
			_gameplay.OnInit -= Init;
			_gameplay.OnEndGame -= StopGame;
		}

		private void StopGame(bool value)  => _isStopGame = true;
	}

	public class EnemyItem
	{
		public Unit UnitItem;
		public float TimeAwait;
		public Action<Unit, float> OnSpawn;
		public float Dam;

		private float _curTime = 0f;

		public void UpdateItem()
		{
			_curTime += Time.deltaTime;
			
			if (_curTime > TimeAwait)
			{
				_curTime = 0f;
				OnSpawn?.Invoke(UnitItem, Dam);
			}
		}
	}
}