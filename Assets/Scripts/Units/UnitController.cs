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
		[Inject] private ObjectPool _pool;
		[Inject] private AssetLoader _assetLoader;
		[Inject] private ProgressController _progrss;

		public Action<Collider, Collider> OnTrigger;
		public Transform SpawnPoint { get; set; }

		private Base _playerBase;
		private List<Base> _enenyBases = new List<Base>();

		private List<Unit> _units = new List<Unit>();

		public  void Start()
		{
			OnTrigger += OnOnTrigger;
		}

		public async Task<float> AddBase(Team team, Base b)
		{
			var power = 0;
			var progress = _progrss.GetSave();
			var data = await _assetLoader.LoadConfig(Constants.GameData) as GameData;

			if (team == Team.Player)
			{
				_playerBase = b;
				power = data.PlayerParm.BasePower;
			}
			else if (team == Team.Enemy)
			{
				_enenyBases.Add(b);
				power = data.Lvls[progress.Lvl - 1].BasePower;

			}
			return power;
		}

		private void OnOnTrigger(Collider thisCol, Collider enterCol)
		{
			var thisUnit = _units.Find(x => x.Col == thisCol);
			var enterUnit = _units.Find(x => x.Col == enterCol);

			if (enterUnit != null)
			{
				if (thisUnit.tag == Constants.PlayerTag && enterUnit.tag == Constants.EnemyTag)
				{
					SetDamages(thisUnit, enterUnit, () => DestroyUnit(thisUnit), () => DestroyUnit(enterUnit));
				}
				else if (thisUnit.tag == Constants.EnemyTag && enterUnit.tag == Constants.PlayerTag)
				{
					SetDamages(thisUnit, enterUnit, () => DestroyUnit(thisUnit), () => DestroyUnit(enterUnit));
				}

				return;
			}

			if (enterCol == _playerBase.Col && thisUnit.tag == Constants.EnemyTag)
			{
				var onDestroyThisUnit = new Action(() =>
				{
					DestroyUnit(thisUnit);
				});

				var onDestroyEnterUnit = new Action(() =>
				{
					Debug.Log("You lose");

					DestroyUnit(enterUnit);
				});

				SetDamages(thisUnit, enterUnit, onDestroyThisUnit, onDestroyEnterUnit);
				return;
			}

			enterUnit = _enenyBases.Find(x => x.Col == enterCol);
			if (enterUnit != null && thisUnit.tag == Constants.PlayerTag)
			{
				var onDestroyThisUnit = new Action(() =>
				{
					DestroyUnit(thisUnit);
				});

				var onDestroyEnterUnit = new Action(() =>
				{
					Debug.Log("You win");

					DestroyUnit(enterUnit);
				});

				SetDamages(thisUnit, enterUnit, onDestroyThisUnit, onDestroyEnterUnit);
			}
		}

		private void SetDamages(Unit thisUnit, Unit enterUnit, Action onDestroyThisUnit, Action onDestroyEnterUnit)
		{
			thisUnit.SetDamage(enterUnit.Dam, onDestroyThisUnit);
			enterUnit.SetDamage(thisUnit.Dam, onDestroyEnterUnit);
		}

		private void DestroyUnit(Unit unit)
		{
			_units.Remove(unit);
			_pool.Despawn(unit);
		}

		public void Spawn(Unit unit, Vector3 pos, Quaternion rot, Vector3 target, float power)
		{
			var obj = _pool.Spawn(unit, pos, rot, SpawnPoint);
			obj.SetTarget(Constants.PlayerTag, target, OnTrigger, power);
			AddUnit(obj);
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
		}
	}
}
