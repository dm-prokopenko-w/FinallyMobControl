using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
	public class BtnMenuView : MonoBehaviour
	{
		[SerializeField] private Image _iconActive;
		[SerializeField] private Image _iconDisable;
		[SerializeField] private Image _icon;
		[SerializeField] private Button _btnView;
		[SerializeField] private Animator _anim;

		public void Init(UnityAction onClick, Sprite icon)
		{
			_btnView.onClick.AddListener(onClick);
			_iconActive.sprite = icon;
			_iconDisable.sprite = icon;
		}

		public void ActiveBtn(bool value)
		{
			_btnView.interactable = !value;
			if (value)
			{
				_anim.SetTrigger("ActiveBtn");
			}
			else
			{
				_anim.SetTrigger("DisableBtn");
			}
		}
	}
}
