using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

namespace GameplaySystem.Player
{
	public class PlayerParmView : MonoBehaviour, IStartable
	{
		[Inject] private PlayerController _playerContr;

		[SerializeField] private Image _bar;
		[SerializeField] private GameObject _maxObj;

		public void Start()
		{
			_playerContr.OnStep += OnStep;
		}

		private void OnDestroy()
		{
			_playerContr.OnStep -= OnStep;
		}

		private void OnStep(float value)
		{
			_bar.fillAmount = value;
			_maxObj.SetActive(value >= 1);
		}
	}
}
