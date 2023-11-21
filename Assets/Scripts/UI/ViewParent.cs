using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace UI
{
	public class ViewParent : MonoBehaviour, IStartable
	{
		[Inject] private MenuController _menuContr;

		[SerializeField] private Transform _btnsParent;
		[SerializeField] private Transform _viewsParent;

		public void Start()
		{
			_menuContr.OnInitParent?.Invoke(_btnsParent, _viewsParent);
		}
	}
}