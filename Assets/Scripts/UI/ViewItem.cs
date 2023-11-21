using UnityEngine;

namespace UI
{
	public class ViewItem : MonoBehaviour
	{
		[SerializeField] private GameObject _body;

		public void ActiveBody(bool value) => _body.SetActive(value);
	}
}