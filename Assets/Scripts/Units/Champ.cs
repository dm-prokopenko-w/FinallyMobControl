using System;
using UnityEngine;

namespace GameplaySystem.Units
{
	public class Champ : Unit
	{
		public override void SetTarget(string tag, Vector3 pos, Action<Collider, Collider> onTrigger, float power)
		{
			base.SetTarget(tag, pos, onTrigger, power);
			Move(pos);
		}
	}
}