using System;
using UnityEngine;
using UnityEngine.AI;

namespace GameplaySystem.Units
{
	public class UnitBody : MonoBehaviour
	{
		[SerializeField] private NavMeshAgent _agent;
		[SerializeField] private Collider _col;

		public Action<Collider, Collider> OnTrigger;

		public NavMeshAgent Agent => _agent;
		public Collider Col => _col;

		private void OnTriggerEnter(Collider other)
		{
			if (gameObject.tag == other.tag) return;
			OnTrigger?.Invoke(Col, other);
		}
	}
}