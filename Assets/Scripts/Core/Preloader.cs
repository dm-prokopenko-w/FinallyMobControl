using GameplaySystem;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

namespace UI
{
	public class Preloader : MonoBehaviour, IStartable
	{
		[Inject] private MenuController _menuContr;

		[SerializeField] private GameObject _body;
		[SerializeField] private Image _loading;

		private float _curTime;
		private float _timeAwait = 3f;

		public void Start()
		{
			_body.SetActive(true);
			_menuContr.OnInit += Hide;
		}

		private async void Hide(GameData gameData)
		{
			int time = (int)(_timeAwait * 1000f);
			await Task.Delay(time);
			_body.SetActive(false);
		}

		private void Update()
		{
			_curTime += Time.deltaTime;
			_loading.fillAmount = _curTime / _timeAwait;

			if (_curTime > _timeAwait)
			{
				_curTime = 0f;
			}
		}
	}
}
