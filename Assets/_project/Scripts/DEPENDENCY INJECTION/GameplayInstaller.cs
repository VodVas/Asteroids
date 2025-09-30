using AsteroidsClone;
using Zenject;

public sealed class GameplayInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<CollisionDetector>().AsSingle();

        Container.Bind<CollisionMath>().AsSingle();
        Container.Bind<CollisionHandler>().AsSingle();
        Container.Bind<PlayerController>().AsSingle();
        Container.Bind<WeaponController>().AsSingle();
        Container.Bind<EntityController>().AsSingle();
        Container.Bind<EntryPoint>().AsSingle();
    }
}