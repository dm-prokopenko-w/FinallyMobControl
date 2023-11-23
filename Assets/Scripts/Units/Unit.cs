using Core;
using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace GameplaySystem.Units
{
	public class Unit : MonoBehaviour
	{
		[SerializeField] private UnitBody _body;
		[SerializeField] private Animator _anim;

		public Collider Col => _body.Col;
		public float HP { get; protected set; }
		public float Dam { get; protected set; }

		private Vector3 _target;

		public virtual void SetTarget(string tag, Vector3 pos, Action<Collider, Collider> onTrigger, float power)
		{
			this.gameObject.tag = tag;
			HP = power;
			Dam = power;

			_body.OnTrigger = onTrigger;
			_target = pos;
		}

		private IEnumerator Atack(Action OnDestroy)
		{
			_body.Agent.ResetPath();
			_anim.SetTrigger(Constants.AtackAnim);
			if (HP <= 0)
			{
				Col.enabled = false;
			}
			yield return new WaitForSeconds(2.5f);
			if (HP <= 0)
			{
				OnDestroy?.Invoke();
			}
			else
			{
				Move();
			}
		}

		public void Atack(float dam, Action OnDestroy)
		{
			HP -= dam;
			StartCoroutine(Atack(OnDestroy));
		}

		public async void Move()
		{
			Col.enabled = true;

			await Task.Delay(100);
			if (!_body.Agent.isActiveAndEnabled) return;
			_body.Agent.SetDestination(_target);
			_anim.SetTrigger(Constants.MoveAnim);
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