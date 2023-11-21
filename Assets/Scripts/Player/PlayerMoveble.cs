using Core;
using UnityEngine;
using UnityEngine.EventSystems;
using VContainer;

namespace GameplaySystem.Player
{
	public class PlayerMoveble : MonoBehaviour
	{
		[Inject] private ControlModule _control;
		[Inject] private AssetLoader _assetLoader;

		private float _speedMove = 0.1f;

		private Vector3 _direction = new Vector3();

		private async void Start()
		{
			_control.TouchMoved += OnDrag;

			GameData data = await _assetLoader.LoadConfig(Constants.GameData) as GameData;
			_speedMove = data.PlayerParm.Speed;
		}

		private void OnDestroy()
		{
			_control.TouchMoved -= OnDrag;
		}

		private void OnDrag(PointerEventData eventData)
		{
			if (eventData.delta.x > 0)
			{
				if (transform.position.x > 1.2f) return;
				_direction = new Vector3(_speedMove, 0, 0);
			}
			else if (eventData.delta.x < 0)
			{
				if (transform.position.x < -1.2f) return;

				_direction = new Vector3(-_speedMove, 0, 0);
			}

			transform.Translate(_direction, Space.World);
		}
	}
}