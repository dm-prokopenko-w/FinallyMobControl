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

            builder.Register<AssetLoader>(Lifetime.Scoped);
            builder.Register<ControlModule>(Lifetime.Scoped);
            builder.Register<ControlModule>(Lifetime.Scoped);
            builder.Register<ObjectPool>(Lifetime.Scoped);
            builder.Register<UnitController>(Lifetime.Scoped).As<UnitController, IStartable, IDisposable>();
            builder.Register<PlayerController>(Lifetime.Scoped).As<PlayerController, IStartable, IDisposable>();
            builder.Register<EnemyController>(Lifetime.Scoped).As<EnemyController, IStartable, IDisposable>();
            /*
            builder.Register<AIModule>(Lifetime.Scoped).As<AIModule, ITickable>();
            builder.Register<GameplayManager>(Lifetime.Scoped);
            builder.Register<BasesController>(Lifetime.Scoped).As<BasesController, ITickable>();
            builder.Register<UnitsManager>(Lifetime.Scoped);
            builder.Register<AIController>(Lifetime.Scoped);
            builder.Register<CameraController>(Lifetime.Scoped);
            */
        }
    }
}