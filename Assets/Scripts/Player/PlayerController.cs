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
		[Inject] private ControlModule _control;
		[Inject] private ProgressController _progrss;
		[Inject] private AssetLoader _assetLoader;
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

		public async void Start()
		{
			_control.TouchStart += OnStart;
			_control.TouchMoved += OnDrag;
			_control.TouchEnd += OnEnd;

			var progress = _progrss.GetSave();

			GameData data = await _assetLoader.LoadConfig(Constants.GameData) as GameData;

			_mobPrefab = data.Mobs[progress.NumMod].Prefab as Mob;
			_champPrefab = data.Champs[progress.NumChamp].Prefab as Champ;

			_mobPower = data.Mobs[progress.NumMod].Powers[progress.NumModPower];
			_champPower = data.Mobs[progress.NumMod].Powers[progress.NumChampPower];
		}

		public void Dispose()
		{
			_control.TouchStart -= OnStart;
			_control.TouchMoved -= OnDrag;
			_control.TouchEnd -= OnEnd;
		}

		public void OnStart(PointerEventData eventData)
		{
			_currentTime = 0;
			Step();
			_unitContr.Spawn(_mobPrefab, StartPoint.position, StartPoint.rotation, TargetPoint, _mobPower);
		}

		public void OnDrag(PointerEventData eventData)
		{
			_currentTime += Time.deltaTime;

			if (_currentTime > _timeAwait)
			{
				_currentTime = 0;
				Step();
				_unitContr.Spawn(_mobPrefab, StartPoint.position, StartPoint.rotation, TargetPoint, _mobPower);
			}
		}

		public void OnEnd(PointerEventData eventData)
		{
			if (_curStep >= _maxStep)
			{
				_curStep = 0;
				_unitContr.Spawn(_champPrefab, StartPoint.position, StartPoint.rotation, TargetPoint, _champPower);
			}
		}

		private void Step()
		{
			_curStep++;
			OnStep?.Invoke(_curStep / 100f);
		}
	}
}
