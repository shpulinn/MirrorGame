using VContainer;
using VContainer.Unity;

public class PlayerScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<PlayerInput>(Lifetime.Scoped);
    }
}