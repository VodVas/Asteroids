using Zenject;

namespace AsteroidsClone
{
    public sealed class EntityInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<EntityRegistry>().AsSingle();
            Container.BindInterfacesAndSelfTo<EntityFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<EntitySpawner>().AsSingle();
        }
    }
}