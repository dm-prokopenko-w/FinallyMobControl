using UnityEngine;
using VContainer;

namespace Core
{
	public class EffectParent : MonoBehaviour
	{
		[Inject] private ControllerVFX _effect;

		private void Start()
		{
			_effect.OnInitParent?.Invoke(transform);
		}
	}
}
