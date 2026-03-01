using System;
using UnityEngine;
using Zenject;

namespace AsteroidsClone
{
    public sealed class CoreInstaller : MonoInstaller
    {
        [SerializeField] private ScreenConfig _screenConfig;
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private WeaponsConfig _weaponsConfig;
        [SerializeField] private AsteroidConfig _asteroidConfig;
        [SerializeField] private UfoConfig _ufoConfig;
        [SerializeField] private SpawningConfig _spawningConfig;
        [SerializeField] private ViewConfig _viewConfig;
        [SerializeField] private InputConfig _inputConfig;

        public override void InstallBindings()
        {
            ValidateConfigs();

            Container.Bind<ScreenConfig>().FromInstance(_screenConfig).AsSingle();
            Container.Bind<PlayerConfig>().FromInstance(_playerConfig).AsSingle();
            Container.Bind<WeaponsConfig>().FromInstance(_weaponsConfig).AsSingle();
            Container.Bind<AsteroidConfig>().FromInstance(_asteroidConfig).AsSingle();
            Container.Bind<UfoConfig>().FromInstance(_ufoConfig).AsSingle();
            Container.Bind<SpawningConfig>().FromInstance(_spawningConfig).AsSingle();
            Container.Bind<ViewConfig>().FromInstance(_viewConfig).AsSingle();
            Container.Bind<InputConfig>().FromInstance(_inputConfig).AsSingle();
            Container.Bind<GameState>().AsSingle();
            Container.Bind<Player>().AsSingle();

            Container.BindInterfacesAndSelfTo<RandomService>().AsSingle();
            Container.BindInterfacesAndSelfTo<InputService>().AsSingle();
        }

        private void ValidateConfigs()
        {
            if (_screenConfig == null) throw new ArgumentNullException(nameof(_screenConfig));
            if (_playerConfig == null) throw new ArgumentNullException(nameof(_playerConfig));
            if (_weaponsConfig == null) throw new ArgumentNullException(nameof(_weaponsConfig));
            if (_asteroidConfig == null) throw new ArgumentNullException(nameof(_asteroidConfig));
            if (_ufoConfig == null) throw new ArgumentNullException(nameof(_ufoConfig));
            if (_spawningConfig == null) throw new ArgumentNullException(nameof(_spawningConfig));
            if (_viewConfig == null) throw new ArgumentNullException(nameof(_viewConfig));
            if (_inputConfig == null) throw new ArgumentNullException(nameof(_inputConfig));
        }
    }
}