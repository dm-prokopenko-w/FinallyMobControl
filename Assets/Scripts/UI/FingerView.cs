using Core;
using UnityEngine;
using UnityEngine.EventSystems;
using VContainer;
using VContainer.Unity;

namespace UI
{
	public class FingerView : MonoBehaviour	, IStartable
	{
		[Inject] private ControlModule _control;

		[SerializeField] private float _minX = -0.2f;
		[SerializeField] private float _maxX = 0.2f;
		[SerializeField] private float _speed = 0.8f;

		private Vector3 _minPosX;
		private Vector3 _maxPosX;
		private Vector3 _target;

		public void Start()
		{
			_control.TouchStart += OnTouchStart;

			_minPosX = new Vector3(_minX, 0, 0);
			_maxPosX = new Vector3(_maxX, 0, 0);
			_target = _minPosX;
			gameObject.SetActive(true);
		}

		private void OnTouchStart(PointerEventData data)
		{
			_control.TouchStart -= OnTouchStart;
			gameObject.SetActive(false);
		}

		private void Update()
		{
			if (transform.position.x > _maxX)
			{
				_target = _minPosX;
			}
			else if (transform.position.x < _minX)
			{
				_target = _maxPosX;
			}

			transform.Translate(_target * Time.deltaTime * _speed, Space.World);
		}
	}
}
