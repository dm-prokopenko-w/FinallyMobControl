using VContainer;
using VContainer.Unity;
using Core;
using GameplaySystem.Player;
using System;
using GameplaySystem.Enemy;
using GameplaySystem.Units;

namespace GameplaySystem
{
	public class GameplayBootStarter : BootStarter
	{
		protected override void Configure(IContainerBuilder builder)
		{
			base.Configure(builder);

			builder.Register<GameplayController>(Lifetime.Scoped).As<GameplayController, IStartable, IDisposable>();

			builder.Register<UnitObjectPool>(Lifetime.Scoped);

			builder.Register<UnitController>(Lifetime.Scoped).As<UnitController, IStartable, IDisposable>();
			builder.Register<PlayerController>(Lifetime.Scoped).As<PlayerController, IStartable, IDisposable>();
			builder.Register<EnemyController>(Lifetime.Scoped).As<EnemyController, IStartable, ITickable>();
		}
	}
}