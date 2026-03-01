using System;
using UnityEngine;

namespace AsteroidsClone
{
    public sealed class EntryPoint : IDisposable
    {
        private readonly GameState _gameState;
        private readonly SpawningConfig _spawningConfig;
        private readonly IInputService _inputService;
        private readonly IEntitySpawner _entitySpawner;
        private readonly ICollisionDetector _collisionDetector;
        private readonly CollisionHandler _collisionHandler;
        private readonly PlayerController _playerController;
        private readonly WeaponController _weaponController;
        private readonly EntityController _entityController;

        public EntryPoint(SpawningConfig spawningConfig, IInputService inputService, GameState gameState,
            PlayerController playerController, WeaponController weaponController, EntityController entityController,
            IEntitySpawner entitySpawner, ICollisionDetector collisionService, CollisionHandler collisionHandler)
        {
            _spawningConfig = spawningConfig;
            _inputService = inputService;
            _gameState = gameState;
            _playerController = playerController;
            _weaponController = weaponController;
            _entityController = entityController;
            _entitySpawner = entitySpawner;
            _collisionDetector = collisionService;
            _collisionHandler = collisionHandler;
        }

        public void Initialize()
        {
            _gameState.Reset();
            _playerController.Initialize();
            _entityController.Initialize();
            _entitySpawner.Reset();
            _gameState.SubscribeToCollisions(_collisionHandler);

            SpawnInitialAsteroids();
        }

        public void Update(float deltaTime)
        {
            if (HandleRestart()) return;
            if (_gameState.IsGameOver) return;

            _playerController.Update(deltaTime);
            _weaponController.Update(deltaTime);
            _entityController.Update(deltaTime);

            _entitySpawner.Update(deltaTime);
            _collisionDetector.CheckCollisions();
        }

        private bool HandleRestart()
        {
            if (_inputService.RestartGame && _gameState.IsGameOver)
            {
                Initialize();
                return true;
            }

            return false;
        }

        private void SpawnInitialAsteroids()
        {
            for (int i = 0; i < _spawningConfig.InitialAsteroidsCount; i++)
            {
                _entitySpawner.SpawnAsteroid();
            }
        }

        public void Dispose()
        {
            _playerController?.Dispose();
        }
    }
}