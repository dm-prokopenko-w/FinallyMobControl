using System.Collections.Generic;
using UnityEngine;
using GameplaySystem.Units;

namespace Core
{
    public class UnitObjectPool
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
				Unit unit;
                if (_inactive.Count == 0)
                {
                    unit = Object.Instantiate(_prefab, pos, rot, container);
                    unit.name = _prefab.name;
                }
                else
                {
                    unit = _inactive[_inactive.Count - 1];
                    _inactive.RemoveAt(_inactive.Count - 1);
                }

                unit.SetStartPos(unit.transform.position, unit.transform.rotation);
                unit.Active(true);
                return unit;
            }

            public void Despawn(Unit unit)
            {
                unit.Active(false);
                unit.transform.SetParent(_container);
                _inactive.Add(unit);
            }
        }
    }
}