using System;
using UnityEngine;
using VContainer.Unity;

namespace GameplaySystem.Enemy
{
	public class EnemyController  :  IStartable, IDisposable
	{
		public Transform SpawnPoint { get; set; }
		public Transform StartPoint { get; set; }
		public Vector3 TargetPoint { get; set; }

		public void Dispose()
		{
		}

		public void Start()
		{
		}
	}
}
