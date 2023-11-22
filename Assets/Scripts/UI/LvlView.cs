using Core;
using TMPro;
using UnityEngine;
using VContainer;

namespace GameplaySystem
{
	public class LvlView : MonoBehaviour
	{
		[Inject] private ProgressController _progress;

		[SerializeField] private TMP_Text _count;

		private void Start()
		{
			_count.text = Constants.LvlView + _progress.Save.LoadLvl;
		}
	}
}
