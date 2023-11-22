using Core;
using GameplaySystem;
using System;
using UnityEngine;
using VContainer;
using Object = UnityEngine.Object;
using VContainer.Unity;

namespace UI
{
	public class MenuController : IStartable, IDisposable
	{
		[Inject] private AssetLoader _assetLoader;

		public Action<ItemMenu> OnInitMenuItem;
		public Action<GameData> OnInit;
		public Action<Transform, Transform> OnInitParent;

		private ItemMenu _activeView;

		public void Start()
		{
			OnInitParent += Init;
		}

		private async void Init(Transform btnsParent, Transform viewsParent)
		{
			OnInitParent -= Init;

			var gameData = await _assetLoader.LoadConfig(Constants.GameData) as GameData;
			var dataMenu = await _assetLoader.LoadConfig(Constants.MenuData) as MenuConfig;

			foreach (var viewItem in dataMenu.ViewItems)
			{
				if (viewItem.Enable)
				{
					var btn = Object.Instantiate(dataMenu.BtnPrefab);
					btn.name = viewItem.ViewType.ToString();
					btn.transform.SetParent(btnsParent);
					var view = Object.Instantiate(viewItem.View, viewsParent);
					view.name = viewItem.ViewType.ToString();

					ItemMenu item = new ItemMenu()
					{
						Type = viewItem.ViewType,
						Btn = btn,
						View = view,
					};

					item.Active(dataMenu.StartViewType == viewItem.ViewType);
					if (dataMenu.StartViewType == viewItem.ViewType)
					{
						_activeView = item;
					}

					btn.Init(() => OnClick(item), viewItem.IconSprite);
					OnInitMenuItem?.Invoke(item);
				}
			}

			OnInit?.Invoke(gameData);
		}

		private void OnClick(ItemMenu item)
		{
			_activeView.Active(false);
			_activeView = item;
			_activeView.Active(true);
		}

		public void Dispose()
		{
			OnInitParent -= Init;
		}
	}

	public class ItemMenu
	{
		public Views Type;
		public BtnMenuView Btn;
		public ViewItem View;

		public void Active(bool value)
		{
			Btn.ActiveBtn(value);
			View.ActiveBody(value);
		}
	}
}
