using UI;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

namespace GameplaySystem
{
	public class EndGamePanel : MonoBehaviour, IStartable
	{
		[Inject] private GameplayController _gameplay;

		[SerializeField] private GameObject _winPanel;
		[SerializeField] private GameObject _losePanel;
		[SerializeField] private Button _exitBtn;
		[SerializeField] private RewardView _rewardView;
		[SerializeField] private Transform _rewardParent;

		public void Start()
		{
			_winPanel.SetActive(false);
			_losePanel.SetActive(false);

			_gameplay.OnEndGame += ShowPanel;

			_exitBtn.onClick.AddListener(Exit);
			_exitBtn.gameObject.SetActive(false);
		}

		private void OnDestroy()
		{
			_gameplay.OnEndGame -= ShowPanel;
		}

		private void Exit()
		{
			_exitBtn.interactable = false;
			_gameplay.Exit();
		}

		private void ShowPanel(bool value)
		{
			if (value)
			{
				_winPanel.SetActive(true);
			}
			else
			{
				_losePanel.SetActive(true);

			}

			_exitBtn.gameObject.SetActive(true);
		}

		[Inject] private MenuController _menuContr;

		private LvlProgressView _lvlProgress;

		private void InitData(GameData data)
		{
			_lvlProgress.Init(data.Lvls);
		}

	}
}
