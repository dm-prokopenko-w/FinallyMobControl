using GameplaySystem.Player;
using UnityEngine;
using VContainer;
using Core;
using GameplaySystem.Enemy;
using VContainer.Unity;
using GameplaySystem.Units;

namespace GameplaySystem
{
	public class SpawnerPoint : MonoBehaviour
	{
		[Inject] private PlayerController _playerContr;
		[Inject] private EnemyController _enemyContr;
		[Inject] private UnitController _unitContr;

		[SerializeField] private Team _team;

		private void Start()
		{
			if (_team == Team.Player)
			{
				_playerContr.StartPoint = transform;
				_enemyContr.TargetPoint = transform.position;
			}
			else if(_team == Team.Enemy)
			{
				_playerContr.TargetPoint = transform.position;
				_enemyContr.StartPoint = transform;
			}
			else
			{
				_unitContr.SpawnPoint = transform;
			}
		}
	}
}