using Game.Configs;
using GameplaySystem.Units;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameplaySystem
{
    [CreateAssetMenu(fileName = "GameData", menuName = "Data/GameData", order = 0)]
    public class GameData : Config
    {
        public PlayerData PlayerParm;
        public List<UnitConfig> Mobs;
        public List<UnitConfig> Champs;
        public List<LvlData> Lvls;
    }

    [Serializable]
    public class PlayerData
    {
        public float Speed = 0.1f;
		public int BasePower = 1;
	}

	[Serializable]
    public class LvlData
    {
		public int BasePower = 10;
	}
}