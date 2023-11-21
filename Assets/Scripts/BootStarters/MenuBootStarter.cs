using Merge;
using System;
using VContainer;
using VContainer.Unity;

namespace UI
{
    public class MenuBootStarter : BootStarter
    {
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            builder.Register<MenuController>(Lifetime.Scoped).As<MenuController, IStartable, IDisposable>();

			builder.Register<LvlProgressController>(Lifetime.Scoped).As<LvlProgressController, IStartable, IDisposable>();
			builder.Register<MergeController>(Lifetime.Scoped).As<MergeController, IStartable, IDisposable>();
        }
    }
}