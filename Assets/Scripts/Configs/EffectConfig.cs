using Game.Configs;
using UnityEngine;

namespace Core
{
	[CreateAssetMenu(fileName = "VFXData", menuName = "Data/VFXData", order = 0)]
	public class EffectConfig : Config
	{
		public GameObject DeadUnit;
		public GameObject SpawnUnit;
	}
}
