using Core;
using Game.Configs;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
	[CreateAssetMenu(fileName = "MenuData", menuName = "Data/MenuData", order = 0)]
	public class MenuConfig : Config
	{
		public BtnMenuView BtnPrefab;
		public Views StartViewType = Views.Battle;
		public List<MenuItemView> ViewItems;
	}

	[Serializable]
	public class MenuItemView
	{
		public bool Enable;
		public Views ViewType;
		public Sprite IconSprite;
		public ViewItem View;
	}
}
