using Core;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using VContainer;
using VContainer.Unity;
using GameplaySystem.Units;

namespace GameplaySystem.Player
{
	public class PlayerController : IStartable, IDisposable
	{
		[Inject] private GameplayController _gameplay;

		[Inject] private ControlModule _control;
		[Inject] private ProgressController _progrss;
		[Inject] private UnitController _unitContr;

		public Transform StartPoint { get; set; }
		public Vector3 TargetPoint { get; set; }

		public Action<float> OnStep;

		private Champ _champPrefab;
		private Mob _mobPrefab;

		private int _curStep = 0;
		private int _maxStep = 100;

		private float _currentTime = 0f;
		private float _timeAwait = 0.15f;

		private float _mobPower;
		private float _champPower;

		private bool _isStopGame = false;

		public void Start()
		{
			_control.TouchStart += OnStart;
			_control.TouchMoved += OnDrag;
			_control.TouchEnd += OnEnd;

			_gameplay.OnInit += Init;
			_gameplay.OnEndGame += StopGame;
		}

		private void Init(GameData data)
		{
			var progress = _progrss.Save;
			_mobPrefab = data.PlayerParm.Mobs[progress.NumMod].Prefab as Mob;
			_champPrefab = data.PlayerParm.Champs[progress.NumChamp].Prefab as Champ;

			_mobPower = data.PlayerParm.Mobs[progress.NumMod].Power;
			_champPower = data.PlayerParm.Mobs[progress.NumMod].Power;
		}

		public void Dispose()
		{
			_control.TouchStart -= OnStart;
			_control.TouchMoved -= OnDrag;
			_control.TouchEnd -= OnEnd;

			_gameplay.OnInit -= Init;
			_gameplay.OnEndGame -= StopGame;
		}

		public void OnStart(PointerEventData eventData)
		{
			if (_isStopGame) return;

			_currentTime = 0;
			Step();
			Spawn(_mobPrefab, StartPoint.transform.position, _mobPower);
		}

		public void OnDrag(PointerEventData eventData)
		{
			if (_isStopGame) return;

			_currentTime += Time.deltaTime;

			if (_currentTime > _timeAwait)
			{
				_currentTime = 0;
				Step();
				Spawn(_mobPrefab, StartPoint.transform.position, _mobPower);
			}
		}

		public void OnEnd(PointerEventData eventData)
		{
			if (_isStopGame) return;

			if (_curStep >= _maxStep)
			{
				Step();
				_curStep = 0;
				Spawn(_champPrefab, StartPoint.transform.position, _champPower);
			}
		}

		public void Spawn(Unit unit, Vector3 startPos, float power)
		{
			if (unit == null) return;

			_unitContr.Spawn(unit, startPos, Quaternion.identity, TargetPoint, power);
		}
		private void StopGame(bool value) => _isStopGame = true;

		private void Step()
		{
			_curStep++;
			OnStep?.Invoke(_curStep / 100f);
		}
	}
}