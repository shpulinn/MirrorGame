using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameInstaller : LifetimeScope
{
    [SerializeField] private NetworkManagerCustom networkManager;
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponent(networkManager).AsSelf();
    }
}