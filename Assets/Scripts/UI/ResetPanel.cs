using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class ResetPanel : MonoBehaviour
	{
		[SerializeField] private Button _openResetPanelBtn;
		[SerializeField] private Button _closeResetPanelBtn;
		[SerializeField] private Button _resetPanelBtn;
		[SerializeField] private GameObject _body;

		private Action _onReset;

		public void Init(Action onReset)
		{
			_openResetPanelBtn.onClick.AddListener(() => ActiveResetPanel(true));
			_closeResetPanelBtn.onClick.AddListener(() => ActiveResetPanel(false));
			_resetPanelBtn.onClick.AddListener(ResetSave);
			_onReset = onReset;
		}

		private void ResetSave()
		{
			_body.SetActive(false);
			_onReset?.Invoke();
		}

		private void ActiveResetPanel(bool value)
		{
			_body.SetActive(value);
		}
	}
}
