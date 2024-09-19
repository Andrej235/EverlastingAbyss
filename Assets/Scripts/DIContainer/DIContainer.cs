using Zenject;

public class DIContainer : MonoInstaller
{
    public override void InstallBindings()
    {
        _ = Container.Bind<PlayerStateManager>().AsSingle();
    }
}
