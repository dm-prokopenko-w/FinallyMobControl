using System;
using System.Threading.Tasks;
using UnityEngine;

namespace GameplaySystem.Units
{
	public class Unit : MonoBehaviour
	{
		[SerializeField] private UnitBody _body;

		public Collider Col => _body.Col;
		public float HP { get; protected set; }
		public float Dam { get; protected set; }

		public virtual void SetTarget(string tag, Vector3 pos, Action<Collider, Collider> onTrigger, float power)
		{
			this.gameObject.tag = tag;
			HP = power;
			Dam = power;
			_body.OnTrigger = onTrigger;
		}

		public void SetDamage(float dam, Action OnDestroy)
		{
			HP -= dam;
			if (HP <= 0) OnDestroy?.Invoke();
		}

		public async void Move(Vector3 targetPos)
		{
			await Task.Delay(10);
			if (!_body.Agent.isActiveAndEnabled) return;
			_body.Agent.SetDestination(targetPos);
		}

		public void StopUnit()
		{
			if (!_body.Agent.isActiveAndEnabled) return;
			_body.Agent.Stop();
		}

		public void Active(bool value) => _body.gameObject.SetActive(value);

		public void SetStartPos(Vector3 pos, Quaternion rot)
		{
			_body.transform.position = pos;
			_body.transform.rotation = rot;
		}
	}
}