using Core;
using GameplaySystem.Enemy;
using GameplaySystem.Player;
using GameplaySystem.Units;
using TMPro;
using UnityEngine;
using VContainer;

public class Multiplier : MonoBehaviour
{
	private enum Dir
	{
		Left,
		Right
	}

	[Inject] private UnitController _unitContr;
	[Inject] private PlayerController _playerContr;
	[Inject] private EnemyController _enemyContr;

	[SerializeField] private TMP_Text _counter;
	[SerializeField] private int _multCount = 2;
	[SerializeField] private Team _team;
	[SerializeField] private bool _isMoveble = false;
	[SerializeField] private float _minX = -1.7f;
	[SerializeField] private float _maxX = 1.7f;
	[SerializeField] private float _speed = 1f;
	[SerializeField] private Dir _startDir;

	private Vector3 _minPosX;
	private Vector3 _maxPosX;
	private Vector3 _target;

	private void Start()
	{
		_counter.text = "x" + _multCount;

		_minPosX = new Vector3(_minX, 0, 0);
		_maxPosX = new Vector3(_maxX, 0, 0);
		if (_startDir == Dir.Left)
		{
			_target = _minPosX;
		}
		else
		{
			_target = _maxPosX;
		}
	}

	private void OnTriggerEnter(Collider col)
	{
		Unit unit = _unitContr.GetUnitByCol(col);
		if (unit == null) return;

		if (_team == Team.All)
		{
			if (col.tag.Equals(Constants.PlayerTag))
			{
				for (int i = 0; i < _multCount; i++)
				{
					_playerContr.Spawn(unit, new Vector3(col.transform.position.x, col.transform.position.y, col.transform.position.z + 2), unit.Dam);
				}
			}
			else if (col.tag.Equals(Constants.EnemyTag))
			{
				for (int i = 0; i < _multCount; i++)
				{
					_enemyContr.Spawn(unit, unit.Dam);
				}
			}
		}
		else if (_team == Team.Player)
		{
			if (col.tag.Equals(Constants.PlayerTag))
			{
				for (int i = 0; i < _multCount; i++)
				{
					_playerContr.Spawn(unit, new Vector3(col.transform.position.x, col.transform.position.y, col.transform.position.z + 2), unit.Dam);
				}
			}
		}
		else if (_team == Team.Enemy)
		{
			if (col.tag.Equals(Constants.EnemyTag))
			{
				for (int i = 0; i < _multCount; i++)
				{
					_enemyContr.Spawn(unit, unit.Dam);
				}
			}
		}
	}

	private void Update()
	{
		if (!_isMoveble) return;

		if (transform.position.x > _maxX)
		{
			_target = _minPosX;
		}
		else if (transform.position.x < _minX)
		{
			_target = _maxPosX;
		}

		transform.Translate(_target * Time.deltaTime * _speed, Space.World);
	}
}
