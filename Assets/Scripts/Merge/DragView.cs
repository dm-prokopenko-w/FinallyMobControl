using UnityEngine;
using UnityEngine.UI;

namespace Merge
{
	public class DragView : MonoBehaviour
	{
		[SerializeField] private Image _icon;

		public void Init(Sprite icon)
		{
			_icon.sprite = icon;
		}
	}
}