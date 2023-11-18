using Core;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GameplaySystem.Units
{
	public class Base : Unit, IStartable
	{
		[Inject] private UnitController _unitContr;

		[SerializeField] private Team _team;

		public async void Start()
		{
			var power = await _unitContr.AddBase(_team, this);
			HP = power;
			Dam = power;
		}
	}
}
