using Core;
using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GameplaySystem.Units
{
	public class Base : MonoBehaviour, IStartable
	{
		[Inject] private UnitController _unitContr;

		[SerializeField] private Team _team;
		[SerializeField] private Collider _col;

		public float HP { get; private set; }
		public float Dam { get; private set; }
		public Collider Col => _col;
		private Action<Collider, Collider> _onTrigger;

		public async void Start()
		{
			var power = await _unitContr.AddBase(_team, this);
			HP = power.Item1;
			Dam = power.Item1;
			_onTrigger = power.Item2;
		}

		public void Atack(float dam, Action OnDestroy)
		{
			HP -= dam;
			if (HP <= 0) OnDestroy?.Invoke();
		}

		private void OnTriggerEnter(Collider other)
		{
			if (gameObject.tag == other.tag) return;
			_onTrigger?.Invoke(Col, other);
		}
	}
}