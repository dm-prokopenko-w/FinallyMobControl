using GameplaySystem.Player;
using UnityEngine;
using VContainer;
using Core;
using GameplaySystem.Enemy;
using GameplaySystem.Units;
using VContainer.Unity;

namespace GameplaySystem
{
	public class SpawnerPoint : MonoBehaviour, IStartable
	{
		[Inject] private PlayerController _playerContr;
		[Inject] private EnemyController _enemyContr;
		[Inject] private UnitController _unitContr;

		[SerializeField] private Team _team;

		public void Start()
		{
			if (_team == Team.Player)
			{
				_playerContr.StartPoint = transform;
				_enemyContr.TargetPoint = transform.position;
			}
			else if(_team == Team.Enemy)
			{
				_enemyContr.StartPoint = transform;
				_playerContr.TargetPoint = transform.position;
			}
			else
			{
				_unitContr.SpawnPoint = transform;
			}
		}
	}
}