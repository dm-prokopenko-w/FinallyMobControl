using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace GameplaySystem.Units
{
	public class Unit : MonoBehaviour
	{
		[SerializeField] private NavMeshAgent _agent;
		[SerializeField] private Collider _col;

		public Collider Col => _col;
		public float HP { get; protected set; }
		public float Dam { get; protected set; }

		private Action<Collider, Collider> _onTrigger;

		public virtual void SetTarget(string tag, Vector3 pos, Action<Collider, Collider> onTrigger, float power)
		{
			this.gameObject.tag = tag;
			HP = power;
			Dam = power;
			_onTrigger = onTrigger;
		}

		public void SetDamage(float dam, Action OnDestroy)
		{
			HP -= dam;

			if(HP <= 0 )
			{
				OnDestroy?.Invoke();
			}
		}

		public async void Move(Vector3 targetPos)
		{
			await Task.Delay(100);
			_agent.SetDestination(targetPos);
		}

		private void OnTriggerEnter(Collider other)
		{
			_onTrigger?.Invoke(_col, other);
		}

	}
}