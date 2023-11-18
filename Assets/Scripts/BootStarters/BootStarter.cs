using Core;
//using Core.PopupsSystem;
//using Core.UI;
//using Game.Core;
using VContainer;
using VContainer.Unity;

public class BootStarter : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        /*
        builder.Register<SceneLoader>(Lifetime.Scoped).As<SceneLoader, IStartable, ITickable>();
        builder.Register<PopupsModule>(Lifetime.Scoped).As<PopupsModule, IStartable>();
        builder.Register<UIController>(Lifetime.Scoped);
        */
        builder.Register<SaveModule>(Lifetime.Scoped);
        builder.Register<ProgressController>(Lifetime.Scoped).As<ProgressController, IStartable>();
	}
}