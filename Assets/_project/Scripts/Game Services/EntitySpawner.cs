using UnityEngine;

namespace AsteroidsClone
{
    public sealed class EntitySpawner : IEntitySpawner
    {
        private readonly IEntityFactory _entityFactory;
        private readonly EntityRegistry _entityRegistry;
        private readonly SpawningConfig _spawningConfig;
        private readonly GameState _gameState;

        private float _asteroidSpawnTimer;
        private float _ufoSpawnTimer;
        private float _currentSpawnDelay;

        public EntitySpawner(IEntityFactory entityFactory, EntityRegistry entityManager, 
                           SpawningConfig spawningConfig, GameState gameState)
        {
            _entityFactory = entityFactory;
            _entityRegistry = entityManager;
            _spawningConfig = spawningConfig;
            _gameState = gameState;
            _currentSpawnDelay = spawningConfig.InitialSpawnDelay;
        }

        public void Update(float deltaTime)
        {
            if (_gameState.IsGameOver) return;

            _asteroidSpawnTimer += deltaTime;
            _ufoSpawnTimer += deltaTime;

            if (_asteroidSpawnTimer >= _currentSpawnDelay)
            {
                SpawnAsteroid();
                _asteroidSpawnTimer = 0f;
                _currentSpawnDelay = Mathf.Max(_spawningConfig.MinSpawnDelay, _currentSpawnDelay * _spawningConfig.SpawnAcceleration);
            }

            if (_ufoSpawnTimer >= _currentSpawnDelay * _spawningConfig.UfoSpawnDelayMultiplier)
            {
                SpawnUfo();
                _ufoSpawnTimer = 0f;
            }
        }

        public void SpawnAsteroid(Vector2? position = null, int size = 0)
        {
            var asteroid = _entityFactory.CreateAsteroid(position, size);
            _entityRegistry.AddEntity(asteroid);
        }

        public void SpawnUfo()
        {
            var ufo = _entityFactory.CreateUfo();
            _entityRegistry.AddEntity(ufo);
        }

        public void Reset()
        {
            _asteroidSpawnTimer = 0f;
            _ufoSpawnTimer = 0f;
            _currentSpawnDelay = _spawningConfig.InitialSpawnDelay;
        }
    }
}
