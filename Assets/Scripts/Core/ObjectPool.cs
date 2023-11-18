using System.Collections.Generic;
using UnityEngine;
using GameplaySystem.Units;

namespace Core
{
    public class ObjectPool
	{
		private Dictionary<string, Pool> _pools = new Dictionary<string, Pool>();

        public void Preload(Unit prefab, Transform container, int count)
        {
            InitPool(prefab, container);
			Unit[] objs = new Unit[count];
            for (int i = 0; i < count; i++)
            {
                objs[i] = Spawn(prefab, Vector3.zero, Quaternion.identity, container);
            }

            for (int i = 0; i < count; i++)
            {
                Despawn(objs[i]);
            }
        }

        private void InitPool(Unit prefab, Transform container)
        {
            if (prefab != null && !_pools.ContainsKey(prefab.name))
            {
                _pools[prefab.name] = new Pool(prefab, container);
            }
        }

        public Unit Spawn(Unit prefab, Vector3 pos, Quaternion rot, Transform container)
        {
            InitPool(prefab, container);
            return _pools[prefab.gameObject.name].Spawn(pos, rot, container);
        }

        public void Despawn(Unit obj)
        {
            if (_pools.ContainsKey(obj.name))
            {
                _pools[obj.name].Despawn(obj);
            }
            else
            {
                Object.Destroy(obj);
            }
        }

        class Pool
        {
            private List<Unit> _inactive = new List<Unit>();
            private Unit _prefab;
            private Transform _container;

            public Pool(Unit prefab, Transform container)
            {
                _prefab = prefab;
                _container = container;
            }

            public Unit Spawn(Vector3 pos, Quaternion rot, Transform container)
            {
				Unit obj;
                if (_inactive.Count == 0)
                {
                    obj = Object.Instantiate(_prefab, pos, rot, container);
                    obj.name = _prefab.name;
                }
                else
                {
                    obj = _inactive[_inactive.Count - 1];
                    _inactive.RemoveAt(_inactive.Count - 1);
                }

                obj.transform.position = pos;
                obj.transform.rotation = rot;
                obj.gameObject.SetActive(true);
                return obj;
            }

            public void Despawn(Unit obj)
            {
                obj.gameObject.SetActive(false);
                obj.transform.SetParent(_container);
                _inactive.Add(obj);
            }
        }
    }
}