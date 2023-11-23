using GameplaySystem;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

namespace UI
{
	public class Preloader : MonoBehaviour
	{
		[SerializeField] private GameObject _body;
		[SerializeField] private Image _loading;

		private float _curTime;
		private float _timeAwait = 1f;

		private void Start()
		{
			_body.SetActive(true);
		}

		private void Update()
		{
			_curTime += Time.deltaTime;
			_loading.fillAmount = _curTime / _timeAwait;

			if (_curTime > _timeAwait)
			{
				_body.SetActive(false);

				_curTime = 0f;
			}
		}
	}
}
