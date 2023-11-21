using TMPro;
using UnityEngine.UI;
using UnityEngine;

namespace UI
{
	public class RewardView : MonoBehaviour
	{
		[SerializeField] private Image _icon;
		[SerializeField] private TMP_Text _counter;

		public void Init(Sprite icon, int count)
		{
			_icon.sprite = icon;
			_counter.text = count.ToString();
		}
	}
}
