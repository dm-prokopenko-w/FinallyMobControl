using Game.Configs;
using System.Collections.Generic;
using UnityEngine;

namespace GameplaySystem.Units
{
	[CreateAssetMenu(fileName = "UnitConfig", menuName = "Data/UnitConfig", order = 0)]
	public class UnitConfig : Config
	{
		public Unit Prefab;
		public List<int> Powers;
		public Sprite Icon;
	}
}