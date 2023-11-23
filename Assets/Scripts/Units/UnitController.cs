using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Core;
using System.Threading.Tasks;

namespace GameplaySystem.Units
{
	public class UnitController : IStartable, IDisposable
	{
		[Inject] private GameplayController _gameplay;
		[Inject] private ControllerVFX _effect;

		[Inject] private UnitObjectPool _pool;
		[Inject] private ProgressController _progrss;

		public Action<Collider, Collider> OnTrigger;
		public Transform SpawnPoint { get; set; }

		private Base _playerBase;
		private List<Base> _enenyBases = new List<Base>();

		private List<Unit> _units = new List<Unit>();

		private float _playerBasePower;
		private float _enemyBasePower;
		private bool _inInit = false;

		public void Start()
		{
			OnTrigger += OnOnTrigger;
			_gameplay.OnInit += Init;
			_gameplay.OnEndGame += StopGame;
		}

		private void Init(GameData data)
		{
			var progress = _progrss.Save;
			_playerBasePower = data.PlayerParm.BasePower;
			_enemyBasePower = data.Lvls[progress.LoadLvl - 1].EnemyBasePower;
			OnInit();
		}

		private void OnInit() => _inInit = true;

		public async Task<(float, Action<Collider, Collider>)> AddBase(Team team, Base b)
		{
			if (team == Team.Player)
			{
				_playerBase = b;
				await Task.Run(OnInit);
				return (_playerBasePower, OnOnTrigger);
			}
			else if (team == Team.Enemy)
			{
				_enenyBases.Add(b);
				await Task.Run(OnInit);
				return (_enemyBasePower, OnOnTrigger);
			}

			return (0, OnTrigger);
		}

		private void OnOnTrigger(Collider thisCol, Collider enterCol)
		{
			var thisUnit = _units.Find(x => x.Col == thisCol);
			var enterUnit = _units.Find(x => x.Col == enterCol);

			if (thisUnit != null && enterUnit != null)
			{
				if (thisUnit.tag == Constants.PlayerTag && enterUnit.tag == Constants.EnemyTag)
				{
					var onDestroy = new Action(() =>
					{
						_effect.DeadUnit(thisCol.transform.position);
						DestroyUnit(thisUnit);
					});

					Atack(thisUnit, enterUnit.Dam, onDestroy);
				}
				else if (thisUnit.tag == Constants.EnemyTag && enterUnit.tag == Constants.PlayerTag)
				{
					var onDestroy = new Action(() =>
					{
						_effect.DeadUnit(thisCol.transform.position);
						DestroyUnit(thisUnit);
					});

					Atack(thisUnit, enterUnit.Dam, onDestroy);
				}
				return;
			}

			if (enterCol == _playerBase.Col && thisUnit != null && thisUnit.tag == Constants.EnemyTag)
			{
				var onLose = new Action(() =>
				{
					_gameplay?.OnEndGame(false);
				});
				var onDestroy = new Action(() =>
				{
					_effect.DeadUnit(thisCol.transform.position);
					Atack(thisUnit, _playerBase.Dam, () => DestroyUnit(thisUnit));
				});

				Atack(_playerBase, thisUnit.Dam, onLose);
				Atack(thisUnit, _playerBase.Dam, onDestroy);
				return;
			}

			var enterBase = _enenyBases.Find(x => x.Col == enterCol);
			if (enterBase != null && thisUnit != null && thisUnit.tag == Constants.PlayerTag)
			{
				var onWin = new Action(() =>
				{
					_gameplay?.OnEndGame(true);
				});
				var onDestroy = new Action(() =>
				{
					_effect.DeadUnit(thisCol.transform.position);
					Atack(thisUnit, enterBase.Dam, () => DestroyUnit(thisUnit));
				});

				Atack(enterBase, thisUnit.Dam, onWin);
				Atack(thisUnit, enterBase.Dam, onDestroy);
			}
		}

		private void Atack(Unit unit, float dam, Action onDestroy) =>
			unit.Atack(dam, onDestroy);

		private void Atack(Base b, float dam, Action onDestroy) =>
			b.Atack(dam, onDestroy);

		private void DestroyUnit(Unit unit) => _pool.Despawn(unit);

		public void Spawn(Unit unit, Vector3 pos, Quaternion rot, Vector3 target, float power)
		{
			var obj = _pool.Spawn(unit, pos, rot, SpawnPoint);
			obj.SetTarget(unit.gameObject.tag, target, OnTrigger, power);
			AddUnit(obj);
		}

		public Unit GetUnitByCol(Collider col)
		{
			var unit = _units.Find(x => x.Col == col);
			if (unit != null)
			{
				return unit;
			}
			return null;
		}

		private void AddUnit(Unit unit)
		{
			var u = _units.Find(x => x == unit);

			if (u == null)
			{
				_units.Add(unit);
			}
		}

		public void Dispose()
		{
			OnTrigger -= OnOnTrigger;
			_gameplay.OnInit -= Init;
			_gameplay.OnEndGame -= StopGame;
		}

		private void StopGame(bool value)
		{
			foreach (var unit in _units)
			{
				unit.StopUnit();
			}

			_units.Clear();
		}
	}
}
