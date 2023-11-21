using Game.Configs;
using GameplaySystem.Units;
using Merge;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameplaySystem
{
	[CreateAssetMenu(fileName = "GameData", menuName = "Data/GameData", order = 0)]
	public class GameData : Config
	{
		public PlayerData PlayerParm;
		public List<LvlData> Lvls;
		public DragDropItem DropPrefab;
	}

	[Serializable]
	public class PlayerData
	{
		public List<UnitConfig> Mobs;
		public List<UnitConfig> Champs;
		public float Speed = 0.1f;
		public int BasePower = 1;
	}

	[Serializable]
	public class LvlData
	{
		public int EnemyBasePower = 150;
		public GameObject LvlEnv;
		public List<RewardItem> Rewards;
		public List<EnemyItemData> MobEnemis;
		public List<EnemyItemData> ChampEnemis;
	}

	[Serializable]
	public class RewardItem
	{
		public UnitConfig Item;
		public int Count;
	}

	[Serializable]
	public class EnemyItemData
	{
		public UnitConfig Item;
		public float TimeAwait = 1f;
		public int PowerItem = 1;
	}
}