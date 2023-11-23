using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace Core
{
	public class ControllerVFX : IStartable, IDisposable
	{
		[Inject] private AssetLoader _assetLoader;

		public Action<Transform> OnInitParent;
		private Transform _parent;

		private List<GameObject> _effects = new List<GameObject>();

		private GameObject _deadUnitPrefab;
		private GameObject _spawnUnitPrefab;

		[Inject]
		public void Construct()
		{
			OnInitParent += InitParent;
		}

		public async void Start()
		{
			var data = await _assetLoader.LoadConfig(Constants.VFXData) as EffectConfig;
            _deadUnitPrefab = data.DeadUnit;
            _spawnUnitPrefab = data.SpawnUnit;
		}

		private void InitParent(Transform tr) => _parent = tr;

		public void DeadUnit(Vector3 pos) => Spawn(pos, _deadUnitPrefab);

		public void SpawnUnit(Vector3 pos) => Spawn(pos, _spawnUnitPrefab);

		private void Spawn(Vector3 pos, GameObject prefab)
		{
			if (_effects.Count > 0)
			{
				var activeObj = _effects.Find(x => prefab.name.Equals(x.name));
				if (activeObj != null && !activeObj.activeSelf)
				{
					activeObj.transform.position = pos;
					activeObj.SetActive(true);
					return;
				}
			}

			var obj = Object.Instantiate(prefab);
			obj.transform.position = pos;
			obj.name = prefab.name;
			_effects.Add(obj);
			obj.transform.SetParent(_parent);
		}

		public void Dispose()
		{
			OnInitParent -= InitParent;
		}
	}
}